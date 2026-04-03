using MartinGC94.DeviceManager.Native.Enums;
using MartinGC94.DeviceManager.Native.Structs;
using System;
using System.Runtime.InteropServices;

namespace MartinGC94.DeviceManager.Native
{
    internal class NativeMethods
    {
        #region setupapi
        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevsW(ref Guid ClassGuid, string Enumerator, IntPtr hwndParent, DIGCF Flags);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevsW(IntPtr ClassGuid, string Enumerator, IntPtr hwndParent, DIGCF Flags);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SetupDiCreateDeviceInfoList(ref Guid ClassGuid, IntPtr hwndParent);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SetupDiCreateDeviceInfoList(IntPtr ClassGuid, IntPtr hwndParent);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiBuildDriverInfoList(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, SPDIT DriverType);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiBuildDriverInfoList(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, SPDIT DriverType);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiCancelDriverInfoSearch(IntPtr DeviceInfoSet);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInstallParamsW(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, ref SP_DEVINSTALL_PARAMS_W_x64 DeviceInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInstallParamsW(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, ref SP_DEVINSTALL_PARAMS_W_x86 DeviceInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInstallParamsW(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref SP_DEVINSTALL_PARAMS_W_x64 DeviceInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInstallParamsW(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref SP_DEVINSTALL_PARAMS_W_x86 DeviceInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiSetDeviceInstallParamsW(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, ref SP_DEVINSTALL_PARAMS_W_x64 DeviceInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiSetDeviceInstallParamsW(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, ref SP_DEVINSTALL_PARAMS_W_x86 DeviceInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiSetDeviceInstallParamsW(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref SP_DEVINSTALL_PARAMS_W_x64 DeviceInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiSetDeviceInstallParamsW(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref SP_DEVINSTALL_PARAMS_W_x86 DeviceInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiCallClassInstaller(DIF InstallFunction, IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiClassGuidsFromNameW(string ClassName, [Out] Guid[] ClassGuidList, int ClassGuidListSize, out int RequiredSize);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiBuildClassInfoList(uint Flags, [Out] Guid[] ClassGuidList, int ClassGuidListSize, out int RequiredSize);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiBuildClassInfoList(uint Flags, IntPtr ClassGuidList, int ClassGuidListSize, out int RequiredSize);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiClassNameFromGuidW(ref Guid ClassGuid, [Out] char[] ClassName, int ClassNameSize, out int RequiredSize);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDevicePropertyW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref PROPERTYKEY PropertyKey,
            out DEVPROPTYPE PropertyType,
            byte[] PropertyBuffer,
            int PropertyBufferSize,
            out int RequiredSize,
            uint Flags);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDevicePropertyKeys(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            [Out] PROPERTYKEY[] PropertyKeyArray,
            uint PropertyKeyCount,
            out uint RequiredPropertyKeyCount,
            uint Flags);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDriverInfoW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            SPDIT DriverType,
            uint MemberIndex,
            ref SP_DRVINFO_DATA_V2_W_x64 DriverInfoData);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDriverInfoW(
            IntPtr DeviceInfoSet,
            IntPtr DeviceInfoData,
            SPDIT DriverType,
            uint MemberIndex,
            ref SP_DRVINFO_DATA_V2_W_x64 DriverInfoData);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDriverInfoW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            SPDIT DriverType,
            uint MemberIndex,
            ref SP_DRVINFO_DATA_V2_W_x86 DriverInfoData);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDriverInfoW(
            IntPtr DeviceInfoSet,
            IntPtr DeviceInfoData,
            SPDIT DriverType,
            uint MemberIndex,
            ref SP_DRVINFO_DATA_V2_W_x86 DriverInfoData);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDriverInstallParamsW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x64 DriverInfoData,
            ref SP_DRVINSTALL_PARAMS_x64 DriverInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDriverInstallParamsW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x86 DriverInfoData,
            ref SP_DRVINSTALL_PARAMS_x86 DriverInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDriverInstallParamsW(
            IntPtr DeviceInfoSet,
            IntPtr DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x64 DriverInfoData,
            ref SP_DRVINSTALL_PARAMS_x64 DriverInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDriverInstallParamsW(
            IntPtr DeviceInfoSet,
            IntPtr DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x86 DriverInfoData,
            ref SP_DRVINSTALL_PARAMS_x86 DriverInstallParams);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDriverInfoDetailW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x64 DriverInfoData,
            IntPtr DriverInfoDetailData,
            int DriverInfoDetailDataSize,
            out int RequiredSize);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDriverInfoDetailW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x86 DriverInfoData,
            IntPtr DriverInfoDetailData,
            int DriverInfoDetailDataSize,
            out int RequiredSize);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDriverInfoDetailW(
            IntPtr DeviceInfoSet,
            IntPtr DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x64 DriverInfoData,
            IntPtr DriverInfoDetailData,
            int DriverInfoDetailDataSize,
            out int RequiredSize);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDriverInfoDetailW(
            IntPtr DeviceInfoSet,
            IntPtr DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x86 DriverInfoData,
            IntPtr DriverInfoDetailData,
            int DriverInfoDetailDataSize,
            out int RequiredSize);

        [DllImport("setupapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiSetClassInstallParamsW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_PROPCHANGE_PARAMS ClassInstallParams,
            int ClassInstallParamsSize);
        #endregion

        #region Newdev
        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiRollbackDriver(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, IntPtr hwndParent, uint Flags, ref bool NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiRollbackDriver(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, IntPtr hwndParent, uint Flags, IntPtr NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiUninstallDevice(IntPtr hwndParent, IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, uint Flags, out bool NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiUninstallDevice(IntPtr hwndParent, IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, uint Flags, IntPtr NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool UpdateDriverForPlugAndPlayDevicesW(IntPtr hwndParent, string HardwareId, string FullInfPath, INSTALLFLAG InstallFlags, out bool NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool UpdateDriverForPlugAndPlayDevicesW(IntPtr hwndParent, string HardwareId, string FullInfPath, INSTALLFLAG InstallFlags, IntPtr NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiInstallDriverW(IntPtr hwndParent, string InfPath, DIIRFLAG Flags, out bool NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiInstallDriverW(IntPtr hwndParent, string InfPath, DIIRFLAG Flags, IntPtr NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiShowUpdateDevice(IntPtr hwndParent, IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, uint Flags, IntPtr NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiUninstallDriverW(IntPtr hwndParent, string InfPath, uint Flags, out bool NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiUninstallDriverW(IntPtr hwndParent, string InfPath, uint Flags, IntPtr NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiInstallDevice(
            IntPtr hwndParent,
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x64 DriverInfoData,
            DIIDFLAG Flags,
            out bool NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiInstallDevice(
            IntPtr hwndParent,
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x64 DriverInfoData,
            DIIDFLAG Flags,
            IntPtr NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiInstallDevice(
            IntPtr hwndParent,
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x86 DriverInfoData,
            DIIDFLAG Flags,
            out bool NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiInstallDevice(
            IntPtr hwndParent,
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_DRVINFO_DATA_V2_W_x86 DriverInfoData,
            DIIDFLAG Flags,
            IntPtr NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiInstallDevice(
            IntPtr hwndParent,
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            IntPtr DriverInfoData,
            DIIDFLAG Flags,
            out bool NeedReboot);

        [DllImport("Newdev.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DiInstallDevice(
            IntPtr hwndParent,
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            IntPtr DriverInfoData,
            DIIDFLAG Flags,
            IntPtr NeedReboot);
        #endregion

        #region cfgmgr32
        [DllImport("cfgmgr32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern CR_Codes CM_Enumerate_EnumeratorsW(uint ulEnumIndex, [Out] char[] Buffer, ref uint pulLength, uint ulFlags);

        [DllImport("cfgmgr32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern CR_Codes CM_Locate_DevNodeW(out IntPtr pdnDevInst, string pDeviceID, uint ulFlags);

        [DllImport("cfgmgr32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern CR_Codes CM_Reenumerate_DevNode(IntPtr dnDevInst, uint ulFlags);
        #endregion
    }
}