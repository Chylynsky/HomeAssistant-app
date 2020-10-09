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
            httpClient = new HomeAssistantClient(new Uri("home.as"), new WebProxy("192.168.0.109:80"));
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
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
