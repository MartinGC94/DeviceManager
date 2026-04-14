using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Management.Automation;

namespace MartinGC94.DeviceManager.API
{
    public sealed class DeviceAdapter : PSPropertyAdapter
    {
        /// <summary>
        /// We skip native Device properties + Properties that PowerShell might add
        /// </summary>
        private static readonly HashSet<string> propertiesToSkip = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            nameof(Device.Name),
            nameof(Device.DeviceType),
            nameof(Device.Manufacturer),
            nameof(Device.Location),
            nameof(Device.DevicePath),
            nameof(Device.InfPath),
            nameof(Device.DeviceProperties),
            nameof(Device.IsPresent),
            nameof(Device.HasProblem),
            nameof(Device.ProblemCode),
            nameof(Device.DevNodeStatus),
            nameof(Device.NTStatus),
            nameof(Device.ConfigFlags),
            "PSComputerName",
            "RunspaceId",
            "PSShowComputerName",
            "PSSourceJobInstanceId",
            "PSEventObject"
        };

        public override Collection<PSAdaptedProperty> GetProperties(object baseObject)
        {
            if (!(baseObject is Device device))
            {
                throw new PSInvalidOperationException($"Cannot convert baseObject to an object of type {typeof(Device)}");
            }

            var properties = device.DeviceProperties;
            var result = new Collection<PSAdaptedProperty>();
            foreach (var item in properties)
            {
                string propertyName = item.Name;
                if (propertyName.Equals("Unknown") || propertiesToSkip.Contains(propertyName))
                {
                    continue;
                }

                var itemToAdd = new PSAdaptedProperty(propertyName, item);
                result.Add(itemToAdd);
            }

            return result;
        }

        public override PSAdaptedProperty GetProperty(object baseObject, string propertyName)
        {
            if (propertyName is null)
            {
                throw new PSArgumentNullException(nameof(propertyName));
            }

            if (!(baseObject is Device device))
            {
                throw new PSInvalidOperationException($"Cannot convert baseObject to an object of type {typeof(Device)}");
            }

            if (propertiesToSkip.Contains(propertyName))
            {
                return null;
            }

            DeviceProperty devProp;

            try
            {
                devProp = device.GetDevicePropertyByName(propertyName);
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (Win32Exception)
            {
                return null;
            }

            return new PSAdaptedProperty(propertyName, devProp);
        }

        public override string GetPropertyTypeName(PSAdaptedProperty adaptedProperty)
        {
            if (adaptedProperty is null)
            {
                throw new ArgumentNullException(nameof(adaptedProperty));
            }

            if (adaptedProperty.Tag is DeviceProperty devProperty)
            {
                return devProperty.Data.GetType().FullName;
            }

            throw new ArgumentException("Invalid argument", nameof(adaptedProperty));
        }

        public override object GetPropertyValue(PSAdaptedProperty adaptedProperty)
        {
            if (adaptedProperty is null)
            {
                throw new ArgumentNullException(nameof(adaptedProperty));
            }

            if (adaptedProperty.Tag is DeviceProperty devProperty)
            {
                return devProperty.Data;
            }

            throw new ArgumentException("Invalid argument", nameof(adaptedProperty));
        }

        public override bool IsGettable(PSAdaptedProperty adaptedProperty)
        {
            return true;
        }

        public override bool IsSettable(PSAdaptedProperty adaptedProperty)
        {
            return false;
        }

        public override void SetPropertyValue(PSAdaptedProperty adaptedProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}