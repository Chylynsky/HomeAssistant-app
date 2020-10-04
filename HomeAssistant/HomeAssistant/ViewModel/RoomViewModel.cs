using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeAssistant.Model;

namespace HomeAssistant.ViewModel
{
    class RoomViewModel : IThemedViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command<string> SelectDeviceCommand { get; }

        private ObservableCollection<DeviceCardSmallViewModel> deviceCardsViewModels;

        public ObservableCollection<DeviceCardSmallViewModel> DeviceCardsViewModels
        {
            get
            {
                return deviceCardsViewModels;
            }
            private set
            {
                deviceCardsViewModels = value;
                NotifyPropertyChanged(nameof(DeviceCardsViewModels));
            }
        }

        private RoomModel roomModel;

        public RoomModel RoomModel
        {
            get
            {
                return roomModel;
            }
            private set
            {
                roomModel = value;
                NotifyPropertyChanged(nameof(RoomModel));
            }
        }

        private ImageSource background;

        public ImageSource Background 
        { 
            get
            {
                return background;
            }
            set
            {
                background = value;
                NotifyPropertyChanged(nameof(Background));
            }
        }

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

        public RoomViewModel(RoomModel roomModel, ImageSource background = default(ImageSource))
        {
            RoomModel = roomModel;
            Background = background;
            DeviceCardsViewModels = new ObservableCollection<DeviceCardSmallViewModel>();

            SelectDeviceCommand = new Command<string>((string deviceId) => {

                if (deviceId == null)
                {
                    return;
                }

                var deviceEnumerator = RoomModel.Devices.Where((DeviceModel device) => {
                    return device.Id.Equals(deviceId);
                });

                SelectedDevice = deviceEnumerator.First();
            });

            foreach (DeviceModel deviceModel in RoomModel.Devices)
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
