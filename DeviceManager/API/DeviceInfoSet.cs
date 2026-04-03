using MartinGC94.DeviceManager.Native;
using MartinGC94.DeviceManager.Native.Enums;
using MartinGC94.DeviceManager.Native.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static MartinGC94.DeviceManager.Native.Constants;

namespace MartinGC94.DeviceManager.API
{
    public sealed class DeviceInfoSet : IDisposable
    {
        public Guid? ClassGuid { get; }
        public IntPtr UiWindowHandle { get; set; } = IntPtr.Zero;
        private bool disposedValue;
        internal readonly IntPtr handle;

        private DeviceInfoSet(IntPtr deviceSetHandle, Guid? devClass)
        {
            handle = deviceSetHandle;
            ClassGuid = devClass;
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _ = NativeMethods.SetupDiDestroyDeviceInfoList(handle);
                disposedValue = true;
            }
        }

        ~DeviceInfoSet()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public static DeviceInfoSet CreateDeviceInfoSet(string classGuid, string enumerator, DIGCF flags)
        {
            IntPtr devSet;
            Guid? guid;
            if (classGuid is null)
            {
                devSet = NativeMethods.SetupDiGetClassDevsW(IntPtr.Zero, enumerator, IntPtr.Zero, flags);
                guid = null;
            }
            else
            {
                var guidValue = new Guid(classGuid);
                guid = guidValue;
                devSet = NativeMethods.SetupDiGetClassDevsW(ref guidValue, enumerator, IntPtr.Zero, flags);
            }

            if (devSet == new IntPtr(-1))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return new DeviceInfoSet(devSet, guid);
        }

        public static DeviceInfoSet CreateEmptyDeviceInfoSet(string classGuid)
        {
            IntPtr devSet;
            Guid? guid;
            if (classGuid is null)
            {
                devSet = NativeMethods.SetupDiCreateDeviceInfoList(IntPtr.Zero, IntPtr.Zero);
                guid = null;
            }
            else
            {
                var guidValue = new Guid(classGuid);
                guid = guidValue;
                devSet = NativeMethods.SetupDiCreateDeviceInfoList(ref guidValue, IntPtr.Zero);
            }

            if (devSet == new IntPtr(-1))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return new DeviceInfoSet(devSet, guid);
        }

        public IEnumerable<EnumerationResult<Device>> EnumerateDevices()
        {
            uint setIndex = 0;
            while (true)
            {
                var devInfo = new SP_DEVINFO_DATA();
                devInfo.cbSize = Marshal.SizeOf(devInfo);
                if (!NativeMethods.SetupDiEnumDeviceInfo(handle, setIndex, ref devInfo))
                {
                    int errorId = Marshal.GetLastWin32Error();
                    if (errorId == ERROR_NO_MORE_ITEMS)
                    {
                        break;
                    }

                    yield return new EnumerationResult<Device>(setIndex, new Win32Exception(errorId));
                    break;
                }

                yield return new EnumerationResult<Device>(new Device(this, devInfo));
                setIndex++;
            }
        }

        public void BuildDriverInfoList()
        {
            BuildDriverInfoList(true, null);
        }

