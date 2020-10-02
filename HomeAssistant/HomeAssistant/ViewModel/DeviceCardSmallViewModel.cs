using HomeAssistant.Model;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    class DeviceCardSmallViewModel
    {
        public Command<string> SelectDeviceCommand { get; set; }

        public DeviceModel DeviceModel { get; private set; }

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

        public ImageSource IconSource
        {
            get
            {
                return DeviceModel.IconSource;
            }
        }

        public DeviceCardSmallViewModel(DeviceModel device)
        {
            DeviceModel = device;
        }
    }
}
