using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MartinGC94.DeviceManager.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PROPERTYKEY
    {
        public Guid fmtid;
        public uint pid;

        public PROPERTYKEY(Guid InputId, uint InputPid)
        {
            fmtid = InputId;
            pid = InputPid;
        }

        public PROPERTYKEY(string id)
        {
            // Expected input looks something like: {3AB22E31-8264-4B4E-9AF5-A8D2D8E33E62}[10]
            Match matchInfo = Regex.Match(id, "^{([0-9|A-F]{8}-[0-9|A-F]{4}-[0-9|A-F]{4}-[0-9|A-F]{4}-[0-9|A-F]{12})}\\[(\\d+)]$", RegexOptions.IgnoreCase);
            if (!matchInfo.Success)
            {
                throw new ArgumentException($"The string '{id}' is not a valid ID string.");
            }

            fmtid = new Guid(matchInfo.Groups[1].Value);
            pid = uint.Parse(matchInfo.Groups[2].Value);
        }

        public override string ToString()
        {
            return $"{{{fmtid}}}[{pid}]";
        }
    }
}