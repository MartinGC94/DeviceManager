using MartinGC94.DeviceManager.Native;
using System;
using System.Management.Automation;
using System.Text.RegularExpressions;

namespace MartinGC94.DeviceManager.API.ParamAttributes
{
    internal class DeviceClassTransformer : ArgumentTransformationAttribute
    {
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {
            string inputAsString = LanguagePrimitives.ConvertTo<string>(inputData);
            string cleanInput = inputAsString.Trim('{', '}').Replace("-", string.Empty);
            if (Regex.IsMatch(cleanInput, "[a-f|0-9]{32}"))
            {
                return cleanInput;
            }

            var foundGuids = new Guid[1];
            if (NativeMethods.SetupDiClassGuidsFromNameW(inputAsString, foundGuids, 1, out int numberOfGuids) && numberOfGuids == 1)
            {
                return foundGuids[0].ToString().Trim('{', '}').Replace("-", string.Empty);
            }

            throw new ArgumentException($"'{inputAsString}' is not a valid device class name or ID.");
        }
    }
}