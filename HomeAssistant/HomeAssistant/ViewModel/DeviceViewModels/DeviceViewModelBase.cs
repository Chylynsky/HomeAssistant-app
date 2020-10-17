using HomeAssistant.Helper;
using HomeAssistant.Model;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel.DeviceViewModels
{
    abstract class DeviceViewModelBase : IActionViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected DeviceModelBase deviceModel;

        public string Title
        {
            get
            {
                return deviceModel.Name;
            }
        }

        public string Id
        {
            get
            {
                return deviceModel.Id;
            }
        }

        public DeviceViewModelBase(DeviceModelBase deviceModel)
        {
            this.deviceModel = deviceModel;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
