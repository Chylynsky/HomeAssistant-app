using HomeAssistant.Model;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    class DeviceCardSmallViewModel
    {
        public Command<string> SelectDeviceCommand { get; set; }

        public DeviceModelBase DeviceModel { get; private set; }

        public string Id
        {
            get
            {
                return DeviceModel.Id;
            }
        }

        public string Name
        {
            get
            {
                return DeviceModel.Name;
            }
        }

        public DeviceCardSmallViewModel(DeviceModelBase device)
        {
            DeviceModel = device;
        }
    }
}
