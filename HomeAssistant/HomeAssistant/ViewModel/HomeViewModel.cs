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
    class HomeViewModel : ThemedViewModelBase
    {
        public Command<string> SelectRoomCommand { get; }

        ObservableCollection<RoomViewModel> roomViewModels;

        public ObservableCollection<RoomViewModel> RoomViewModels 
        { 
            get
            {
                return roomViewModels;
            }
            set
            {
                roomViewModels = value;
                NotifyPropertyChanged(nameof(RoomViewModels));
            }
        }

        public HomeViewModel(UserModel userModel = null)
        {
            RoomViewModels = new ObservableCollection<RoomViewModel>();

            SelectRoomCommand = new Command<string>(async (string roomName) =>
            {

                if (roomName == null)
                {
                    return;
                }

                var roomEnumerator = RoomViewModels.Where((RoomViewModel roomViewModel) =>
                {
                    return roomViewModel.RoomModel.Name.Equals(roomName);
                });

                var slectedRoomViewModel = roomEnumerator.First();

                await NavigationService.Navigation.NavigateToAsync<RoomViewModel>(slectedRoomViewModel.RoomModel, slectedRoomViewModel.Background);
            });

            Task.Run(async () => {

                if (userModel == null)
                {
                    userModel = await HomeAssistantClient.GetUserData();
                }

                var connectedDevices = await HomeAssistantClient.GetConnectedDevices();

                foreach (var roomEntry in userModel.Rooms)
                {
                    var roomModel = new RoomModel()
                    {
                        RoomType = (RoomType)Enum.Parse(typeof(RoomType), roomEntry.Type.RemoveWhitespaces()),
                        Name = roomEntry.Name,
                        Devices = new ObservableCollection<DeviceModelBase>(connectedDevices.Where((DeviceModelBase deviceModel) =>
                        {

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

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        RoomViewModels.Add(new RoomViewModel(roomModel));
                    });
                }
            });
        }
    }
}
