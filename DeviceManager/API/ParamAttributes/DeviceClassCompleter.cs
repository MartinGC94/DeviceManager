using System;
using MartinGC94.DeviceManager.Native;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using static MartinGC94.DeviceManager.Native.Constants;

namespace MartinGC94.DeviceManager.API.ParamAttributes
{
    public sealed class DeviceClassCompleter : IArgumentCompleter
    {
        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            string cleanInput = wordToComplete is null
                ? "*"
                : wordToComplete.Trim('\'', '"') + "*";
            WildcardPattern inputPattern = WildcardPattern.Get(cleanInput, WildcardOptions.IgnoreCase);

            foreach (string key in GetClassNames())
            {
                if (inputPattern.IsMatch(key))
                {
                    yield return new CompletionResult(key, key, CompletionResultType.ParameterValue, key);
                }
            }
        }

        private IEnumerable<string> GetClassNames()
        {
            _ = NativeMethods.SetupDiBuildClassInfoList(0, IntPtr.Zero, 0, out int requiredSize);
            var guids = new Guid[requiredSize];
            if (!NativeMethods.SetupDiBuildClassInfoList(0, guids, requiredSize, out _))
            {
                yield break;
            }

            var classNameBuffer = new char[MAX_CLASS_NAME_LEN];
            for (int i = 0; i < guids.Length; i++)
            {
                Guid id = guids[i];
                if (NativeMethods.SetupDiClassNameFromGuidW(ref id, classNameBuffer, classNameBuffer.Length, out int actualSize))
                {
                    string className = new string(classNameBuffer, 0, actualSize - 1);
                    yield return className;
                    Array.Clear(classNameBuffer, 0, actualSize);
                }
            }
        }
    }
}