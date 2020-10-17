using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using HomeAssistant.Model;
using HomeAssistant.Helper;
using System.Net;
using System.Threading.Tasks;

namespace HomeAssistant.ViewModel
{
    /// <summary>
    /// View model for Home page. Home page contains collection of rooms to select and allows to control them.
    /// </summary>
    class HomeViewModel : ThemedViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command<string> SelectRoomCommand { get; }

        public ObservableCollection<RoomCardViewModel> RoomCardViewModels { get; set; }

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

            var getUserDataTask = HomeAssistantClient.GetUserData();
            var getConnectedDevicesTask = HomeAssistantClient.GetConnectedDevices();

            Task.WhenAll(getUserDataTask, getConnectedDevicesTask).ContinueWith((task) => {

                var userData = getUserDataTask.Result;
                var connectedDevices = getConnectedDevicesTask.Result;

                foreach (var roomEntry in userData.Rooms)
                {
                    var roomModel = new RoomModel()
                    {
                        Type = (RoomType)Enum.Parse(typeof(RoomType), roomEntry.Type.RemoveWhitespaces()),
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
