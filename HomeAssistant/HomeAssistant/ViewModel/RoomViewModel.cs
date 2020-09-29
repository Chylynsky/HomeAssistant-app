using HomeAssistant.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.ViewModel
{
    class RoomViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command<string> SelectDeviceCommand { get; }

        public ObservableCollection<DeviceCardSmallViewModel> DeviceCardsViewModels { get; private set; }

        public RoomModel Room { get; private set; }

        private DeviceModel selectedDevice;

        public DeviceModel SelectedDevice 
        {
            get
            {
                return selectedDevice;
            }
            private set
            {
                selectedDevice = value;
                NotifyPropertyChanged(nameof(SelectedDevice));
            }
        }

        public RoomViewModel(RoomModel room)
        {
            Room = room;
            DeviceCardsViewModels = new ObservableCollection<DeviceCardSmallViewModel>();

            SelectDeviceCommand = new Command<string>((string deviceId) => {

                if (deviceId == null)
                {
                    return;
                }

                var deviceEnumerator = Room.Devices.Where((DeviceModel device) => {
                    return device.Id.Equals(deviceId);
                });

                SelectedDevice = deviceEnumerator.First();
            });

            foreach (DeviceModel deviceModel in Room.Devices)
            {
                var deviceCardViewModel = new DeviceCardSmallViewModel(deviceModel);
                deviceCardViewModel.SelectDeviceCommand = SelectDeviceCommand;
                DeviceCardsViewModels.Add(deviceCardViewModel);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
