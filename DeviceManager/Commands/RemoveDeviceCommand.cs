using MartinGC94.DeviceManager.API;
using System.ComponentModel;
using System.Management.Automation;

namespace MartinGC94.DeviceManager.Commands
{
    [Cmdlet(VerbsCommon.Remove, "Device", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public sealed class RemoveDeviceCommand : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
        public Device Device { get; set; }

        [Parameter]
        public SwitchParameter ShowUI { get; set; }

        protected override void ProcessRecord()
        {
            string deviceName = Device.ToString();
            if (ShouldProcess(deviceName))
            {
                bool rebootRequired;
                try
                {
                    rebootRequired = Device.RemoveDevice(ShowUI);
                }
                catch (Win32Exception e)
                {
                    WriteError(e.NewErrorRecord("DeviceRemovalError", Device.DevicePath));
                    return;
                }

                if (rebootRequired && !ShowUI)
                {
                    WriteWarning($"A reboot is required to finish removing device: {deviceName}");
                }
            }
        }
    }
}