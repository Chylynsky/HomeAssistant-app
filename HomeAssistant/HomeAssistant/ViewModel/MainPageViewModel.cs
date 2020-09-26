using HomeAssistant.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using System.Net;
using System;
using HomeAssistant.Helper;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAssistant.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private static readonly WebProxy proxy = new WebProxy("192.168.0.109:80");
        private static readonly Uri address = new Uri("http://home.as");

        public event PropertyChangedEventHandler PropertyChanged;

        //private Task<ObservableCollection<DeviceBase>> InitializationTask; 

        private HomeAssistantClient apiClient;

        public ObservableCollection<DeviceBase> ConnectedDevices { get; private set; }

        public Command<string> ConnectDevice { get; }

        public Command<string> SelectDeviceCommand { get; }

        private DeviceBase selectedDevice;

        public DeviceBase SelectedDevice 
        {
            get
            {
                return selectedDevice;
            }
            private set
            {
                selectedDevice = value;
                NotifyPropertyChanged(nameof(SelectedDevice));
            }
        }

        public MainPageViewModel()
        {
            // Wait for response from server containing connected devices
            apiClient = new HomeAssistantClient(address, proxy);
            var InitializationTask = apiClient.GetConnectedDevices();
            InitializationTask.ContinueWith((initializationResult) => {
                ConnectedDevices = initializationResult.Result;

                var testDevice = new MiKettle()
                {
                    Id = "997-ten-numer-to-kłopoty",
                    Name = "Xiaomi Mi Kettle"
                };

                ConnectedDevices.Add(testDevice);
                NotifyPropertyChanged(nameof(ConnectedDevices));
            });
            
            SelectDeviceCommand = new Command<string>((string deviceId) => {

                if (deviceId == null)
                {
                    return;
                }

                var deviceEnumerator = ConnectedDevices.Where((DeviceBase device) => {
                    return device.Id.Equals(deviceId);
                });

                SelectedDevice = deviceEnumerator.First();
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
