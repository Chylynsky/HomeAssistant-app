using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using HomeAssistant.Helper.Events;
using HomeAssistant.Model;
using HomeAssistant.Helper;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace HomeAssistant.ViewModel
{
    class HomeViewModel : ThemedViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command<string> SelectRoomCommand { get; }

        public ObservableCollection<RoomCardViewModel> RoomCardViewModels { get; set; }

        private HomeAssistantClient httpClient;

        private RoomViewModel selectedRoomModel;

        public RoomViewModel SelectedRoomViewModel
        {
            get
            {
                return selectedRoomModel;
            }
            private set
            {
                selectedRoomModel = value;
                NotifyPropertyChanged(nameof(SelectedRoomViewModel));
            }
        }

        public HomeViewModel()
        {
            httpClient = new HomeAssistantClient(new Uri("http://home.as"), new WebProxy("http://192.168.0.109:80"));
            RoomCardViewModels = new ObservableCollection<RoomCardViewModel>();

            SelectRoomCommand = new Command<string>((string roomName) =>
            {

                if (roomName == null)
                {
                    return;
                }

                var roomEnumerator = RoomCardViewModels.Where((RoomCardViewModel roomCardViewModel) =>
                {
                    return roomCardViewModel.Name.Equals(roomName);
                });

                var selectedRoomCard = roomEnumerator.First();
                SelectedRoomViewModel = new RoomViewModel(selectedRoomCard.RoomModel, selectedRoomCard.Background);
            });

            var getUserDataTask = httpClient.GetUserData();
            var getConnectedDevicesTask = httpClient.GetConnectedDevices();

            Task.WhenAll(getUserDataTask, getConnectedDevicesTask).ContinueWith((task) => {

                var userData = getUserDataTask.Result;
                var connectedDevices = getConnectedDevicesTask.Result;

                foreach (var roomEntry in userData.Rooms)
                {
                    var roomModel = new RoomModel()
                    {
                        Type = RoomModel.RoomTypeStringToRoomTypeEnum(roomEntry.Type),
                        Name = roomEntry.Name,
                        Devices = new ObservableCollection<DeviceModelBase>(connectedDevices.Where((DeviceModelBase deviceModel) => {

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

                    var roomCardViewModel = new RoomCardViewModel(roomModel)
                    {
                        SelectRoomCommand = SelectRoomCommand
                    };

                    Device.BeginInvokeOnMainThread(() => {
                        RoomCardViewModels.Add(roomCardViewModel);
                    });
                }
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
