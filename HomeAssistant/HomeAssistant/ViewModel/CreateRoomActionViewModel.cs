using HomeAssistant.Helper;
using HomeAssistant.Model;
using HomeAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace HomeAssistant.View
{
    public class CreateRoomActionViewModel : IActionViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        RoomModel roomModel;

        public Command CreateRoom;

        public string Title
        {
            get
            {
                return "Create new room";
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
                return new List<string>(Enum.GetNames(typeof(RoomType)).Select((elem) => { return elem.SplitCamelCase(); }));
            }
        }

        public CreateRoomActionViewModel()
        {
            roomModel = new RoomModel();
            CreateRoom = new Command(() => {
                Application.Current.MainPage.DisplayPromptAsync("git", "konkrecik, zapraszam");
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}