using MartinGC94.DeviceManager.Native;
using MartinGC94.DeviceManager.Native.Enums;
using MartinGC94.DeviceManager.Native.Structs;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static MartinGC94.DeviceManager.Native.Constants;

namespace MartinGC94.DeviceManager.API
{
    public sealed class DeviceDriver : IComparable<DeviceDriver>, IComparable
    {
        public string Description { get; }
        public string DeviceManufacturer { get; }
        public string Provider { get; }
        public DateTime DriverDate { get; }
        public Version DriverVersion { get; }
        public uint DriverRank => InstallParams.DriverRank;
        public bool IsInstalledDriver => InstallParams.Flags.HasFlag(DNFFlags.DNF_INSTALLEDDRIVER);
        public bool IsInboxDriver => InstallParams.Flags.HasFlag(DNFFlags.DNF_INBOX_DRIVER);
        public DriverDetails DriverDetails
        {
            get
            {
                if (_driverDetails is null)
                {
                    _driverDetails = GetDriverDetails();
                }

                return _driverDetails;
            }
        }
        public DriverInstallParams InstallParams
        {
            get
            {
                if (_installParams is null)
                {
                    _installParams = GetDriverInstallParams();
                }

                return _installParams;
            }
        }
        internal Device Device { get; }
        private DeviceInfoSet DeviceInfoSet { get; }
        private DriverDetails _driverDetails;
        private DriverInstallParams _installParams;
        private readonly SPDIT driverType;
        private readonly UIntPtr reservedDrvInfoPtr;

        #region Constructors
        private DeviceDriver(SP_DRVINFO_DATA_V2_W_x64 driverStruct)
        {
            Description = driverStruct.Description;
            DeviceManufacturer = driverStruct.MfgName;
            Provider = driverStruct.ProviderName;
            DriverDate = driverStruct.DriverDate.ToDateTimeUtc();
            DriverVersion = driverStruct.DriverVersion.ToVersion();
            driverType = driverStruct.DriverType;
            reservedDrvInfoPtr = driverStruct.Reserved;
        }

        internal DeviceDriver(SP_DRVINFO_DATA_V2_W_x64 driverStruct, DeviceInfoSet deviceSet) : this(driverStruct)
        {
            DeviceInfoSet = deviceSet;
        }

        internal DeviceDriver(SP_DRVINFO_DATA_V2_W_x64 driverStruct, Device associatedDevice) : this(driverStruct)
        {
            Device = associatedDevice;
            DeviceInfoSet = associatedDevice.deviceInfoSet;
        }

        private DeviceDriver(SP_DRVINFO_DATA_V2_W_x86 driverStruct)
        {
            Description = driverStruct.Description;
            DeviceManufacturer = driverStruct.MfgName;
            Provider = driverStruct.ProviderName;
            DriverDate = driverStruct.DriverDate.ToDateTimeUtc();
            DriverVersion = driverStruct.DriverVersion.ToVersion();
            driverType = driverStruct.DriverType;
            reservedDrvInfoPtr = driverStruct.Reserved;
        }

        internal DeviceDriver(SP_DRVINFO_DATA_V2_W_x86 driverStruct, DeviceInfoSet deviceSet) : this(driverStruct)
        {
            DeviceInfoSet = deviceSet;
        }

        internal DeviceDriver(SP_DRVINFO_DATA_V2_W_x86 driverStruct, Device associatedDevice) : this(driverStruct)
        {
            Device = associatedDevice;
            DeviceInfoSet = associatedDevice.deviceInfoSet;
        }
        #endregion

        #region Interface and override implementations
        public int CompareTo(DeviceDriver other)
        {
            if (other is null)
            {
                return -1;
            }

            int rankComparison = DriverRank.CompareTo(other.DriverRank);
            if (rankComparison != 0)
            {
                return rankComparison;
            }

            int dateComparison = other.DriverDate.CompareTo(DriverDate);
            if (dateComparison != 0)
            {
                return dateComparison;
            }

            return other.DriverVersion.CompareTo(DriverVersion);
        }

        public int CompareTo(object other)
        {
            if (other is DeviceDriver otherDriver)
            {
                return CompareTo(otherDriver);
            }

            return -1;
        }

        public override string ToString() => $"{Description} ({DriverVersion})";
        #endregion

        internal SP_DRVINFO_DATA_V2_W_x64 GetDriverInfoStructX64()
        {
            var result = new SP_DRVINFO_DATA_V2_W_x64()
            {
                Description = Description,
                DriverDate = DriverDate.ToFileTime2(),
                DriverType = driverType,
                DriverVersion = DriverVersion.ToUlong(),
                MfgName = DeviceManufacturer,
                ProviderName = Provider,
                Reserved = reservedDrvInfoPtr
            };

            result.cbSize = Marshal.SizeOf(result);

            return result;
        }

