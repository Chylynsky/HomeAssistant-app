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
        public static IDeviceModel GetDeviceModelForTypeName(string deviceTypeName)
        {
            string assemblyName = Assembly.GetCallingAssembly().FullName;
            string fullTypeName = string.Format("HomeAssistant.Model.{0}Model", deviceTypeName);
            Type deviceModelType = Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", fullTypeName, assemblyName));

            if (deviceModelType == null)
            {
                throw new Exception(string.Format("Type {0} does not exist in assembly {1}.", fullTypeName, assemblyName));
            }

            return Activator.CreateInstance(deviceModelType) as IDeviceModel;
        }

        public static IDeviceViewModel GetDeviceViewModelForModel(Type deviceModel)
        {
            string assemblyName = deviceModel.GetTypeInfo().Assembly.FullName;
            string viewModelName = deviceModel.FullName.Replace("Model", "ViewModel");

            Type deviceViewModelType = Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewModelName, assemblyName));

            if (deviceViewModelType == null)
            {
                throw new Exception(string.Format("Type {0} does not exist in assembly {1}.", viewModelName, assemblyName));
            }

            return Activator.CreateInstance(deviceViewModelType) as IDeviceViewModel;
        }

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
