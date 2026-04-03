using MartinGC94.DeviceManager.Native;
using MartinGC94.DeviceManager.Native.Enums;
using MartinGC94.DeviceManager.Native.Structs;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using static MartinGC94.DeviceManager.Native.Constants;

namespace MartinGC94.DeviceManager.API
{
    public sealed class Device
    {
        public string Name
        {
            get
            {
                if (_name is null)
                {
                    _name = GetPropertyValueAsString(new PROPERTYKEY(new Guid("b725f130-47ef-101a-a5f1-02608c9eebac"), 10));
                }

                return _name;
            }
        }
        public string DeviceType
        {
            get
            {
                if (_deviceType is null)
                {
                    _deviceType = GetPropertyValueAsString(new PROPERTYKEY(new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"), 9));
                }

                return _deviceType;
            }
        }
        public string Manufacturer
        {
            get
            {
                if (_manufacturer is null)
                {
                    _manufacturer = GetPropertyValueAsString(new PROPERTYKEY(new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"), 13));
                }

                return _manufacturer;
            }
        }
        public string Location
        {
            get
            {
                if (_location is null)
                {
                    _location = GetPropertyValueAsString(new PROPERTYKEY(new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"), 15));
                }

                return _location;
            }
        }
        public string DevicePath
        {
            get
            {
                if (_devicePath is null)
                {
                    _devicePath = GetPropertyValueAsString(new PROPERTYKEY(new Guid("78c34fc8-104a-4aca-9ea4-524d52996e57"), 256));
                }

                return _devicePath;
            }
        }
        public string InfPath => GetPropertyValueAsString(new PROPERTYKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"), 5));
        public DeviceProperty[] DeviceProperties
        {
            get
            {
                if (_deviceProperties is null)
                {
                    _deviceProperties = GetDeviceProperties();
                }

                return _deviceProperties;
            }
        }
        public bool IsPresent => GetIsPresent();
        public bool HasProblem => GetHasProblem();
        public uint ProblemCode => GetPropertyValueAsUint(new PROPERTYKEY(new Guid("4340a6c5-93fa-4706-972c-7b648008a5a7"), 3));
        public DevInstanceStatusFlags DevNodeStatus => (DevInstanceStatusFlags)GetPropertyValueAsUint(new PROPERTYKEY(new Guid("4340a6c5-93fa-4706-972c-7b648008a5a7"), 2));
        public uint NTStatus => GetPropertyValueAsUint(new PROPERTYKEY(new Guid("4340a6c5-93fa-4706-972c-7b648008a5a7"), 12));
        public uint ConfigFlags => GetPropertyValueAsUint(new PROPERTYKEY(new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"), 12));
        private string _name;
        private string _deviceType;
        private string _manufacturer;
        private string _location;
        private string _devicePath;
        private DeviceProperty[] _deviceProperties;
        internal DeviceInfoSet deviceInfoSet;
        internal SP_DEVINFO_DATA deviceInfoData;

        internal Device(DeviceInfoSet infoSet, SP_DEVINFO_DATA devInfo)
        {
            deviceInfoSet = infoSet;
            deviceInfoData = devInfo;
        }

        public override string ToString()
        {
            return $"{Name} ({DevicePath})";
        }

        public string GetRollbackTarget()
        {
            return GetPropertyValueAsString(new PROPERTYKEY(new Guid("83da6326-97a6-4088-9453-a1923f573b29"), 4));
        }

        private DeviceProperty[] GetDeviceProperties()
        {
            uint bufferSize = 128;
            var foundKeys = new PROPERTYKEY[bufferSize];
            if (!NativeMethods.SetupDiGetDevicePropertyKeys(deviceInfoSet.handle, ref deviceInfoData, foundKeys, bufferSize, out uint keyCount, 0))
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode == ERROR_INSUFFICIENT_BUFFER)
                {
                    foundKeys = new PROPERTYKEY[keyCount];
                    if (!NativeMethods.SetupDiGetDevicePropertyKeys(deviceInfoSet.handle, ref deviceInfoData, foundKeys, keyCount, out keyCount, 0))
                    {
                        return new DeviceProperty[0];
                    }
                }
                else
                {
                    return new DeviceProperty[0];
                }
            }

            var result = new DeviceProperty[keyCount];
            var dataBuffer = new byte[512];
            for (uint i = 0; i < keyCount; i++)
            {
                if (!NativeMethods.SetupDiGetDevicePropertyW(deviceInfoSet.handle, ref deviceInfoData, ref foundKeys[i], out DEVPROPTYPE propType, dataBuffer, dataBuffer.Length, out int actualSize, 0))
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    if (errorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        dataBuffer = new byte[actualSize];
                        if (!NativeMethods.SetupDiGetDevicePropertyW(deviceInfoSet.handle, ref deviceInfoData, ref foundKeys[i], out propType, dataBuffer, actualSize, out actualSize, 0))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                object data = ProcessPropertyData(propType, dataBuffer, actualSize, foundKeys[i]);
                result[i] = new DeviceProperty(foundKeys[i], propType, data);
            }

            return result;
        }

        private bool GetHasProblem()
        {
            object value = GetPropertyValue(new PROPERTYKEY(new Guid("540b947e-8b40-45bc-a8a2-6a0b894cbda2"), 6));
            if (value is bool valAsBool)
            {
                return valAsBool;
            }

            return true;
        }

        private bool GetIsPresent()
        {
            object value = GetPropertyValue(new PROPERTYKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"), 5));
            if (value is bool valAsBool)
            {
                return valAsBool;
            }

            return false;
        }

        private string GetPropertyValueAsString(PROPERTYKEY property)
        {
            object result = GetPropertyValue(property);
            return result is null ? string.Empty : result.ToString();
        }

        private uint GetPropertyValueAsUint(PROPERTYKEY property)
        {
            object result = GetPropertyValue(property);
            if (result is uint resAsUint)
            {
                return resAsUint;
            }

            return uint.MinValue;
        }

        private object GetPropertyValue(PROPERTYKEY property)
        {
            int bufferSize = 512;
            var outputBuffer = new byte[bufferSize];
            if (!NativeMethods.SetupDiGetDevicePropertyW(deviceInfoSet.handle, ref deviceInfoData, ref property, out DEVPROPTYPE propType, outputBuffer, bufferSize, out int actualSize, 0))
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode == ERROR_INSUFFICIENT_BUFFER)
                {
                    outputBuffer = new byte[actualSize];
                    if (!NativeMethods.SetupDiGetDevicePropertyW(deviceInfoSet.handle, ref deviceInfoData, ref property, out propType, outputBuffer, actualSize, out actualSize, 0))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            object result = ProcessPropertyData(propType, outputBuffer, actualSize, property);
            return result;
        }

        private object ProcessPropertyData(DEVPROPTYPE type, byte[] data, int dataSize, PROPERTYKEY propKey)
        {
            if (propKey.ToString().Equals("{a45c254e-df1c-4efd-8020-67d146a850e0}[32]"))
            {
                return CM_POWER_DATA.FromByteArray(data);
            }

            switch (type)
            {
                case DEVPROPTYPE.INT16:
                    return BitConverter.ToInt16(data, 0);

                case DEVPROPTYPE.UINT16:
                    return BitConverter.ToUInt16(data, 0);

                case DEVPROPTYPE.INT32:
                    return BitConverter.ToInt32(data, 0);

                case DEVPROPTYPE.UINT32:
                case DEVPROPTYPE.NTSTATUS:
                case DEVPROPTYPE.ERROR:
                    return BitConverter.ToUInt32(data, 0);

                case DEVPROPTYPE.INT64:
                    return BitConverter.ToInt64(data, 0);

                case DEVPROPTYPE.UINT64:
                    return BitConverter.ToUInt64(data, 0);

                case DEVPROPTYPE.DOUBLE:
                    return BitConverter.ToDouble(data, 0);

                case DEVPROPTYPE.GUID:
                    var guidData = new byte[dataSize];
                    Array.Copy(data, guidData, dataSize);
                    return new Guid(guidData);

                case DEVPROPTYPE.FILETIME:
                    return DateTime.FromFileTime(BitConverter.ToInt64(data, 0));

                case DEVPROPTYPE.BOOLEAN:
                    return BitConverter.ToBoolean(data, 0);

                case DEVPROPTYPE.STRING_LIST:
                    // Multistrings should end with 2 null characters but the standard is not always followed for empty strings.
                    // If there's less than 4 bytes (2 per char) then we assume it's just an empty string.
                    if (dataSize < 4)
                    {
                        return new string[] { string.Empty };
                    }
                    
                    return Encoding.Unicode.GetString(data, 0, (dataSize - 4)).Split('\0');

                case DEVPROPTYPE.STRING:
                case DEVPROPTYPE.SECURITY_DESCRIPTOR_STRING:
                case DEVPROPTYPE.STRING_INDIRECT:
                    // strings end with a null character that we skip, hence the -2 (Unicode = 2 bytes per char)
                    return Encoding.Unicode.GetString(data, 0, (dataSize - 2));

                default:
                    var result = new byte[dataSize];
                    Array.Copy(data, result, dataSize);
                    return result;
            }
        }
    
        public bool RollbackDriver(bool showUI)
        {
            bool needReboot = false;
            bool success = showUI
                ? NativeMethods.DiRollbackDriver(deviceInfoSet.handle, ref deviceInfoData, deviceInfoSet.UiWindowHandle, 0, IntPtr.Zero)
                : NativeMethods.DiRollbackDriver(deviceInfoSet.handle, ref deviceInfoData, deviceInfoSet.UiWindowHandle, 1, ref needReboot);
            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return showUI || needReboot;
        }

        public bool RemoveDevice(bool showUI)
        {
            bool needReboot = false;
            bool success = showUI
                ? NativeMethods.DiUninstallDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, 0, IntPtr.Zero)
                : NativeMethods.DiUninstallDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, 0, out needReboot);
            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return showUI || needReboot;
        }

        public void SetDeviceState(bool enabled)
        {
            DICS_State state = enabled ? DICS_State.DICS_ENABLE : DICS_State.DICS_DISABLE;
            var propChangeParams = new SP_PROPCHANGE_PARAMS()
            {
                HwProfile = 0,
                Scope = DICS_FLAG_GLOBAL,
                StateChange = state
            };
            propChangeParams.ClassInstallHeader.cbSize = Marshal.SizeOf(propChangeParams.ClassInstallHeader);
            propChangeParams.ClassInstallHeader.InstallFunction = DIF.DIF_PROPERTYCHANGE;
            int structSize = Marshal.SizeOf(propChangeParams);

            if (!NativeMethods.SetupDiSetClassInstallParamsW(deviceInfoSet.handle, ref deviceInfoData, ref propChangeParams, structSize))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!NativeMethods.SetupDiCallClassInstaller(DIF.DIF_PROPERTYCHANGE, deviceInfoSet.handle, ref deviceInfoData))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public bool InstallDevice(bool showUI, bool installNullDriver, DeviceDriver driver)
        {
            DIIDFLAG installFlags = showUI ? DIIDFLAG.DIIDFLAG_SHOWSEARCHUI : DIIDFLAG.DIIDFLAG_NOFINISHINSTALLUI;
            if (installNullDriver)
            {
                installFlags |= DIIDFLAG.DIIDFLAG_INSTALLNULLDRIVER;
            }

            bool installSuccess;
            bool needReboot = false;
            if (driver is null)
            {
                installSuccess = showUI
                    ? NativeMethods.DiInstallDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, IntPtr.Zero, installFlags, IntPtr.Zero)
                    : NativeMethods.DiInstallDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, IntPtr.Zero, installFlags, out needReboot);
            }
            else
            {
                if (IntPtr.Size == 8)
                {
                    var driverStruct = driver.GetDriverInfoStructX64();
                    installSuccess = showUI
                        ? NativeMethods.DiInstallDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, ref driverStruct, installFlags, IntPtr.Zero)
                        : NativeMethods.DiInstallDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, ref driverStruct, installFlags, out needReboot);
                }
                else
                {
                    var driverStruct = driver.GetDriverInfoStructX86();
                    installSuccess = showUI
                        ? NativeMethods.DiInstallDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, ref driverStruct, installFlags, IntPtr.Zero)
                        : NativeMethods.DiInstallDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, ref driverStruct, installFlags, out needReboot);
                }
            }

            if (!installSuccess)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return showUI || needReboot;
        }

        public void LaunchUpdateDeviceWizard()
        {
            if (!NativeMethods.DiShowUpdateDevice(deviceInfoSet.UiWindowHandle, deviceInfoSet.handle, ref deviceInfoData, 0, IntPtr.Zero))
            {
                int error = Marshal.GetLastWin32Error();
                if (error != ERROR_CANCELLED)
                {
                    throw new Win32Exception(error);
                }
            }
        }
    }
}