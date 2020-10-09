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
using HomeAssistant.ViewModel.DeviceViewModels;
using HomeAssistant.Helper;
using System.Net;

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

        private HomeAssistantClient httpClient;

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

        private DeviceViewModelBase selectedDevice;

        public DeviceViewModelBase SelectedDeviceViewModel 
        {
            get
            {
                return selectedDevice;
            }
            private set
            {
                selectedDevice = value;
                NotifyPropertyChanged(nameof(SelectedDeviceViewModel));
            }
        }

        public RoomViewModel(RoomModel roomModel, ImageSource background = default(ImageSource))
        {
            RoomModel = roomModel;
            Background = background;
            DeviceCardsViewModels = new ObservableCollection<DeviceCardSmallViewModel>();
            httpClient = new HomeAssistantClient(new Uri("http://home.as"), new WebProxy("http://192.168.0.109:80"));

            SelectDeviceCommand = new Command<string>((string deviceId) => {

                if (deviceId == null)
                {
                    return;
                }

                var deviceEnumerator = RoomModel.Devices.Where((DeviceModelBase device) => {
                    return device.Id.Equals(deviceId);
                });

                SelectedDeviceViewModel = SelectDeviceViewModel(deviceEnumerator.First());
            });

            foreach (DeviceModelBase deviceModel in RoomModel.Devices)
            {
                var deviceCardViewModel = new DeviceCardSmallViewModel(deviceModel)
                { 
                    SelectDeviceCommand = SelectDeviceCommand 
                };

                DeviceCardsViewModels.Add(deviceCardViewModel);
            }
        }

        private DeviceViewModelBase SelectDeviceViewModel(DeviceModelBase deviceModel)
        {
            if (deviceModel is MiKettleModel)
            {
                return new MiKettleViewModel(deviceModel);
            }

            return null;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
