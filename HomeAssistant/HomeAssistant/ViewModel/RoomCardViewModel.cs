using HomeAssistant.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class RoomCardViewModel : ThemedRoomViewModelBase
    {
        public Command<string> SelectRoomCommand { get; set; }

        public RoomModel Room { get; private set; }

        public string Name
        {
            get
            {
                return RoomModel.RoomTypeToString(Room.Type);
            }
        }

        public RoomCardViewModel(RoomModel roomModel) : base(roomModel.Type)
        {
            Room = roomModel;
        }
    }
}
