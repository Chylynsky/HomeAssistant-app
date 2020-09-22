using HomeAssistant.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    class MainPageViewModel
    {
        public ObservableCollection<DeviceModel> ConnectedDevices { get; }
        public ObservableCollection<DeviceModel> AvailableDevices { get; }
        public Command<string> ConnectDevice { get; }
        public Command<string> SelectDeviceCommand { get; }
        public DeviceModel SelectedDevice { get; private set; }

        public MainPageViewModel()
        {
            ConnectedDevices = new ObservableCollection<DeviceModel>();
            AvailableDevices = new ObservableCollection<DeviceModel>();

            AvailableDevices.Add(new DeviceModel()
            {
                Name = "Rakieta",
                Id = "997",
                IconSource = "C:\\Users\\borch\\Pictures\\Saved Pictures\\Chylinski.jpg"
            });

            AvailableDevices.Add(new DeviceModel()
            {
                Name = "Łopata",
                Id = "1",
                IconSource = "C:\\Users\\borch\\Pictures\\Saved Pictures\\Chylinski.jpg"
            });

            ConnectedDevices.Add(new DeviceModel()
            {
                Name = "Czajnik",
                Id = "2",
                IconSource = "C:\\Users\\borch\\Pictures\\Saved Pictures\\Chylinski.jpg"
            });

            ConnectedDevices.Add(new DeviceModel()
            {
                Name = "Balonix",
                Id = "3",
                IconSource = "C:\\Users\\borch\\Pictures\\Saved Pictures\\Chylinski.jpg"
            });

            ConnectDevice = new Command<string>((string deviceId) => {
                var deviceEnumerator = AvailableDevices.Where((DeviceModel device) => { 
                    return device.Id.Equals(deviceId); 
                });

                ConnectedDevices.Add(deviceEnumerator.First());
            });

            SelectDeviceCommand = new Command<string>((string deviceId) => {
                var deviceEnumerator = ConnectedDevices.Where((DeviceModel device) => {
                    return device.Id.Equals(deviceId);
                });

                SelectedDevice = deviceEnumerator.First();
            });
        }
    }
}
