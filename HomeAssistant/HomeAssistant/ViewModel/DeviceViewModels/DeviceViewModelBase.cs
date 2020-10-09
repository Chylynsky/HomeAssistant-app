using HomeAssistant.Helper;
using HomeAssistant.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel.DeviceViewModels
{
    abstract class DeviceViewModelBase : INotifyPropertyChanged
    {
        public static HomeAssistantClient HttpClient { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected DeviceModelBase deviceModel;

        public string Name
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

        public ImageSource IconSource
        {
            get
            {
                return deviceModel.IconSource;
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
