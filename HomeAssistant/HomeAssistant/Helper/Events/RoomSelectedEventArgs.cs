using HomeAssistant.Model;
using System;

namespace HomeAssistant.Helper.Events
{
    public class RoomSelectedEventArgs : EventArgs
    {
        public RoomModel RoomModel { get; set; }

        public RoomSelectedEventArgs(RoomModel roomModel)
        {
            RoomModel = roomModel;
        }
    }
}
