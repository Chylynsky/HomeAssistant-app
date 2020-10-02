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
        public event PropertyChangedEventHandler PropertyChanged;

        private Task<ObservableCollection<DeviceModel>> InitializationTask; 

        private HomeAssistantClient apiClient;

        private List<RoomViewModel> roomViewModels;

        private ObservableCollection<RoomModel> roomModels;

        public ObservableCollection<RoomModel> RoomModels 
        { 
            get
            {
                return roomModels;
            }
            private set
            {
                roomModels = value;
                NotifyPropertyChanged(nameof(RoomModels));
            }
        }

        private HomeViewModel HomeViewModel;

        public HomeViewModel UserHomeViewModel 
        { 
            get
            {
                return HomeViewModel;
            }
            private set
            {
                HomeViewModel = value;
                NotifyPropertyChanged(nameof(UserHomeViewModel));
            }
        }

        private RoomViewModel selectedRoomViewModel;

        public RoomViewModel SelectedRoomViewModel 
        {
            get
            {
                return selectedRoomViewModel;
            }
            private set
            {
                selectedRoomViewModel = value;
                NotifyPropertyChanged(nameof(SelectedRoomViewModel));
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

            RoomModels = new ObservableCollection<RoomModel>();
            roomViewModels = new List<RoomViewModel>();

            var roomModel = new RoomModel()
            {
                Devices = new ObservableCollection<DeviceModel>(),
                Type = RoomType.LivingRoom
            };

            DeviceModel deviceModel0 = new MiKettle()
            {
                Id = "1234",
                Name = "Xiaomi Mi Kettle"
            };

            DeviceModel deviceModel1 = new MiKettle()
            {
                Id = "698",
                Name = "Dupa"
            };

            roomModel.Devices.Add(deviceModel0);
            roomModel.Devices.Add(deviceModel1);
            RoomModels.Add(roomModel);

            UserHomeViewModel = new HomeViewModel(RoomModels.ToList());
            UserHomeViewModel.RoomSelected += HomeViewModel_RoomSelected;

            foreach (var roomCardViewModel in UserHomeViewModel.RoomCardViewModels)
            {
                roomViewModels.Add(new RoomViewModel(roomCardViewModel.RoomModel, roomCardViewModel.Background));
            }
        }

        private void HomeViewModel_RoomSelected(RoomCardViewModel sender, RoomSelectedEventArgs args)
        {
            SelectedRoomViewModel = roomViewModels.Find((RoomCard) => {
                return RoomCard.RoomModel == args.RoomModel && RoomCard.Background == sender.Background;
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
