using System;

namespace MartinGC94.DeviceManager.Native.Enums
{
    [Flags]
    public enum DNFFlags : uint
    {
        DNF_ALWAYSEXCLUDEFROMLIST     = 0x00080000,
        DNF_AUTHENTICODE_SIGNED       = 0x00020000,
        DNF_BAD_DRIVER                = 0x00000800,
        DNF_BASIC_DRIVER              = 0x00010000,
        DNF_CLASS_DRIVER              = 0x00000020,
        DNF_COMPATIBLE_DRIVER         = 0x00000040,
        DNF_DUPDESC                   = 0x00000001,
        DNF_DUPDRIVERVER              = 0x00008000,
        DNF_DUPPROVIDER               = 0x00001000,
        DNF_EXCLUDEFROMLIST           = 0x00000004,
        DNF_INBOX_DRIVER              = 0x00100000,
        DNF_INET_DRIVER               = 0x00000080,
        DNF_INF_IS_SIGNED             = 0x00002000,
        DNF_INSTALLEDDRIVER           = 0x00040000,
        DNF_LEGACYINF                 = 0x00000010,
        DNF_NODRIVER                  = 0x00000008,
        DNF_OEM_F6_INF                = 0x00004000,
        DNF_OLD_INET_DRIVER           = 0x00000400,
        DNF_OLDDRIVER                 = 0x00000002,
        DNF_REQUESTADDITIONALSOFTWARE = 0x00200000
    }
}