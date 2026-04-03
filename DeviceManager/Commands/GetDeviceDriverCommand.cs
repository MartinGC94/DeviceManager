using MartinGC94.DeviceManager.API;
using MartinGC94.DeviceManager.API.ParamAttributes;
using MartinGC94.DeviceManager.Native;
using MartinGC94.DeviceManager.Native.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;

namespace MartinGC94.DeviceManager.Commands
{
    [Cmdlet(VerbsCommon.Get, "DeviceDriver", DefaultParameterSetName = "Default")]
    [OutputType(typeof(DeviceDriver))]
    public sealed class GetDeviceDriverCommand : PSCmdlet
    {
        [Parameter(ParameterSetName = "Default", ValueFromPipeline = true)]
        [Parameter(ParameterSetName = "FromPath", ValueFromPipeline = true)]
        [Parameter(ParameterSetName = "FromLiteralPath", ValueFromPipeline = true)]
        [ValidateNotNull]
        public Device Device { get; set; }

        [Parameter(ParameterSetName = "Default")]
        [Parameter(ParameterSetName = "FromPath")]
        [Parameter(ParameterSetName = "FromLiteralPath")]
        [ArgumentCompleter(typeof(DeviceClassCompleter))]
        [DeviceClassTransformer]
        [Alias("Class")]
        public string DeviceClass { get; set; }

        [Parameter(ParameterSetName = "FromPath", Mandatory = true)]
        public string[] Path
        {
            get => pathsToResolve;
            set
            {
                expandWildcards = true;
                pathsToResolve = value;
            }
        }

        [Parameter(ParameterSetName = "FromLiteralPath", Mandatory = true)]
        public string[] LiteralPath
        {
            get => pathsToResolve;
            set
            {
                expandWildcards = false;
                pathsToResolve = value;
            }
        }

        [Parameter(ParameterSetName = "FromPath")]
        [Parameter(ParameterSetName = "FromLiteralPath")]
        public SwitchParameter Recurse { get; set; }

        [Parameter(ParameterSetName = "Default")]
        [Parameter(ParameterSetName = "FromPath")]
        [Parameter(ParameterSetName = "FromLiteralPath")]
        public SwitchParameter IncludeAvailableDrivers { get; set; }

        [Parameter(ParameterSetName = "Default")]
        [Parameter(ParameterSetName = "FromPath")]
        [Parameter(ParameterSetName = "FromLiteralPath")]
        public SwitchParameter AllClassDrivers { get; set; }

        private string[] pathsToResolve;
        private bool expandWildcards;

        private List<string> resolvedPaths;
        private DeviceInfoSet deviceInfoSet;

        protected override void BeginProcessing()
        {
            if (pathsToResolve != null)
            {
                resolvedPaths = new List<string>(this.ResolveInputPaths(pathsToResolve, expandWildcards));
            }
        }

