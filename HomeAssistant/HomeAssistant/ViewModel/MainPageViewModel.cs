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
using System.Collections.Generic;
using HomeAssistant.Helper.Events;

namespace HomeAssistant.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private static readonly WebProxy proxy = new WebProxy("192.168.0.109:80");
        private static readonly Uri address = new Uri("http://home.as");

        public event PropertyChangedEventHandler PropertyChanged;

        private Task<ObservableCollection<DeviceModel>> InitializationTask; 

        private HomeAssistantClient apiClient;

        private ObservableCollection<RoomModel> rooms;

        public ObservableCollection<RoomModel> Rooms 
        { 
            get
            {
                return rooms;
            }
            private set
            {
                rooms = value;
                NotifyPropertyChanged(nameof(Rooms));
            }
        }

        public HomeViewModel HomeViewModel { get; private set; }

        private RoomViewModel roomViewModel;

        public RoomViewModel RoomViewModel 
        {
            get
            {
                return roomViewModel;
            }
            private set
            {
                roomViewModel = value;
                NotifyPropertyChanged(nameof(RoomViewModel));
            }
        }

        public MainPageViewModel()
        {
            // Wait for response from server containing connected devices
            /*apiClient = new HomeAssistantClient(address, proxy);
            var InitializationTask = apiClient.GetConnectedDevices();
            InitializationTask.ContinueWith((initializationResult) => {
                
                foreach (device in initializationResult.Result)
                {
                    ConnectedDevices.Add(new DeviceCardSmallViewModel(device))
                }

                NotifyPropertyChanged(nameof(ConnectedDevices));
            });*/

            Rooms = new ObservableCollection<RoomModel>();
            var roomModel = new RoomModel()
            {
                Devices = new ObservableCollection<DeviceModel>(),
                Type = RoomType.LivingRoom
            };

            DeviceModel deviceModel = new MiKettle()
            {
                Id = "1234",
                Name = "Xiaomi Mi Kettle"
            };

            roomModel.Devices.Add(deviceModel);
            Rooms.Add(roomModel);

            HomeViewModel = new HomeViewModel(Rooms.ToList());
            HomeViewModel.RoomSelected += HomeViewModel_RoomSelected;
        }

        private void HomeViewModel_RoomSelected(object sender, RoomSelectedEventArgs args)
        {
            RoomViewModel = new RoomViewModel(args.RoomModel);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
