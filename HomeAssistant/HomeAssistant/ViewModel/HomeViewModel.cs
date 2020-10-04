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
    class HomeViewModel : ThemedViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event RoomSelectedEventHandler RoomSelected;

        public Command<string> SelectRoomCommand { get; }

        private ObservableCollection<RoomCardViewModel> roomCardViewModels;

        public ObservableCollection<RoomCardViewModel> RoomCardViewModels
        {
            get
            {
                return roomCardViewModels;
            }
            set
            {
                roomCardViewModels = value;
                NotifyPropertyChanged(nameof(RoomCardViewModels));
            }
        }

        private RoomModel selectedRoomModel;
        public RoomModel SelectedRoomModel
        {
            get
            {
                return selectedRoomModel;
            }
            private set
            {
                selectedRoomModel = value;
                NotifyPropertyChanged(nameof(SelectedRoomModel));
            }
        }

        public HomeViewModel()
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

                var selectedRoomCard = roomEnumerator.First();
                SelectedRoomModel = selectedRoomCard.RoomModel;
                RoomSelected?.Invoke(selectedRoomCard, new RoomSelectedEventArgs(SelectedRoomModel));
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
