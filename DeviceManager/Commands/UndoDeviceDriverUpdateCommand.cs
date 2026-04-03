using MartinGC94.DeviceManager.API;
using System;
using System.ComponentModel;
using System.Management.Automation;
using static MartinGC94.DeviceManager.Native.Constants;

namespace MartinGC94.DeviceManager.Commands
{
    [Cmdlet(VerbsCommon.Undo, "DeviceDriverUpdate", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("Rollback-DeviceDriver")]
    public sealed class UndoDeviceDriverUpdateCommand : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
        public Device Device { get; set; }

        [Parameter]
        public SwitchParameter ShowUI { get; set; }

        protected override void ProcessRecord()
        {
            string rollbackTarget = Device.GetRollbackTarget();
            if (string.IsNullOrEmpty(rollbackTarget))
            {
                rollbackTarget = "Unavailable";
            }

            string whatIfText = $"Rollback driver for: {Device} to driver: {rollbackTarget}";
            if (ShouldProcess(whatIfText, whatIfText, "Rollback driver?"))
            {
                bool rebootRequired;
                try
                {
                    rebootRequired = Device.RollbackDriver(ShowUI);
                }
                catch (Win32Exception e)
                {
                    if (e.NativeErrorCode == ERROR_CANCELLED)
                    {
                        return;
                    }

                    if (e.NativeErrorCode == ERROR_NO_MORE_ITEMS)
                    {
                        var newError = new InvalidOperationException("There is no driver to roll back to.", e);
                        WriteError(new ErrorRecord(newError, "NoRollbackDriver", ErrorCategory.ObjectNotFound, Device.DevicePath));
                    }
                    else
                    {
                        WriteError(e.NewErrorRecord("RollbackError", Device.DevicePath));
                    }
                    
                    return;
                }

                if (rebootRequired && !ShowUI)
                {
                    WriteWarning($"A reboot is required to finish the driver rollback for {Device}");
                }
            }
        }
    }
}