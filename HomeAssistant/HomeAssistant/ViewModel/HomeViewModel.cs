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

namespace HomeAssistant.ViewModel
{
    class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event RoomSelectedEventHandler RoomSelected;

        public Command<string> SelectRoomCommand { get; }

        public ObservableCollection<RoomCardViewModel> RoomCardViewModels { get; private set; }

        private RoomModel selectedRoom;
        public RoomModel SelectedRoom
        {
            get
            {
                return selectedRoom;
            }
            private set
            {
                selectedRoom = value;
                NotifyPropertyChanged(nameof(SelectedRoom));
            }
        }

        public HomeViewModel(List<RoomModel> roomModels)
        {
            RoomCardViewModels = new ObservableCollection<RoomCardViewModel>();

            SelectRoomCommand = new Command<string>((string roomName) => {

                if (roomName == null)
                {
                    return;
                }
                
                var roomEnumerator = RoomCardViewModels.Where((RoomCardViewModel roomViewModel) => {
                    return roomViewModel.Name.Equals(roomName);
                });

                SelectedRoom = roomEnumerator.First().Room;
                RoomSelected.Invoke(this, new RoomSelectedEventArgs(SelectedRoom));
            });

            foreach (RoomModel roomModel in roomModels)
            {
                var roomViewModel = new RoomCardViewModel(roomModel);
                roomViewModel.SelectRoomCommand = SelectRoomCommand;
                RoomCardViewModels.Add(roomViewModel);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