        protected override void ProcessRecord()
        {
            if (Device is null)
            {
                try
                {
                    deviceInfoSet = DeviceInfoSet.CreateEmptyDeviceInfoSet(DeviceClass);
                }
                catch (Win32Exception e)
                {
                    ThrowTerminatingError(e.NewErrorRecord("CreateDeviceInfoSetError", DeviceClass));
                    return;
                }

                // Without a device specified, it must be all drivers for the specified class.
                AllClassDrivers = true;

                if (ParameterSetName.Equals("Default", StringComparison.OrdinalIgnoreCase))
                {
                    // No device or driver paths have been specified so we need to search the driver store.
                    IncludeAvailableDrivers = true;
                }
            }
            else
            {
                if (DeviceClass != null)
                {
                    var e = new ArgumentException("The DeviceClass parameter may not be used when Device has been specified.");
                    ThrowTerminatingError(e.NewErrorRecord("DeviceClassSpecifiedWithDevice"));
                    return;
                }

                deviceInfoSet = Device.deviceInfoSet;
            }

            DevInstallParams installParams;
            try
            {
                installParams = deviceInfoSet.GetDeviceInstallParams(Device);
            }
            catch (Win32Exception e)
            {
                WriteError(e.NewErrorRecord("GetInitialInstallParams", Device?.DevicePath));
                return;
            }

            installParams.DriverPath = null;
            if (IncludeAvailableDrivers || ParameterSetName.Equals("Default", StringComparison.OrdinalIgnoreCase))
            {
                installParams.DriverPath = null;
                if (IncludeAvailableDrivers)
                {
                    installParams.FlagsEx |= DIFlagsEx.DI_FLAGSEX_SEARCH_PUBLISHED_INFS;
                }
                else if (Device != null)
                {
                    installParams.FlagsEx |= DIFlagsEx.DI_FLAGSEX_INSTALLEDDRIVER;
                    // We set AllClassDrivers to true because we want to find the installed driver and that driver may have been force installed.
                    AllClassDrivers = true;
                }
                
                try
                {
                    deviceInfoSet.SetDeviceInstallParams(installParams, Device);
                }
                catch (Win32Exception e)
                {
                    WriteError(e.NewErrorRecord("SetInstallParams", Device?.DevicePath));
                    return;
                }

                try
                {
                    deviceInfoSet.BuildDriverInfoList(AllClassDrivers, Device);
                }
                catch (Win32Exception e)
                {
                    WriteError(e.NewErrorRecord("BuildDriversList", Device?.DevicePath));
                    return;
                }

                installParams.FlagsEx &= ~DIFlagsEx.DI_FLAGSEX_SEARCH_PUBLISHED_INFS;
                installParams.FlagsEx &= ~DIFlagsEx.DI_FLAGSEX_INSTALLEDDRIVER;

                try
                {
                    deviceInfoSet.SetDeviceInstallParams(installParams, Device);
                }
                catch (Win32Exception e)
                {
                    WriteError(e.NewErrorRecord("ResetInstallParams", Device?.DevicePath));
                    return;
                }
            }

            if (resolvedPaths?.Count > 0)
            {
                installParams.FlagsEx |= DIFlagsEx.DI_FLAGSEX_APPENDDRIVERLIST;
                foreach (string path in resolvedPaths)
                {
                    // It could also be a directory that ends with ".inf" hence the File check.
                    if (File.Exists(path) && path.EndsWith(".inf", StringComparison.OrdinalIgnoreCase))
                    {
                        installParams.Flags |= DIFlags.DI_ENUMSINGLEINF;
                        installParams.FlagsEx &= ~DIFlagsEx.DI_FLAGSEX_RECURSIVESEARCH;
                    }
                    else
                    {
                        installParams.Flags &= ~DIFlags.DI_ENUMSINGLEINF;
                        if (Recurse)
                        {
                            installParams.FlagsEx |= DIFlagsEx.DI_FLAGSEX_RECURSIVESEARCH;
                        }
                    }

                    installParams.DriverPath = path;
                    try
                    {
                        deviceInfoSet.SetDeviceInstallParams(installParams, Device);
                    }
                    catch (Win32Exception e)
                    {
                        WriteError(e.NewErrorRecord("UpdatePathParams", path));
                        continue;
                    }

                    try
                    {
                        deviceInfoSet.BuildDriverInfoList(AllClassDrivers, Device);
                    }
                    catch (Win32Exception e)
                    {
                        WriteError(e.NewErrorRecord("BuildDriversListFromPath", path));
                    }
                }

                installParams.Flags &= ~DIFlags.DI_ENUMSINGLEINF;
                installParams.FlagsEx &= ~DIFlagsEx.DI_FLAGSEX_APPENDDRIVERLIST;
                installParams.FlagsEx &= ~DIFlagsEx.DI_FLAGSEX_RECURSIVESEARCH;
                installParams.DriverPath = null;
                try
                {
                    deviceInfoSet.SetDeviceInstallParams(installParams, Device);
                }
                catch (Win32Exception e)
                {
                    WriteError(e.NewErrorRecord("ResetPathParams"));
                }
            }

            foreach (EnumerationResult<DeviceDriver> resultItem in deviceInfoSet.EnumerateDrivers(AllClassDrivers, Device))
            {
                if (resultItem.Success)
                {
                    WriteObject(resultItem.item);
                }
                else
                {
                    WriteError(resultItem.exception.NewErrorRecord("EnumerationError", resultItem.itemIdentifier));
                }
            }
        }

        protected override void StopProcessing()
        {
            _ = NativeMethods.SetupDiCancelDriverInfoSearch(deviceInfoSet.handle);
        }
    }
}