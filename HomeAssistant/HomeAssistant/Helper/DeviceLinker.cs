using HomeAssistant.Model.Devices;
using HomeAssistant.ViewModel.Devices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace HomeAssistant.Helper
{
    static internal class DeviceLinker
    {
        /// <summary>
        /// Get object of a Model class being the implementation of IDeviceModel interface. Target type is
        /// deduced based on the deviceTypeName parameter.
        /// </summary>
        /// <param name="deviceTypeName">Name of the type.</param>
        /// <returns>Object of the Model corresponding to the given name.</returns>
        public static IDeviceModel GetDeviceModelForTypeName(string deviceTypeName)
        {
            string assemblyName = Assembly.GetCallingAssembly().FullName;
            string fullTypeName = string.Format("HomeAssistant.Model.Devices.{0}Model", deviceTypeName);
            Type deviceModelType = Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", fullTypeName, assemblyName));

            if (deviceModelType == null)
            {
                throw new Exception(string.Format("Type {0} does not exist in assembly {1}.", fullTypeName, assemblyName));
            }

            return Activator.CreateInstance(deviceModelType) as IDeviceModel;
        }

        /// <summary>
        /// Get ViewModel object corresponding to the given Model.
        /// </summary>
        /// <param name="deviceModel">Model for which the ViewModel is to be created.</param>
        /// <returns>ViewModel object being an implementation of the IDeviceViewModel interface.</returns>
        public static IDeviceViewModel GetDeviceViewModelForModel(IDeviceModel deviceModel)
        {
            string assemblyName = deviceModel.GetType().GetTypeInfo().Assembly.FullName;
            string viewModelName = deviceModel.GetType().FullName.Replace("Model", "ViewModel");

            Type deviceViewModelType = Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewModelName, assemblyName));

            if (deviceViewModelType == null)
            {
                throw new Exception(string.Format("Type {0} does not exist in assembly {1}.", viewModelName, assemblyName));
            }

            return Activator.CreateInstance(deviceViewModelType, deviceModel) as IDeviceViewModel;
        }

        /// <summary>
        /// Get View corresponding to the given ViewModel.
        /// </summary>
        /// <param name="deviceViewModel">Type of the ViewModel.</param>
        /// <returns>ContentView object being the View for the specified ViewModel.</returns>
        public static ContentView GetDeviceViewForViewModel(Type deviceViewModel)
        {
            string assemblyName = deviceViewModel.GetTypeInfo().Assembly.FullName;
            string viewName = deviceViewModel.FullName.Replace("Model", string.Empty);

            Type deviceViewType = Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, assemblyName));

            if (deviceViewType == null)
            {
                throw new Exception(string.Format("Type {0} does not exist in assembly {1}.", viewName, assemblyName));
            }

            return Activator.CreateInstance(deviceViewType) as ContentView;
        }
    }
}
