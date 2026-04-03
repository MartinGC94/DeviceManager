using System;

namespace MartinGC94.DeviceManager.Native.Enums
{
    [Flags]
    public enum PD_Capabilities : uint
    {
        PDCAP_D0_SUPPORTED           = 0x00000001,
        PDCAP_D1_SUPPORTED           = 0x00000002,
        PDCAP_D2_SUPPORTED           = 0x00000004,
        PDCAP_D3_SUPPORTED           = 0x00000008,
        PDCAP_WAKE_FROM_D0_SUPPORTED = 0x00000010,
        PDCAP_WAKE_FROM_D1_SUPPORTED = 0x00000020,
        PDCAP_WAKE_FROM_D2_SUPPORTED = 0x00000040,
        PDCAP_WAKE_FROM_D3_SUPPORTED = 0x00000080,
        PDCAP_WARM_EJECT_SUPPORTED   = 0x00000100
    }
}