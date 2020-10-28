using HomeAssistant.Helper;
using HomeAssistant.Model;
using HomeAssistant.Model.Devices;
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
    public class CreateRoomViewModel : IActionViewModel
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

        public ObservableCollection<IDeviceModel> AvailableDevices { get; private set; }

        public CreateRoomViewModel()
        {
            roomModel = new RoomModel();
            AvailableDevices = new ObservableCollection<IDeviceModel>();

            Task.Run(async () => {
                var availableDevices = await HomeAssistantHttpClient.GetConnectedDevices();

                if (availableDevices == null && availableDevices.Count == 0)
                {
                    return;
                }

                AvailableDevices = new ObservableCollection<IDeviceModel>(availableDevices);
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