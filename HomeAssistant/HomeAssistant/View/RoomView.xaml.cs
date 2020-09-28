using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using HomeAssistant.ViewModel;
using HomeAssistant.Model;
using System.Runtime.CompilerServices;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomView : ContentView
    {
        public static readonly BindableProperty ConnectedDevicesProperty = BindableProperty.Create(
            nameof(ConnectedDevices),
            typeof(ObservableCollection<DeviceModel>),
            typeof(RoomView),
            default(ObservableCollection<DeviceModel>));

        public static readonly BindableProperty SelectDeviceCommandProperty = BindableProperty.Create(
            nameof(SelectDeviceCommand),
            typeof(Command<string>),
            typeof(RoomView),
            default(Command<string>));

        public static readonly BindableProperty SelectedDeviceProperty = BindableProperty.Create(
            nameof(SelectedDevice),
            typeof(DeviceModel),
            typeof(RoomView),
            default(DeviceModel));

        private ObservableCollection<DeviceModel> connectedDevices;

        public ObservableCollection<DeviceModel> ConnectedDevices
        {
            get
            {
                return (ObservableCollection<DeviceModel>)GetValue(ConnectedDevicesProperty);
            }
            set
            {
                SetValue(ConnectedDevicesProperty, value);
            }
        }

        private Command<string> selectDeviceCommand;

        public Command<string> SelectDeviceCommand
        {
            get
            {
                return (Command<string>)GetValue(SelectDeviceCommandProperty);
            }
            set
            {
                SetValue(SelectDeviceCommandProperty, value);
            }
        }

        private DeviceModel selectedDevice;

        public DeviceModel SelectedDevice
        {
            get
            {
                return (DeviceModel)GetValue(SelectedDeviceProperty);
            }
            set
            {
                SetValue(SelectedDeviceProperty, value);
            }
        }

        public event EventHandler BackButtonClicked;

        public RoomView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ConnectedDevicesProperty.PropertyName)
            {
                connectedDevices = ConnectedDevices;
            }
            else if (propertyName == SelectDeviceCommandProperty.PropertyName)
            {
                selectDeviceCommand = SelectDeviceCommand;
            }
            else if (propertyName == SelectedDeviceProperty.PropertyName)
            {
                selectedDevice = SelectedDevice;
            }
        }

        private async void ShowActionCard()
        {
            actionCard.IsVisible = true;
            await actionCard.TranslateTo(0.0, 0.0, 100, Easing.SinOut);
            await actionCard.ScaleTo(1.0, 50, Easing.SinOut);
            actionCard.IsEnabled = true;
        }

        private async void HideActionCard()
        {
            actionCard.IsEnabled = false;
            await actionCard.ScaleTo(0.8, 50, Easing.SinIn);
            await actionCard.TranslateTo(0.0, mainGrid.Bounds.Bottom, 100, Easing.SinIn);
            actionCard.IsVisible = false;
        }

        private void actionCard_Closed(object sender, EventArgs e)
        {
            HideActionCard();
        }

        private void actionCard_Swiped(object sender, SwipedEventArgs e)
        {
            if (e.Direction != SwipeDirection.Down)
            {
                return;
            }

            HideActionCard();
        }

        private void deviceCard_Clicked(object sender, EventArgs e)
        {
            ShowActionCard();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            BackButtonClicked.Invoke(sender, e);
        }
    }
}