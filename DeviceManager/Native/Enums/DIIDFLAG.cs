using System;

namespace MartinGC94.DeviceManager.Native.Enums
{
    [Flags]
    internal enum DIIDFLAG : uint
    {
        DIIDFLAG_SHOWSEARCHUI          = 0x00000001,      // Show search UI if no drivers can be found.
        DIIDFLAG_NOFINISHINSTALLUI     = 0x00000002,      // Do NOT show the finish install UI.
        DIIDFLAG_INSTALLNULLDRIVER     = 0x00000004,      // Install the NULL driver on this device.
        DIIDFLAG_INSTALLCOPYINFDRIVERS = 0x00000008      // Install any extra INFs specified via CopyInf directive.

    }
}