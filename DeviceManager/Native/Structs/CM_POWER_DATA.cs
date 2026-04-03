using MartinGC94.DeviceManager.Native.Enums;
using System.Runtime.InteropServices;

namespace MartinGC94.DeviceManager.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CM_POWER_DATA
    {
        public uint PD_Size;
        public DEVICE_POWER_STATE PD_MostRecentPowerState;
        public PD_Capabilities PD_Capabilities;
        public uint PD_D1Latency;
        public uint PD_D2Latency;
        public uint PD_D3Latency;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public DEVICE_POWER_STATE[] PD_PowerStateMapping;
        public SYSTEM_POWER_STATE PD_DeepestSystemWake;

        internal static CM_POWER_DATA FromByteArray(byte[] data)
        {
            GCHandle gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var outputData = Marshal.PtrToStructure<CM_POWER_DATA>(gcHandle.AddrOfPinnedObject());
            gcHandle.Free();
            return outputData;
        }
    }
}