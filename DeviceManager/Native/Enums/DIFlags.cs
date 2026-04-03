using System;

namespace MartinGC94.DeviceManager.Native.Enums
{
    [Flags]
    public enum DIFlags : uint
    {
        DI_SHOWOEM             = 0x00000001,
        DI_SHOWCOMPAT          = 0x00000002,
        DI_SHOWCLASS           = 0x00000004,
        DI_SHOWALL             = 0x00000007,
        DI_NOVCP               = 0x00000008,
        DI_DIDCOMPAT           = 0x00000010,
        DI_DIDCLASS            = 0x00000020,
        DI_AUTOASSIGNRES       = 0x00000040,
        DI_NEEDRESTART         = 0x00000080,
        DI_NEEDREBOOT          = 0x00000100,
        DI_NOBROWSE            = 0x00000200,
        DI_MULTMFGS            = 0x00000400,
        DI_DISABLED            = 0x00000800,
        DI_GENERALPAGE_ADDED   = 0x00001000,
        DI_RESOURCEPAGE_ADDED  = 0x00002000,
        DI_PROPERTIES_CHANGE   = 0x00004000,
        DI_INF_IS_SORTED       = 0x00008000,
        DI_ENUMSINGLEINF       = 0x00010000,
        DI_DONOTCALLCONFIGMG   = 0x00020000,
        DI_INSTALLDISABLED     = 0x00040000,
        DI_COMPAT_FROM_CLASS   = 0x00080000,
        DI_CLASSINSTALLPARAMS  = 0x00100000,
        DI_NODI_DEFAULTACTION  = 0x00200000,
        DI_QUIETINSTALL        = 0x00800000,
        DI_NOFILECOPY          = 0x01000000,
        DI_FORCECOPY           = 0x02000000,
        DI_DRIVERPAGE_ADDED    = 0x04000000,
        DI_USECI_SELECTSTRINGS = 0x08000000,
        DI_OVERRIDE_INFFLAGS   = 0x10000000,
        DI_PROPS_NOCHANGEUSAGE = 0x20000000,
        DI_NOSELECTICONS       = 0x40000000,
        DI_NOWRITE_IDS         = 0x80000000 
    }
}