        internal SP_DRVINFO_DATA_V2_W_x86 GetDriverInfoStructX86()
        {
            var result = new SP_DRVINFO_DATA_V2_W_x86()
            {
                Description = Description,
                DriverDate = DriverDate.ToFileTime2(),
                DriverType = driverType,
                DriverVersion = DriverVersion.ToUlong(),
                MfgName = DeviceManufacturer,
                ProviderName = Provider,
                Reserved = reservedDrvInfoPtr
            };

            result.cbSize = Marshal.SizeOf(result);

            return result;
        }

        private bool SetupDiGetDriverInfoDetail(IntPtr buffer, int bufferSize, out int requiredSize)
        {
            if (IntPtr.Size == 8)
            {
                var structData = GetDriverInfoStructX64();
                return Device is null
                    ? NativeMethods.SetupDiGetDriverInfoDetailW(DeviceInfoSet.handle, IntPtr.Zero, ref structData, buffer, bufferSize, out requiredSize)
                    : NativeMethods.SetupDiGetDriverInfoDetailW(DeviceInfoSet.handle, ref Device.deviceInfoData, ref structData, buffer, bufferSize, out requiredSize);
            }
            else
            {
                var structData = GetDriverInfoStructX86();
                return Device is null
                    ? NativeMethods.SetupDiGetDriverInfoDetailW(DeviceInfoSet.handle, IntPtr.Zero, ref structData, buffer, bufferSize, out requiredSize)
                    : NativeMethods.SetupDiGetDriverInfoDetailW(DeviceInfoSet.handle, ref Device.deviceInfoData, ref structData, buffer, bufferSize, out requiredSize);
            }
        }

