using MartinGC94.DeviceManager.Native.Enums;
using System.Runtime.InteropServices;

namespace MartinGC94.DeviceManager.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SP_CLASSINSTALL_HEADER
    {
        public int cbSize;
        public DIF InstallFunction;
    }
}