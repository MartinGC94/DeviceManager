using MartinGC94.DeviceManager.Native.Enums;
using System;
using System.Runtime.InteropServices;

namespace MartinGC94.DeviceManager.Native.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
    internal struct SP_DRVINSTALL_PARAMS_x64
    {
        public int cbSize;
        public uint Rank;
        public DNFFlags Flags;
        public UIntPtr PrivateData;
        public uint Reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]
    internal struct SP_DRVINSTALL_PARAMS_x86
    {
        public int cbSize;
        public uint Rank;
        public DNFFlags Flags;
        public UIntPtr PrivateData;
        public uint Reserved;
    }
}