using MartinGC94.DeviceManager.API;
using System;
using System.ComponentModel;
using System.Management.Automation;

namespace MartinGC94.DeviceManager.Commands
{
    [Cmdlet(VerbsCommon.Remove, "DeviceDriver", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public sealed class RemoveDeviceDriverCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Driver", ValueFromPipeline = true)]
        public DeviceDriver Driver { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Path")]
        public string[] Path
        {
            get => pathsToResolve;
            set
            {
                expandWildcards = true;
                pathsToResolve = value;
            }
        }

        [Parameter(Mandatory = true, ParameterSetName = "LiteralPath", ValueFromPipelineByPropertyName = true)]
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

        [Parameter]
        public SwitchParameter KeepInf { get; set; }

        [Parameter]
        public SwitchParameter ShowUI { get; set; }

        private string[] pathsToResolve;
        private bool expandWildcards;
        protected override void ProcessRecord()
        {
            if (ParameterSetName.Equals("Driver", StringComparison.OrdinalIgnoreCase))
            {
                LiteralPath = new string[] { Driver.DriverDetails.InfFullName };
            }

            foreach (string resolvedPath in this.ResolveInputPaths(pathsToResolve, expandWildcards))
            {
                if (ShouldProcess(resolvedPath))
                {
                    bool rebootRequired;
                    try
                    {
                        rebootRequired = DeviceDriver.UninstallDriver(resolvedPath, ShowUI, KeepInf);
                    }
                    catch (Win32Exception e)
                    {
                        WriteError(e.NewErrorRecord("UninstallError", resolvedPath));
                        return;
                    }

                    if (rebootRequired && !ShowUI)
                    {
                        WriteWarning($"A reboot is required to finish removal of driver: {resolvedPath}");
                    }
                }
            }
        }
    }
}