using System;

namespace MartinGC94.DeviceManager.Native.Enums
{
    [Flags]
    public enum DIIRFLAG : uint
    {
        DIIRFLAG_INF_ALREADY_COPIED = 0x00000001,   // Don't copy inf, it has been published
        DIIRFLAG_FORCE_INF          = 0x00000002,   // use the inf as if users picked it.
        DIIRFLAG_HW_USING_THE_INF   = 0x00000004,   // limit installs on hw using the inf
        DIIRFLAG_HOTPATCH           = 0x00000008,   // Perform a hotpatch service pack install
        DIIRFLAG_NOBACKUP           = 0x00000010,   // install w/o backup and no rollback
        DIIRFLAG_PRE_CONFIGURE_INF  = 0x00000020,   // Pre-install inf, if possible
        DIIRFLAG_INSTALL_AS_SET     = 0x00000040
    }
}