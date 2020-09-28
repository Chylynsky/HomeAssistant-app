using HomeAssistant.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class RoomCardViewModel
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

        public ImageSource RoomImageSource
        {
            get
            {
                return RoomModel.RoomTypeToImageSource(Room.Type);
            }
        }

        public RoomCardViewModel(RoomModel room)
        {
            Room = room;
        }
    }
}
