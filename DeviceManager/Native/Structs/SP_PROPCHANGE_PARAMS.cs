using MartinGC94.DeviceManager.Native.Enums;
using System.Runtime.InteropServices;

namespace MartinGC94.DeviceManager.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SP_PROPCHANGE_PARAMS
    {
        public SP_CLASSINSTALL_HEADER ClassInstallHeader;
        public DICS_State StateChange;
        public uint Scope;
        public uint HwProfile;
    }
}