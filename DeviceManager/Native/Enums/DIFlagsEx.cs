using System;

namespace MartinGC94.DeviceManager.Native.Enums
{
    [Flags]
    public enum DIFlagsEx : uint
    {
        DI_FLAGSEX_RESERVED2                = 0x00000001,  // DI_FLAGSEX_USEOLDINFSEARCH is obsolete
        DI_FLAGSEX_RESERVED3                = 0x00000002,  // DI_FLAGSEX_AUTOSELECTRANK0 is obsolete
        DI_FLAGSEX_CI_FAILED                = 0x00000004,  // Failed to Load/Call class installer
        DI_FLAGSEX_FINISHINSTALL_ACTION     = 0x00000008,  // Class/co-installer wants to get a DIF_FINISH_INSTALL
        DI_FLAGSEX_DIDINFOLIST              = 0x00000010,  // Did the Class Info List
        DI_FLAGSEX_DIDCOMPATINFO            = 0x00000020,  // Did the Compat Info List
        DI_FLAGSEX_FILTERCLASSES            = 0x00000040,
        DI_FLAGSEX_SETFAILEDINSTALL         = 0x00000080,
        DI_FLAGSEX_DEVICECHANGE             = 0x00000100,
        DI_FLAGSEX_ALWAYSWRITEIDS           = 0x00000200,
        DI_FLAGSEX_PROPCHANGE_PENDING       = 0x00000400,  // One or more device property sheets have had changes made // to them, and need to have a DIF_PROPERTYCHANGE occur.
        DI_FLAGSEX_ALLOWEXCLUDEDDRVS        = 0x00000800,
        DI_FLAGSEX_NOUIONQUERYREMOVE        = 0x00001000,
        DI_FLAGSEX_USECLASSFORCOMPAT        = 0x00002000,  // Use the device's class when building compat drv list.// (Ignored if DI_COMPAT_FROM_CLASS flag is specified.)
        DI_FLAGSEX_RESERVED4                = 0x00004000,  // DI_FLAGSEX_OLDINF_IN_CLASSLIST is obsolete
        DI_FLAGSEX_NO_DRVREG_MODIFY         = 0x00008000,  // Don't run AddReg and DelReg for device's software (driver) key.
        DI_FLAGSEX_IN_SYSTEM_SETUP          = 0x00010000,  // Installation is occurring during initial system setup.
        DI_FLAGSEX_INET_DRIVER              = 0x00020000,  // Driver came from Windows Update
        DI_FLAGSEX_APPENDDRIVERLIST         = 0x00040000,  // Cause SetupDiBuildDriverInfoList to append// a new driver list to an existing list.
        DI_FLAGSEX_PREINSTALLBACKUP         = 0x00080000,  // not used
        DI_FLAGSEX_BACKUPONREPLACE          = 0x00100000,  // not used
        DI_FLAGSEX_DRIVERLIST_FROM_URL      = 0x00200000,  // build driver list from INF(s) retrieved from URL specified// in SP_DEVINSTALL_PARAMS.DriverPath (empty string means// Windows Update website)
        DI_FLAGSEX_RESERVED1                = 0x00400000,
        DI_FLAGSEX_EXCLUDE_OLD_INET_DRIVERS = 0x00800000,  // Don't include old Internet drivers when building// a driver list.// Ignored on Windows Vista and later.
        DI_FLAGSEX_POWERPAGE_ADDED          = 0x01000000,  // class installer added their own power page
        DI_FLAGSEX_FILTERSIMILARDRIVERS     = 0x02000000,  // only include similar drivers in class list
        DI_FLAGSEX_INSTALLEDDRIVER          = 0x04000000,  // only add the installed driver to the class or compat
        DI_FLAGSEX_NO_CLASSLIST_NODE_MERGE  = 0x08000000,  // Don't remove identical driver nodes from the class list
        DI_FLAGSEX_ALTPLATFORM_DRVSEARCH    = 0x10000000,  // Build driver list based on alternate platform
        DI_FLAGSEX_RESTART_DEVICE_ONLY      = 0x20000000,  // only restart the device drivers are being installed on
        DI_FLAGSEX_RECURSIVESEARCH          = 0x40000000,  // Tell SetupDiBuildDriverInfoList to do a recursive search
        DI_FLAGSEX_SEARCH_PUBLISHED_INFS    = 0x80000000  // Tell SetupDiBuildDriverInfoList to do a "published INF"
    }
}