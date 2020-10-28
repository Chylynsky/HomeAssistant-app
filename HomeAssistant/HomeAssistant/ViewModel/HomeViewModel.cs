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
using HomeAssistant.Model.Devices;
using HomeAssistant.View;

namespace HomeAssistant.ViewModel
{
    /// <summary>
    /// View model for Home page. Home page contains collection of rooms to select and allows to control them.
    /// </summary>
    class HomeViewModel : ThemedViewModelBase
    {
        public Command<string> SelectRoomCommand { get; }

        public ObservableCollection<RoomViewModel> RoomViewModels { get; private set; }

        public CreateRoomViewModel CreateRoomViewModel { get; private set; }

        public HomeViewModel()
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

                await NavigationService.Navigation.NavigateToAsync(slectedRoomViewModel);
            });

            Task.Run(async () => { await InitializeAsync(null); });
        }

        public HomeViewModel(UserModel userModel)
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

                await NavigationService.Navigation.NavigateToAsync(slectedRoomViewModel);
            });

            Task.Run(async () => { await InitializeAsync(userModel); });
        }

        private async Task InitializeAsync(UserModel userModel)
        {
            if (userModel == null)
            {
                userModel = await HomeAssistantHttpClient.GetUserData();
            }

            var connectedDevices = await HomeAssistantHttpClient.GetConnectedDevices();

            foreach (var roomEntry in userModel.Rooms)
            {
                var roomModel = new RoomModel()
                {
                    RoomType = (RoomType)Enum.Parse(typeof(RoomType), roomEntry.Type.RemoveWhitespaces()),
                    Name = roomEntry.Name,
                    Devices = new ObservableCollection<IDeviceModel>(connectedDevices.Where((IDeviceModel deviceModel) =>
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
        }
    }
}
