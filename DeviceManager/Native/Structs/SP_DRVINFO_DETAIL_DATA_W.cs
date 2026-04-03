using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace MartinGC94.DeviceManager.Native.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
    internal struct SP_DRVINFO_DETAIL_DATA_W_x64
    {
        public int cbSize;
        public FILETIME InfDate;
        public int CompatIDsOffset;
        public int CompatIDsLength;
        public UIntPtr Reserved;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string SectionName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string InfFileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DrvDescription;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]
    internal struct SP_DRVINFO_DETAIL_DATA_W_x86
    {
        public int cbSize;
        public FILETIME InfDate;
        public int CompatIDsOffset;
        public int CompatIDsLength;
        public UIntPtr Reserved;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string SectionName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string InfFileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DrvDescription;
    }
}