using MartinGC94.DeviceManager.Native.Enums;
using MartinGC94.DeviceManager.Native.Structs;
using System;
using System.Runtime.InteropServices;

namespace MartinGC94.DeviceManager.API
{
    public sealed class DevInstallParams
    {
        public DIFlags Flags { get; set; }
        public DIFlagsEx FlagsEx { get; set; }
        public string DriverPath { get; set; }
        private readonly IntPtr hwndParent;
        private readonly IntPtr installMsgHandler;
        private readonly IntPtr installMsgHandlerContext;
        private readonly IntPtr fileQueue;
        private readonly UIntPtr classInstallReserved;
        private readonly uint reserved;

        internal DevInstallParams(SP_DEVINSTALL_PARAMS_W_x64 structData)
        {
            Flags = structData.Flags;
            FlagsEx = structData.FlagsEx;
            DriverPath = structData.DriverPath;
            hwndParent = structData.hwndParent;
            installMsgHandler = structData.InstallMsgHandler;
            installMsgHandlerContext = structData.InstallMsgHandlerContext;
            fileQueue = structData.FileQueue;
            classInstallReserved = structData.ClassInstallReserved;
            reserved = structData.Reserved;
        }

        internal DevInstallParams(SP_DEVINSTALL_PARAMS_W_x86 structData)
        {
            Flags = structData.Flags;
            FlagsEx = structData.FlagsEx;
            DriverPath = structData.DriverPath;
            hwndParent = structData.hwndParent;
            installMsgHandler = structData.InstallMsgHandler;
            installMsgHandlerContext = structData.InstallMsgHandlerContext;
            fileQueue = structData.FileQueue;
            classInstallReserved = structData.ClassInstallReserved;
            reserved = structData.Reserved;
        }

        internal SP_DEVINSTALL_PARAMS_W_x64 GetStructX64()
        {
            var result = new SP_DEVINSTALL_PARAMS_W_x64()
            {
                ClassInstallReserved = classInstallReserved,
                DriverPath = DriverPath,
                FileQueue = fileQueue,
                Flags = Flags,
                FlagsEx = FlagsEx,
                hwndParent = hwndParent,
                InstallMsgHandler = installMsgHandler,
                InstallMsgHandlerContext = installMsgHandlerContext,
                Reserved = reserved
            };

            result.cbSize = Marshal.SizeOf(result);

            return result;
        }

        internal SP_DEVINSTALL_PARAMS_W_x86 GetStructX86()
        {
            var result = new SP_DEVINSTALL_PARAMS_W_x86()
            {
                ClassInstallReserved = classInstallReserved,
                DriverPath = DriverPath,
                FileQueue = fileQueue,
                Flags = Flags,
                FlagsEx = FlagsEx,
                hwndParent = hwndParent,
                InstallMsgHandler = installMsgHandler,
                InstallMsgHandlerContext = installMsgHandlerContext,
                Reserved = reserved
            };

            result.cbSize = Marshal.SizeOf(result);

            return result;
        }
    }
}