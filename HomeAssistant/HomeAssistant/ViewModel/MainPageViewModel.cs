using HomeAssistant.Model;
using System.Collections.ObjectModel;.
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

        private readonly WebProxy proxy = new WebProxy("0.0.0.0:80");

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
            apiClient = new HomeAssistantClient(address, proxy);

            RoomModels = new ObservableCollection<RoomModel>();
            roomViewModels = new List<RoomViewModel>();



            /*
            RoomModels.Add(roomModel);
            
            UserHomeViewModel = new HomeViewModel(RoomModels.ToList());
            UserHomeViewModel.RoomSelected += HomeViewModel_RoomSelected;

            foreach (var roomCardViewModel in UserHomeViewModel.RoomCardViewModels)
            {
                roomViewModels.Add(new RoomViewModel(roomCardViewModel.RoomModel, roomCardViewModel.Background));
            }
            */
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
