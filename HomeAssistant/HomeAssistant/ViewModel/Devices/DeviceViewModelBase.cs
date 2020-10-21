using HomeAssistant.Model.Devices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAssistant.ViewModel.Devices
{
    public abstract class DeviceViewModelBase : IDeviceViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IDeviceModel DeviceModel { get; private set; }

        public string Name
        {
            get
            {
                return DeviceModel.Name;
            }
        }

        public string Id
        {
            get
            {
                return DeviceModel.Id;
            }
        }

        public DeviceViewModelBase(IDeviceModel deviceModel)
        {
            DeviceModel = deviceModel;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void UpdateData();
    }
}
