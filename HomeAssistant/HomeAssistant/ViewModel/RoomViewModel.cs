using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using HomeAssistant.Model;
using HomeAssistant.ViewModel.Devices;
using HomeAssistant.Helper;
using HomeAssistant.Model.Devices;

namespace HomeAssistant.ViewModel
{
    class RoomViewModel : ThemedViewModelBase
    {
        public Command<string> SelectDeviceCommand { get; }

        private static readonly int ThemedBackgroundMax = 5;

        private ObservableCollection<IDeviceViewModel> deviceViewModels;

        public ObservableCollection<IDeviceViewModel> DeviceViewModels
        {
            get
            {
                return deviceViewModels;
            }
            private set
            {
                deviceViewModels = value;
                NotifyPropertyChanged(nameof(DeviceViewModels));
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

        private IDeviceViewModel selectedDevice;

        public IDeviceViewModel SelectedDeviceViewModel 
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
            RoomModel = roomModel;
            Background = GetImage();
            DeviceViewModels = new ObservableCollection<IDeviceViewModel>();

            SelectDeviceCommand = new Command<string>((string deviceId) => {

                if (deviceId == null)
                {
                    return;
                }

                var deviceEnumerator = DeviceViewModels.Where((IDeviceViewModel deviceViewModel) => {
                    return deviceViewModel.Id.Equals(deviceId);
                });

                SelectedDeviceViewModel = deviceEnumerator.First();
            });

            foreach (IDeviceModel deviceModel in RoomModel.Devices)
            {
                DeviceViewModels.Add(DeviceLinker.GetDeviceViewModelForModel(deviceModel.GetType()));
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
    }
}
