using MartinGC94.DeviceManager.API;
using System.ComponentModel;
using System.Management.Automation;

namespace MartinGC94.DeviceManager.Commands
{
    [Cmdlet(VerbsCommon.Show, "DeviceUpdateWizard")]
    public sealed class ShowDeviceUpdateWizardCommand : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
        public Device Device { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                Device.LaunchUpdateDeviceWizard();
            }
            catch (Win32Exception e)
            {
                WriteError(e.NewErrorRecord("InstallFailed", Device.DevicePath));
            }
        }
    }
}