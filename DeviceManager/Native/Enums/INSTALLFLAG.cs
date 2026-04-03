using System;

namespace MartinGC94.DeviceManager.Native.Enums
{
    [Flags]
    internal enum INSTALLFLAG : uint
    {
        INSTALLFLAG_FORCE          = 0x00000001,  // Force the installation of the specified driver
        INSTALLFLAG_READONLY       = 0x00000002,  // Do a read-only install (no file copy)
        INSTALLFLAG_NONINTERACTIVE = 0x00000004  // No UI shown at all. API will fail if any UI must be shown.
    }
}