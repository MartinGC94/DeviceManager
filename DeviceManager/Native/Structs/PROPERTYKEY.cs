using System;
using System.Runtime.InteropServices;

namespace MartinGC94.DeviceManager.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PROPERTYKEY
    {
        public Guid fmtid;
        public uint pid;

        public PROPERTYKEY(Guid InputId, uint InputPid)
        {
            fmtid = InputId;
            pid = InputPid;
        }

        public override string ToString()
        {
            return $"{{{fmtid}}}[{pid}]";
        }
    }
}