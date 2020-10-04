using HomeAssistant.Model;
using System.Collections.ObjectModel;
using System.Linq;
using HomeAssistant.Helper;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using HomeAssistant.Helper.Events;
using System;
using System.Net;

namespace HomeAssistant.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly Uri address = new Uri("http://home.as");

        private readonly WebProxy proxy = new WebProxy("192.168.0.109:80");

        public event PropertyChangedEventHandler PropertyChanged;

        private HomeAssistantClient apiClient;

        private List<RoomViewModel> roomViewModels;

        private List<RoomModel> roomModels;

        private HomeViewModel homeViewModel;

        public HomeViewModel HomeViewModel 
        { 
            get
            {
                return homeViewModel;
            }
            private set
            {
                homeViewModel = value;
                NotifyPropertyChanged(nameof(HomeViewModel));
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

        private LoginViewModel loginPageViewModel;

        public LoginViewModel LoginViewModel
        {
            get
            {
                return loginPageViewModel;
            }
            private set
            {
                loginPageViewModel = value;
                NotifyPropertyChanged(nameof(LoginViewModel));
            }
        }

        public MainPageViewModel()
        {
            // Wait for response from server containing connected devices
            apiClient = new HomeAssistantClient(address, proxy);

            roomModels = new List<RoomModel>();
            roomViewModels = new List<RoomViewModel>();

            LoginViewModel = new LoginViewModel(apiClient);
            LoginViewModel.LoginSuccess += async (sender, args) => {

                var connectedDevices = await apiClient.GetConnectedDevices();

                foreach (var roomEntry in args.UserModel.Rooms)
                {
                    var roomModel = new RoomModel()
                    {
                        Type = RoomModel.RoomTypeStringToRoomTypeEnum(roomEntry.Type),
                        Name = roomEntry.Name,
                        Devices = new ObservableCollection<DeviceModel>(connectedDevices.Where((DeviceModel deviceModel) => {

                            foreach (var deviceEntry in roomEntry.Devices)
                            {
                                if (deviceEntry.Id == deviceModel.Id)
                                {
                                    return true;
                                }
                            }

                            return false;
                        }))
                    };

                    roomModels.Add(roomModel);
                    roomViewModels.Add(new RoomViewModel(roomModel));
                }

                HomeViewModel = new HomeViewModel(roomModels);
                HomeViewModel.RoomSelected += HomeViewModel_RoomSelected;
            };
        }

        private void HomeViewModel_RoomSelected(RoomCardViewModel sender, RoomSelectedEventArgs args)
        {
            SelectedRoomViewModel = roomViewModels.Find((RoomCard) => {
                return RoomCard.RoomModel.Name == args.RoomModel.Name;
            });

            // Assign background of the Room Card selected to the Room View
            SelectedRoomViewModel.Background = sender.Background;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
