using MartinGC94.DeviceManager.Native.Enums;
using MartinGC94.DeviceManager.Native.Structs;

namespace MartinGC94.DeviceManager.API
{
    public sealed class DriverInstallParams
    {
        public uint DriverRank { get; }
        public DNFFlags Flags { get; }

        internal DriverInstallParams(SP_DRVINSTALL_PARAMS_x64 structData)
        {
            DriverRank = structData.Rank;
            Flags = structData.Flags;
        }

        internal DriverInstallParams(SP_DRVINSTALL_PARAMS_x86 structData)
        {
            DriverRank = structData.Rank;
            Flags = structData.Flags;
        }
    }
}