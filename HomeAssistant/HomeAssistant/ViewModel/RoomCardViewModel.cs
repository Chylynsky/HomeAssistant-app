using HomeAssistant.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class RoomCardViewModel : ThemedRoomCardViewModelBase
    {
        public Command<string> SelectRoomCommand { get; set; }

        public RoomModel RoomModel { get; private set; }

        public string Name
        {
            get
            {
                return RoomModel.RoomTypeToString(RoomModel.Type);
            }
        }

        public RoomCardViewModel(RoomModel roomModel) : base(roomModel.Type)
        {
            RoomModel = roomModel;
        }
    }
}
