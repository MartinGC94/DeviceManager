using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using static MartinGC94.DeviceManager.Native.Constants;

namespace MartinGC94.DeviceManager
{
    internal static class Util
    {
        private static readonly char[] badChars = new char[]
        {
            '$',
            '`', // Backtick
            ' ', // Space
            '"',
            '\'',
            ',',
            ';',
            '(',
            ')',
            '{',
            '}',
            '<',
            '>',
            '#',
            '&',
            '|',
            '-',
            (char)0x2013, // EnDash
            (char)0x2014, // EmDash
            (char)0x2015, // HorizontalBar
        };

        public static string QuoteIfNeededForCompletion(this string completionText)
        {
            return completionText.IndexOfAny(badChars) == -1
                ? completionText
                : $"'{completionText.Replace("\'", "\'\'")}'";
        }

        public static ErrorCategory GetErrorCategory(this Exception exception)
        {
            switch (exception)
            {
                case Win32Exception nativeError:
                    switch (nativeError.NativeErrorCode)
                    {
                        case 2:
                            return ErrorCategory.ObjectNotFound;

                        case 5:
                        case 1314:
                            return ErrorCategory.PermissionDenied;

                        case 6:
                        case 50:
                            return ErrorCategory.InvalidOperation;

                        case 87:
                        case 1610:
                            return ErrorCategory.InvalidArgument;

                        case 31:
                            return ErrorCategory.DeviceError;

                        default:
                            return ErrorCategory.NotSpecified;
                    }

                case ArgumentNullException _:
                case ArgumentException _:
                    return ErrorCategory.InvalidArgument;

                case ItemNotFoundException _:
                    return ErrorCategory.ObjectNotFound;

                case IOException _:
                case ObjectDisposedException _:
                    return ErrorCategory.ResourceUnavailable;

                case InvalidOperationException _:
                    return ErrorCategory.InvalidOperation;

                case UnauthorizedAccessException _:
                case SecurityException _:
                    return ErrorCategory.PermissionDenied;

                case InvalidCastException _:
                    return ErrorCategory.InvalidType;

                default:
                    return ErrorCategory.NotSpecified;
            }
        }

        /// <summary>
        /// Creates a known error record for known errors with a fallback to a generic error record for anything else.
        /// </summary>
        public static ErrorRecord NewErrorRecord(this Exception exception, string errorId, object targetObject = null)
        {
            ErrorRecord output;
            Exception e;
            if (exception is Win32Exception nativeException)
            {
                switch (nativeException.NativeErrorCode)
                {
                    case ERROR_ACCESS_DENIED:
                        output = new ErrorRecord(nativeException, errorId, ErrorCategory.PermissionDenied, targetObject);
                        output.SetRecommendedAction("Elevate the process and try again.");
                        return output;

                    case ERROR_IN_WOW64:
                        e = new InvalidOperationException("This operation cannot be done from a 32-bit process on a 64-bit system.", nativeException);
                        output = new ErrorRecord(e, errorId, ErrorCategory.InvalidOperation, targetObject);
                        output.SetRecommendedAction("Launch a 64-bit instance of PowerShell.");
                        return output;

                    case ERROR_NOT_AN_INSTALLED_OEM_INF:
                        e = new InvalidOperationException("Inbox drivers cannot be removed from the system.", nativeException);
                        output = new ErrorRecord(e, errorId, ErrorCategory.InvalidOperation, targetObject);
                        return output;

                    case ERROR_NO_DRIVER_SELECTED:
                        e = new ItemNotFoundException("Found no drivers to install.", nativeException);
                        output = new ErrorRecord(e, errorId, ErrorCategory.ObjectNotFound, targetObject);
                        output.SetRecommendedAction("Specify a driver with the -Driver parameter.");
                        return output;

                    default:
                        break;
                }
            }

            return new ErrorRecord(exception, errorId, exception.GetErrorCategory(), targetObject);
        }

        public static void SetRecommendedAction(this ErrorRecord record, string action)
        {
            if (record.ErrorDetails is null)
            {
                record.ErrorDetails = new ErrorDetails(string.Empty);
            }

            record.ErrorDetails.RecommendedAction = action;
        }

        public static IEnumerable<string> ResolveInputPaths(this PSCmdlet cmdlet, string[] paths, bool expandWildcards)
        {
            foreach (string pathItem in paths)
            {
                if (expandWildcards)
                {
                    Collection<string> resolvedPaths;
                    ProviderInfo provider;
                    try
                    {
                        resolvedPaths = cmdlet.GetResolvedProviderPathFromPSPath(pathItem, out provider);
                    }
                    catch (Exception e) when (!(e is PipelineStoppedException))
                    {
                        cmdlet.WriteError(e.NewErrorRecord("PathExpandError", pathItem));
                        continue;
                    }

                    if (!"FileSystem".Equals(provider.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        var e = new ArgumentException($"{pathItem} did not resolve to a filesystem path.");
                        ErrorRecord record = e.NewErrorRecord("InvalidPath", pathItem);
                        record.SetRecommendedAction("Change the path to a filesystem path like C:\\");
                        cmdlet.WriteError(record);
                        continue;
                    }

                    for (int i = 0; i < resolvedPaths.Count; i++)
                    {
                        yield return resolvedPaths[i];
                    }
                }
                else
                {
                    string resolvedPath;
                    try
                    {
                        resolvedPath = cmdlet.GetUnresolvedProviderPathFromPSPath(pathItem);
                    }
                    catch (Exception e) when (!(e is PipelineStoppedException))
                    {
                        cmdlet.WriteError(e.NewErrorRecord("PathResolveError", pathItem));
                        continue;
                    }

                    yield return resolvedPath;
                }
            }
        }

        public static DateTime ToDateTimeUtc(this FILETIME fileTime)
        {
            long value = ((long)fileTime.dwHighDateTime << 32) | (uint)fileTime.dwLowDateTime;
            return DateTime.FromFileTimeUtc(value);
        }

        public static FILETIME ToFileTime2(this DateTime dateTime)
        {
            long value = dateTime.ToFileTimeUtc();
            return new FILETIME
            {
                dwLowDateTime = (int)(value & 0xFFFFFFFF),
                dwHighDateTime = (int)(value >> 32)
            };
        }

        public static ulong ToUlong(this Version version)
        {
            ulong major = (ushort)version.Major;
            ulong minor = (ushort)version.Minor;
            ulong build = (ushort)version.Build;
            ulong revision = (ushort)version.Revision;

            return (major << 48) |
                   (minor << 32) |
                   (build << 16) |
                   revision;
        }

        public static Version ToVersion(this ulong version)
        {
            ushort major = (ushort)((version >> 48) & 0xFFFF);
            ushort minor = (ushort)((version >> 32) & 0xFFFF);
            ushort build = (ushort)((version >> 16) & 0xFFFF);
            ushort revision = (ushort)(version & 0xFFFF);

            return new Version(major, minor, build, revision);
        }
    }
}