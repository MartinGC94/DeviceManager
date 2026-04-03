using System;

namespace MartinGC94.DeviceManager.Native.Enums
{
    [Flags]
    public enum DevInstanceStatusFlags : uint
    {
        DN_ROOT_ENUMERATED = 0x00000001, // Was enumerated by ROOT
        DN_DRIVER_LOADED   = 0x00000002, // Has Register_Device_Driver
        DN_ENUM_LOADED     = 0x00000004, // Has Register_Enumerator
        DN_STARTED         = 0x00000008, // Is currently configured
        DN_MANUAL          = 0x00000010, // Manually installed
        DN_NEED_TO_ENUM    = 0x00000020, // May need reenumeration
        DN_NOT_FIRST_TIME  = 0x00000040, // Has received a config
        DN_HARDWARE_ENUM   = 0x00000080, // Enum generates hardware ID
        DN_LIAR            = 0x00000100, // Lied about can reconfig once
        DN_HAS_MARK        = 0x00000200, // Not CM_Create_DevInst lately
        DN_HAS_PROBLEM     = 0x00000400, // Need device installer
        DN_FILTERED        = 0x00000800, // Is filtered
        DN_MOVED           = 0x00001000, // Has been moved
        DN_DISABLEABLE     = 0x00002000, // Can be disabled
        DN_REMOVABLE       = 0x00004000, // Can be removed
        DN_PRIVATE_PROBLEM = 0x00008000, // Has a private problem
        DN_MF_PARENT       = 0x00010000, // Multi function parent
        DN_MF_CHILD        = 0x00020000, // Multi function child
        DN_WILL_BE_REMOVED = 0x00040000, // DevInst is being removed
        DN_NOT_FIRST_TIMEE = 0x00080000, // S: Has received a config enumerate
        DN_STOP_FREE_RES   = 0x00100000, // S: When child is stopped, free resources
        DN_REBAL_CANDIDATE = 0x00200000, // S: Don't skip during rebalance
        DN_BAD_PARTIAL     = 0x00400000, // S: This devnode's log_confs do not have same resources
        DN_NT_ENUMERATOR   = 0x00800000, // S: This devnode's is an NT enumerator
        DN_NT_DRIVER       = 0x01000000, // S: This devnode's is an NT driver
        DN_NEEDS_LOCKING   = 0x02000000, // S: Devnode need lock resume processing
        DN_ARM_WAKEUP      = 0x04000000, // S: Devnode can be the wakeup device
        DN_APM_ENUMERATOR  = 0x08000000, // S: APM aware enumerator
        DN_APM_DRIVER      = 0x10000000, // S: APM aware driver
        DN_SILENT_INSTALL  = 0x20000000, // S: Silent install
        DN_NO_SHOW_IN_DM   = 0x40000000, // S: No show in device manager
        DN_BOOT_LOG_PROB   = 0x80000000, // S: Had a problem during preassignment of boot log conf
        DN_NEED_RESTART          =       DN_LIAR,                 // System needs to be restarted for this Devnode to work properly
        DN_DRIVER_BLOCKED        =       DN_NOT_FIRST_TIME,       // One or more drivers are blocked from loading for this Devnode
        DN_LEGACY_DRIVER         =       DN_MOVED,                // This device is using a legacy driver
        DN_CHILD_WITH_INVALID_ID =       DN_HAS_MARK,             // One or more children have invalid ID(s)
        DN_DEVICE_DISCONNECTED   =       DN_NEEDS_LOCKING,        // The function driver for a device reported that the device is not connected.  Typically this means a wireless device is out of range.
        DN_QUERY_REMOVE_PENDING  =       DN_MF_PARENT,            // Device is part of a set of related devices collectively pending query-removal
        DN_QUERY_REMOVE_ACTIVE   =       DN_MF_CHILD             // Device is actively engaged in a query-remove IRP
    }
}
