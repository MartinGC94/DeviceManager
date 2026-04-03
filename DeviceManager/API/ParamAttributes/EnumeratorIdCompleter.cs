using MartinGC94.DeviceManager.Native;
using MartinGC94.DeviceManager.Native.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using static MartinGC94.DeviceManager.Native.Constants;

namespace MartinGC94.DeviceManager.API.ParamAttributes
{
    internal class EnumeratorIdCompleter : IArgumentCompleter
    {
        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            string cleanInput = wordToComplete is null
                ? "*"
                : wordToComplete.Trim('\'', '"') + "*";
            WildcardPattern inputPattern = WildcardPattern.Get(cleanInput, WildcardOptions.IgnoreCase);

            foreach (string id in EnumerateDeviceEnumerators())
            {
                if (inputPattern.IsMatch(id))
                {
                    yield return new CompletionResult(id.QuoteIfNeededForCompletion(), id, CompletionResultType.ParameterValue, id);
                }
            }
        }

        private IEnumerable<string> EnumerateDeviceEnumerators()
        {
            uint bufferSize = MAX_DEVICE_ID_LEN;
            var buffer = new char[bufferSize];
            uint index = 0;

            while (NativeMethods.CM_Enumerate_EnumeratorsW(index++, buffer, ref bufferSize, 0) == CR_Codes.CR_SUCCESS)
            {
                string output = new string(buffer, 0, (int)bufferSize - 1);
                yield return output;

                Array.Clear(buffer, 0, (int)bufferSize);
                bufferSize = MAX_DEVICE_ID_LEN;
            }
        }
    }
}