        private DriverDetails GetDriverDetails()
        {
            int idBufferSize = 512;
            int structSize;
            int idFieldSize;

            // Due to the last field (HardwareID) being dynamic it has been left out of the struct definition.
            // So we get the size of both the struct and the last field so we can manually allocate the memory and assign the correct cbSize.
            if (IntPtr.Size == 8)
            {
                structSize = Marshal.SizeOf<SP_DRVINFO_DETAIL_DATA_W_x64>();
                idFieldSize = 8;
            }
            else
            {
                structSize = Marshal.SizeOf<SP_DRVINFO_DETAIL_DATA_W_x86>();
                idFieldSize = 2;
            }

            int totalBufferSize = structSize + idFieldSize + idBufferSize;
            IntPtr buffer = Marshal.AllocHGlobal(totalBufferSize);
            try
            {
                // Sets cbSize in the struct to the size of the struct + id field.
                Marshal.WriteInt32(buffer, structSize + idFieldSize);

                if (!SetupDiGetDriverInfoDetail(buffer, totalBufferSize, out int requiredSize))
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    if (errorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        Marshal.FreeHGlobal(buffer);
                        buffer = Marshal.AllocHGlobal(requiredSize);
                        Marshal.WriteInt32(buffer, structSize + idFieldSize);

                        if (!SetupDiGetDriverInfoDetail(buffer, requiredSize, out _))
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        throw new Win32Exception(errorCode);
                    }
                }

                DriverDetails details;
                int compatIdOffset;
                int compatIdLength;
                if (IntPtr.Size == 8)
                {
                    var structData = Marshal.PtrToStructure<SP_DRVINFO_DETAIL_DATA_W_x64>(buffer);
                    details = new DriverDetails(structData);
                    compatIdOffset = structData.CompatIDsOffset;
                    compatIdLength = structData.CompatIDsLength;
                }
                else
                {
                    var structData = Marshal.PtrToStructure<SP_DRVINFO_DETAIL_DATA_W_x86>(buffer);
                    details = new DriverDetails(structData);
                    compatIdOffset = structData.CompatIDsOffset;
                    compatIdLength = structData.CompatIDsLength;
                }

                if (compatIdOffset > 1)
                {
                    IntPtr hardwareIdPointer = IntPtr.Add(buffer, structSize);
                    details.HardwareId = Marshal.PtrToStringUni(hardwareIdPointer);
                }
                else
                {
                    details.HardwareId = string.Empty;
                }

                if (compatIdLength > 0)
                {
                    IntPtr compatibleIdPointer = IntPtr.Add(buffer, structSize + compatIdOffset + 1);
                    string compatibleIds = Marshal.PtrToStringUni(compatibleIdPointer, compatIdLength);
                    details.CompatibleIds = compatibleIds.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                }

                return details;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        private DriverInstallParams GetDriverInstallParams()
        {
            bool result;
            if (IntPtr.Size == 8)
            {
                var driverInfo = GetDriverInfoStructX64();
                var installParams = new SP_DRVINSTALL_PARAMS_x64();
                installParams.cbSize = Marshal.SizeOf(installParams);
                result = Device is null
                    ? NativeMethods.SetupDiGetDriverInstallParamsW(DeviceInfoSet.handle, IntPtr.Zero, ref driverInfo, ref installParams)
                    : NativeMethods.SetupDiGetDriverInstallParamsW(DeviceInfoSet.handle, ref Device.deviceInfoData, ref driverInfo, ref installParams);
                if (result)
                {
                    return new DriverInstallParams(installParams);
                }
            }
            else
            {
                var driverInfo = GetDriverInfoStructX86();
                var installParams = new SP_DRVINSTALL_PARAMS_x86();
                installParams.cbSize = Marshal.SizeOf(installParams);
                result = Device is null
                    ? NativeMethods.SetupDiGetDriverInstallParamsW(DeviceInfoSet.handle, IntPtr.Zero, ref driverInfo, ref installParams)
                    : NativeMethods.SetupDiGetDriverInstallParamsW(DeviceInfoSet.handle, ref Device.deviceInfoData, ref driverInfo, ref installParams);
                if (result)
                {
                    return new DriverInstallParams(installParams);
                }
            }

            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Finds the same driver again and associates the specified device with it.
        /// This only makes sense to use if the current driver was found with a global search.
        /// </summary>
        public DeviceDriver GetDeviceAssociatedDriver(Device device)
        {
            string driverPath = DriverDetails.InfFullName;

            DevInstallParams installParams = device.deviceInfoSet.GetDeviceInstallParams(device);
            installParams.DriverPath = driverPath;
            installParams.Flags |= DIFlags.DI_ENUMSINGLEINF;
            device.deviceInfoSet.SetDeviceInstallParams(installParams, device);

            device.deviceInfoSet.BuildDriverInfoList(allClassDrivers: true, device);

            installParams.DriverPath = null;
            installParams.Flags &= ~DIFlags.DI_ENUMSINGLEINF;
            device.deviceInfoSet.SetDeviceInstallParams(installParams, device);

            foreach (EnumerationResult<DeviceDriver> result in device.deviceInfoSet.EnumerateDrivers(allClassDrivers: true, device))
            {
                if (result.Success)
                {
                    DeviceDriver driver = result.item;
                    if (Description.Equals(driver.Description)
                        && DeviceManufacturer.Equals(driver.DeviceManufacturer)
                        && Provider.Equals(driver.Provider)
                        && DriverDate.Equals(driver.DriverDate)
                        && DriverVersion.Equals(driver.DriverVersion))
                    {
                        return driver;
                    }
                }
            }

            throw new ApiException("Failed to find a driver that matches the original.");
        }

        public static bool InstallDriver(string path, bool showUI, DIIRFLAG flags)
        {
            bool needReboot = false;
            bool success = showUI
                ? NativeMethods.DiInstallDriverW(IntPtr.Zero, path, flags, IntPtr.Zero)
                : NativeMethods.DiInstallDriverW(IntPtr.Zero, path, flags, out needReboot);
            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return showUI || needReboot;
        }

        public static bool UpdateDriverForPlugAndPlayDevices(string hardwareId, string path, bool showUI, bool force)
        {
            INSTALLFLAG flags = 0;
            if (force)
            {
                flags |= INSTALLFLAG.INSTALLFLAG_FORCE;
            }
            if (!showUI)
            {
                flags |= INSTALLFLAG.INSTALLFLAG_NONINTERACTIVE;
            }

            bool needReboot = false;
            bool success = showUI
                ? NativeMethods.UpdateDriverForPlugAndPlayDevicesW(IntPtr.Zero, hardwareId, path, flags, IntPtr.Zero)
                : NativeMethods.UpdateDriverForPlugAndPlayDevicesW(IntPtr.Zero, hardwareId, path, flags, out needReboot);
            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return showUI || needReboot;
        }

        public static bool UninstallDriver(string infPath, bool showUI, bool keepInf)
        {
            uint flags = keepInf ? (uint)1 : 0;
            bool needReboot = false;
            bool success = showUI
                ? NativeMethods.DiUninstallDriverW(IntPtr.Zero, infPath, flags, IntPtr.Zero)
                : NativeMethods.DiUninstallDriverW(IntPtr.Zero, infPath, flags, out needReboot);
            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return showUI || needReboot;
        }
    }
}