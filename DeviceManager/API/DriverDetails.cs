using MartinGC94.DeviceManager.Native.Structs;
using System;

namespace MartinGC94.DeviceManager.API
{
    public sealed class DriverDetails
    {
        public DateTime InfDate { get; }
        public string SectionName { get; }
        public string InfFullName { get; }
        public string DriverDescription { get; }
        public string HardwareId { get; internal set; }
        public string[] CompatibleIds { get; internal set; }

        internal DriverDetails(SP_DRVINFO_DETAIL_DATA_W_x64 structData)
        {
            InfDate = structData.InfDate.ToDateTimeUtc();
            SectionName = structData.SectionName;
            InfFullName = structData.InfFileName;
            DriverDescription = structData.DrvDescription;
        }

        internal DriverDetails(SP_DRVINFO_DETAIL_DATA_W_x86 structData)
        {
            InfDate = structData.InfDate.ToDateTimeUtc();
            SectionName = structData.SectionName;
            InfFullName = structData.InfFileName;
            DriverDescription = structData.DrvDescription;
        }
    }
}