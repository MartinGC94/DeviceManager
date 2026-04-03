using MartinGC94.DeviceManager.API;
using MartinGC94.DeviceManager.API.ParamAttributes;
using MartinGC94.DeviceManager.Native.Enums;
using System.ComponentModel;
using System.Management.Automation;

namespace MartinGC94.DeviceManager.Commands
{
    [OutputType(typeof(Device))]
    [Cmdlet(VerbsCommon.Get, "Device", DefaultParameterSetName = "GetByEnumerator")]
    public sealed class GetDeviceCommand : Cmdlet
    {
        [Parameter(ParameterSetName = "GetByEnumerator")]
        [ArgumentCompleter(typeof(DeviceClassCompleter))]
        [DeviceClassTransformer]
        [Alias("Class")]
        public string DeviceClass { get; set; }

        [Parameter(ParameterSetName = "GetByEnumerator")]
        [ArgumentCompleter(typeof(EnumeratorIdCompleter))]
        public string EnumeratorId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "GetByDeviceInstance")]
        public string DeviceInstanceId { get; set; }

        [Parameter(ParameterSetName = "GetByDeviceInstance")]
        [Parameter(ParameterSetName = "GetByEnumerator")]
        public SwitchParameter IncludeNonPresent { get; set; }

        protected override void ProcessRecord()
        {
            DeviceInfoSet deviceInfoSet;
            bool searchByDeviceInstance = !string.IsNullOrEmpty(DeviceInstanceId);
            string enumeratorString = searchByDeviceInstance ? DeviceInstanceId : EnumeratorId;
            DIGCF flags = 0;
            if (!IncludeNonPresent)
            {
                flags |= DIGCF.DIGCF_PRESENT;
            }
            if (searchByDeviceInstance)
            {
                flags |= DIGCF.DIGCF_DEVICEINTERFACE;
            }
            if (string.IsNullOrEmpty(DeviceClass))
            {
                flags |= DIGCF.DIGCF_ALLCLASSES;
            }
            try
            {
                deviceInfoSet = DeviceInfoSet.CreateDeviceInfoSet(DeviceClass, enumeratorString, flags);
            }
            catch (Win32Exception e)
            {
                ThrowTerminatingError(e.NewErrorRecord("CreateDeviceInfoSetError"));
                return;
            }

            foreach (EnumerationResult<Device> result in deviceInfoSet.EnumerateDevices())
            {
                if (result.Success)
                {
                    WriteObject(result.item);
                }
                else
                {
                    WriteError(result.exception.NewErrorRecord("EnumerationError", result.itemIdentifier));
                }
            }
        }
    }
}