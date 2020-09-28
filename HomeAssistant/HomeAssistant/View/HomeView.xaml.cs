using HomeAssistant.Model;
using HomeAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentView
    {
        public static readonly BindableProperty RoomsProperty = BindableProperty.Create(
            nameof(Rooms),
            typeof(ObservableCollection<RoomCardViewModel>),
            typeof(HomeView),
            default(ObservableCollection<RoomCardViewModel>));

        public static readonly BindableProperty SelectRoomCommandProperty = BindableProperty.Create(
            nameof(SelectRoomCommand),
            typeof(Command<string>),
            typeof(HomeView),
            default(Command<string>));

        public static readonly BindableProperty SelectedRoomProperty = BindableProperty.Create(
            nameof(SelectedRoom),
            typeof(RoomModel),
            typeof(HomeView),
            default(RoomModel));

        private ObservableCollection<RoomCardViewModel> rooms;

        public ObservableCollection<RoomCardViewModel> Rooms
        {
            get
            {
                return (ObservableCollection<RoomCardViewModel>)GetValue(RoomsProperty);
            }
            set
            {
                SetValue(RoomsProperty, value);
            }
        }

        private Command<string> selectRoomCommand;

        public Command<string> SelectRoomCommand
        {
            get
            {
                return (Command<string>)GetValue(SelectRoomCommandProperty);
            }
            set
            {
                SetValue(SelectRoomCommandProperty, value);
            }
        }

        private RoomModel selectedRoom;

        public RoomModel SelectedRoom
        {
            get
            {
                return (RoomModel)GetValue(SelectedRoomProperty);
            }
            set
            {
                SetValue(SelectedRoomProperty, value);
            }
        }

        public event EventHandler RoomSelected;

        public HomeView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == RoomsProperty.PropertyName)
            {
                rooms = Rooms;
            }
            else if (propertyName == SelectRoomCommandProperty.PropertyName)
            {
                selectRoomCommand = SelectRoomCommand;
            }
            else if (propertyName == SelectedRoomProperty.PropertyName)
            {
                selectedRoom = SelectedRoom;
            }
        }

        private void roomCard_Clicked(object sender, EventArgs e)
        {
            RoomSelected.Invoke(this, e);
        }
    }
}