using MartinGC94.DeviceManager.API;
using MartinGC94.DeviceManager.Native.Enums;
using System;
using System.ComponentModel;
using System.Management.Automation;
using static MartinGC94.DeviceManager.Native.Constants;

namespace MartinGC94.DeviceManager.Commands
{
    [Cmdlet(VerbsLifecycle.Install, "DeviceDriver", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Default")]
    public sealed class InstallDeviceDriverCommand : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true, ParameterSetName = "Default")]
        [ValidateNotNull()]
        public DeviceDriver Driver { get; set; }

        [Parameter(ValueFromPipeline = true, ParameterSetName = "Default")]
        [ValidateNotNull]
        public Device Device { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "FromPath")]
        [Parameter(Mandatory = true, ParameterSetName = "WithHardwareIdAndPath")]
        public string[] Path
        {
            get => pathsToResolve;
            set
            {
                expandWildcards = true;
                pathsToResolve = value;
            }
        }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "FromLiteralPath")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "WithHardwareIdAndLiteralPath")]
        [Alias("FullName")]
        public string[] LiteralPath
        {
            get => pathsToResolve;
            set
            {
                expandWildcards = false;
                pathsToResolve = value;
            }
        }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "WithHardwareIdAndPath")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "WithHardwareIdAndLiteralPath")]
        public string HardwareId { get; set; }

        [Parameter(ParameterSetName = "FromPath")]
        [Parameter(ParameterSetName = "FromLiteralPath")]
        public SwitchParameter StageForNextBoot { get; set; }

        [Parameter(ParameterSetName = "FromPath")]
        [Parameter(ParameterSetName = "FromLiteralPath")]
        [Parameter(ParameterSetName = "WithHardwareIdAndPath")]
        [Parameter(ParameterSetName = "WithHardwareIdAndLiteralPath")]
        public SwitchParameter ForceInstall { get; set; }

        [Parameter(ParameterSetName = "Default")]
        public SwitchParameter InstallNullDriver { get; set; }

        [Parameter()]
        public SwitchParameter ShowUI { get; set; }

        private string[] pathsToResolve;
        private bool expandWildcards;

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "Default":
                    ProcessDefault();
                    break;

                case "FromPath":
                case "FromLiteralPath":
                    ProcessFromPath();
                    break;

                case "WithHardwareIdAndPath":
                case "WithHardwareIdAndLiteralPath":
                    ProcessFromHardwareId();
                    break;

                default:
                    throw new ArgumentException("Unexpected parameter set.");
            }
        }

        private void ProcessDefault()
        {
            if (Driver is null && Device is null)
            {
                var e = new ArgumentException("Neither Device nor Driver has a value assigned.");
                ErrorRecord record = e.NewErrorRecord("MissingArguments");
                record.SetRecommendedAction("Rerun the command while specifying a value for Device and/or Driver.");
                WriteError(record);
                return;
            }

            if (Driver != null)
            {
                ProcessDriverObject();
            }
            else
            {
                ProcessDevice(Device, Driver);
            }
        }

        private void ProcessDriverObject()
        {
            var device = Driver.Device ?? Device;
            if (device is null)
            {
                var exception = new ArgumentException("The driver object is not associated with a device.");
                ErrorRecord record = exception.NewErrorRecord("NoDeviceObject");
                record.SetRecommendedAction("Specify a device with the -Device parameter.");
                WriteError(record);
                return;
            }

            ProcessDevice(device, Driver);
        }

        private void ProcessDevice(Device device, DeviceDriver driver)
        {
            if (!InstallNullDriver && driver != null && driver.Device is null)
            {
                // Driver is not associated to the device so it cannot be installed.
                // We find it again to get a device associated driver.
                try
                {
                    driver = driver.GetDeviceAssociatedDriver(device);
                }
                catch (Win32Exception e)
                {
                    WriteError(e.NewErrorRecord("AssociateDriverWin32Error"));
                    return;
                }
                catch (ApiException e)
                {
                    WriteError(e.NewErrorRecord("AssociateDriverMatchError"));
                    return;
                }
            }

            string whatIf;
            if (InstallNullDriver)
            {
                whatIf = $"Install Null driver on {device}";
            }
            else if (driver is null)
            {
                whatIf = ShowUI
                    ? $"Install best driver from driver store or show search UI if none is found for {device}"
                    : $"Install best driver from driver store for {device}";
            }
            else
            {
                whatIf = $"Install driver '{driver}' on device: {device}";
            }

            if (ShouldProcess(whatIf, whatIf, "Install driver?"))
            {
                bool rebootRequired;
                try
                {
                    rebootRequired = device.InstallDevice(ShowUI, InstallNullDriver, driver);
                }
                catch (Win32Exception e)
                {
                    WriteError(e.NewErrorRecord("DeviceInstallError", device.DevicePath));
                    return;
                }

                if (rebootRequired && !ShowUI)
                {
                    WriteWarning($"A reboot is required to finish installation for device: {device}");
                }
            }
        }

        private void ProcessFromPath()
        {
            DIIRFLAG flags = 0;
            if (StageForNextBoot)
            {
                flags |= DIIRFLAG.DIIRFLAG_INSTALL_AS_SET;
            }
            if (ForceInstall)
            {
                flags |= DIIRFLAG.DIIRFLAG_FORCE_INF;
            }

            foreach (string resolvedPath in this.ResolveInputPaths(pathsToResolve, expandWildcards))
            {
                string whatIf;
                if (StageForNextBoot)
                {
                    whatIf = $"Stage drivers for install on next boot from directory: {resolvedPath}";
                }
                else if (ForceInstall)
                {
                    whatIf = $"Install driver on any matching device even if a better driver already exists. Driver: {resolvedPath}";
                }
                else
                {
                    whatIf = $"Install driver on any matching device if driver is a better match. Driver: {resolvedPath}";
                }

                if (ShouldProcess(whatIf, whatIf, "Install driver?"))
                {
                    bool rebootRequired;
                    try
                    {
                        rebootRequired = DeviceDriver.InstallDriver(resolvedPath, ShowUI, flags);
                    }
                    catch (Win32Exception e)
                    {
                        if (e.NativeErrorCode == ERROR_NO_MORE_ITEMS)
                        {
                            WriteErrorForNoMoreData(e, resolvedPath);
                            continue;
                        }

                        WriteError(e.NewErrorRecord("InstallFromPath", resolvedPath));
                        continue;
                    }

                    if (rebootRequired && !ShowUI)
                    {
                        WriteWarning($"A reboot is required to finish installation of driver: {resolvedPath}");
                    }
                }
            }
        }

        private void ProcessFromHardwareId()
        {
            foreach (string resolvedPath in this.ResolveInputPaths(pathsToResolve, expandWildcards))
            {
                string whatIf = ForceInstall
                ? $"Install driver on devices matching '{HardwareId}' even if a better driver already exists. Driver: {resolvedPath}"
                : $"Install driver on devices matching '{HardwareId}'. Driver: {resolvedPath}";
                if (ShouldProcess(whatIf, whatIf, "Install driver?"))
                {
                    bool rebootRequired;
                    try
                    {
                        rebootRequired = DeviceDriver.UpdateDriverForPlugAndPlayDevices(HardwareId, resolvedPath, ShowUI, ForceInstall);
                    }
                    catch (Win32Exception e)
                    {
                        if (e.NativeErrorCode == ERROR_NO_MORE_ITEMS)
                        {
                            WriteErrorForNoMoreData(e, resolvedPath);
                            continue;
                        }

                        WriteError(e.NewErrorRecord("UpdateFromPath", resolvedPath));
                        continue;
                    }

                    if (rebootRequired && !ShowUI)
                    {
                        WriteWarning($"A reboot is required to finish installation of driver: {resolvedPath}");
                    }
                }
            }
        }

        private void WriteErrorForNoMoreData(Win32Exception exception, object targetObject)
        {
            // This private function is used over the NewErrorRecord helper method because the intended error message and recommendation
            // is too specific for a general No_More_data error.
            var e = new ApiException("A device was found but the driver is not better than the existing one.", exception);
            var errorRecord = new ErrorRecord(e, "NotBetterThanExisting", ErrorCategory.InvalidData, targetObject);
            errorRecord.SetRecommendedAction("Rerun the command with the -ForceInstall parameter.");
            WriteError(errorRecord);
        }
    }
}