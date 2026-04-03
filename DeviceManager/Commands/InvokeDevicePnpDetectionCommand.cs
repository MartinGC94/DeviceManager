using MartinGC94.DeviceManager.API;
using MartinGC94.DeviceManager.Native;
using MartinGC94.DeviceManager.Native.Enums;
using System;
using System.Management.Automation;

namespace MartinGC94.DeviceManager.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "DevicePnpDetection")]
    public sealed class InvokeDevicePnpDetectionCommand : Cmdlet
    {
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string DevicePath { get; set; }

        [Parameter()]
        public SwitchParameter Async { get; set; }

        protected override void ProcessRecord()
        {
            CR_Codes result = NativeMethods.CM_Locate_DevNodeW(out IntPtr deviceInstance, DevicePath, 0);
            if (result != CR_Codes.CR_SUCCESS)
            {
                var e = new ApiException($"Failed to locate device to rescan from. Error: {result}");
                WriteError(e.NewErrorRecord("DeviceLocateError", DevicePath));
                return;
            }

            uint flags = Async ? (uint)4 : 0;
            result = NativeMethods.CM_Reenumerate_DevNode(deviceInstance, flags);
            if (result != CR_Codes.CR_SUCCESS)
            {
                var e = new ApiException($"Failed to rescan for devices. Error: {result}");
                WriteError(e.NewErrorRecord("DeviceRescanError", DevicePath));
                return;
            }
        }
    }
}