        public void BuildDriverInfoList(bool allClassDrivers, Device device)
        {
            SPDIT driverType = allClassDrivers ? SPDIT.SPDIT_CLASSDRIVER : SPDIT.SPDIT_COMPATDRIVER;
            bool result = device is null
                ? NativeMethods.SetupDiBuildDriverInfoList(handle, IntPtr.Zero, driverType)
                : NativeMethods.SetupDiBuildDriverInfoList(handle, ref device.deviceInfoData, driverType);

            if (!result)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public IEnumerable<EnumerationResult<DeviceDriver>> EnumerateDrivers()
        {
            return EnumerateDrivers(true, null);
        }

        public IEnumerable<EnumerationResult<DeviceDriver>> EnumerateDrivers(bool allClassDrivers, Device device)
        {
            SPDIT driverType = allClassDrivers ? SPDIT.SPDIT_CLASSDRIVER : SPDIT.SPDIT_COMPATDRIVER;
            bool fromDevice = device != null;
            uint index = 0;
            if (IntPtr.Size == 8)
            {
                var driverInfo = new SP_DRVINFO_DATA_V2_W_x64();
                int cbSize = Marshal.SizeOf(driverInfo);
                driverInfo.cbSize = cbSize;
                while (true)
                {
                    bool success = fromDevice
                        ? NativeMethods.SetupDiEnumDriverInfoW(handle, ref device.deviceInfoData, driverType, index, ref driverInfo)
                        : NativeMethods.SetupDiEnumDriverInfoW(handle, IntPtr.Zero, driverType, index, ref driverInfo);
                    if (!success)
                    {
                        int errorId = Marshal.GetLastWin32Error();
                        if (errorId == ERROR_NO_MORE_ITEMS)
                        {
                            break;
                        }

                        yield return new EnumerationResult<DeviceDriver>(index, new Win32Exception(errorId));
                        break;
                    }

                    DeviceDriver driver = fromDevice
                        ? new DeviceDriver(driverInfo, device)
                        : new DeviceDriver(driverInfo, this);
                    yield return new EnumerationResult<DeviceDriver>(driver);

                    driverInfo = new SP_DRVINFO_DATA_V2_W_x64()
                    {
                        cbSize = cbSize
                    };
                    index++;
                }
            }
            else
            {
                var driverInfo = new SP_DRVINFO_DATA_V2_W_x86();
                int cbSize = Marshal.SizeOf(driverInfo);
                driverInfo.cbSize = cbSize;
                while (true)
                {
                    bool success = fromDevice
                        ? NativeMethods.SetupDiEnumDriverInfoW(handle, ref device.deviceInfoData, driverType, index, ref driverInfo)
                        : NativeMethods.SetupDiEnumDriverInfoW(handle, IntPtr.Zero, driverType, index, ref driverInfo);
                    if (!success)
                    {
                        int errorId = Marshal.GetLastWin32Error();
                        if (errorId == ERROR_NO_MORE_ITEMS)
                        {
                            break;
                        }

                        yield return new EnumerationResult<DeviceDriver>(index, new Win32Exception(errorId));
                        break;
                    }

                    DeviceDriver driver = fromDevice
                        ? new DeviceDriver(driverInfo, device)
                        : new DeviceDriver(driverInfo, this);
                    yield return new EnumerationResult<DeviceDriver>(driver);

                    driverInfo = new SP_DRVINFO_DATA_V2_W_x86()
                    {
                        cbSize = cbSize
                    };
                    index++;
                }
            }
        }

        public DevInstallParams GetDeviceInstallParams()
        {
            return GetDeviceInstallParams(null);
        }

        public DevInstallParams GetDeviceInstallParams(Device device)
        {
            bool fromDevice = device != null;
            bool success;
            if (IntPtr.Size == 8)
            {
                var installParams = new SP_DEVINSTALL_PARAMS_W_x64();
                installParams.cbSize = Marshal.SizeOf(installParams);
                success = fromDevice
                    ? NativeMethods.SetupDiGetDeviceInstallParamsW(handle, ref device.deviceInfoData, ref installParams)
                    : NativeMethods.SetupDiGetDeviceInstallParamsW(handle, IntPtr.Zero, ref installParams);
                if (success)
                {
                    return new DevInstallParams(installParams);
                }
            }
            else
            {
                var installParams = new SP_DEVINSTALL_PARAMS_W_x86();
                installParams.cbSize = Marshal.SizeOf(installParams);
                success = fromDevice
                    ? NativeMethods.SetupDiGetDeviceInstallParamsW(handle, ref device.deviceInfoData, ref installParams)
                    : NativeMethods.SetupDiGetDeviceInstallParamsW(handle, IntPtr.Zero, ref installParams);
                if (success)
                {
                    return new DevInstallParams(installParams);
                }
            }

            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public void SetDeviceInstallParams(DevInstallParams installParams)
        {
            SetDeviceInstallParams(installParams, null);
        }

        public void SetDeviceInstallParams(DevInstallParams installParams, Device device)
        {
            bool fromDevice = device != null;
            bool success;
            if (IntPtr.Size == 8)
            {
                var newParams = installParams.GetStructX64();
                success = fromDevice
                    ? NativeMethods.SetupDiSetDeviceInstallParamsW(handle, ref device.deviceInfoData, ref newParams)
                    : NativeMethods.SetupDiSetDeviceInstallParamsW(handle, IntPtr.Zero, ref newParams);
                if (success)
                {
                    return;
                }
            }
            else
            {
                var newParams = installParams.GetStructX86();
                success = fromDevice
                    ? NativeMethods.SetupDiSetDeviceInstallParamsW(handle, ref device.deviceInfoData, ref newParams)
                    : NativeMethods.SetupDiSetDeviceInstallParamsW(handle, IntPtr.Zero, ref newParams);
                if (success)
                {
                    return;
                }
            }

            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}