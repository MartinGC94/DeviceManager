using MartinGC94.DeviceManager.API;
using System.ComponentModel;
using System.Management.Automation;

namespace MartinGC94.DeviceManager.Commands
{
    [Cmdlet(VerbsLifecycle.Enable, "Device", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public sealed class EnableDeviceCommand : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
        public Device Device { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldProcess(Device.ToString()))
            {
                try
                {
                    Device.SetDeviceState(enabled: true);
                }
                catch (Win32Exception e)
                {
                    WriteError(e.NewErrorRecord("EnableDeviceError", Device.DevicePath));
                }
            }
        }
    }
}