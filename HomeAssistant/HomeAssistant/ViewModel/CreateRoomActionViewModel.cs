using HomeAssistant.Helper;
using HomeAssistant.Model;
using HomeAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeAssistant.View
{
    public class CreateRoomActionViewModel : IActionViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        RoomModel roomModel;

        public Command CreateRoomCommand { get; }

        public string Title
        {
            get
            {
                return "New room";
            }
        }

        public string RoomName
        {
            get
            {
                return roomModel.Name;
            }
            set
            {
                roomModel.Name = value;
                NotifyPropertyChanged(nameof(RoomName));
            }
        }

        public RoomType RoomType
        {
            get
            {
                return roomModel.RoomType;
            }
            set
            {
                roomModel.RoomType = value;
            }
        }

        public List<string> RoomTypes
        {
            get
            {
                return new List<string>(Enum.GetNames(typeof(RoomType)).Select((elem) => { 
                    return elem.SplitCamelCase(); 
                }));
            }
        }

        public ObservableCollection<DeviceModelBase> AvailableDevices { get; private set; }

        public CreateRoomActionViewModel()
        {
            roomModel = new RoomModel();
            AvailableDevices = new ObservableCollection<DeviceModelBase>();

            Task.Run(async () => {
                var availableDevices = await HomeAssistantClient.GetConnectedDevices();

                if (availableDevices == null && availableDevices.Count == 0)
                {
                    return;
                }

                AvailableDevices = new ObservableCollection<DeviceModelBase>(availableDevices);
            });

            CreateRoomCommand = new Command(() => {
                return;
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}