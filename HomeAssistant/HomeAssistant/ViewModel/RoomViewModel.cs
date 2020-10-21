using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using HomeAssistant.Model;
using HomeAssistant.ViewModel.DeviceViewModels;
using HomeAssistant.Helper;
using System.Net;

namespace HomeAssistant.ViewModel
{
    class RoomViewModel : ThemedViewModelBase
    {
        public Command<string> SelectDeviceCommand { get; }

        private static readonly int ThemedBackgroundMax = 5;

        private DeviceViewModelSelector deviceViewModelSelector;

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

        public string Name
        {
            get
            {
                return RoomModel.Name;
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

        public RoomViewModel(RoomModel roomModel)
        {
            deviceViewModelSelector = new DeviceViewModelSelector();
            RoomModel = roomModel;
            Background = GetImage();
            DeviceCardsViewModels = new ObservableCollection<DeviceCardSmallViewModel>();

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

        protected override ImageSource GetImage()
        {
            if (RoomModel == null)
            {
                return string.Empty;
            }

            if (RoomModel.RoomType == RoomType.Other)
            {
                base.GetImage();
            }

            string image = RoomModel.RoomType.ToString().ToLower() + randomGenerator.Next(0, ThemedBackgroundMax).ToString() + ".png";

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return string.Empty;
                case Device.Android:
                    return image;
                case Device.UWP: return ResourcePathUWP + image;
                default: return string.Empty;
            }
        }

        private DeviceViewModelBase SelectDeviceViewModel(DeviceModelBase deviceModel)
        {
            return deviceViewModelSelector[deviceModel.GetType()](deviceModel);
        }
    }
}
