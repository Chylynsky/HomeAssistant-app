using HomeAssistant.Model;
using System;

namespace HomeAssistant.Helper.Events
{
    public class RoomSelectedEventArgs : EventArgs
    {
        public RoomModel RoomModel { get; private set; }

        public RoomSelectedEventArgs(RoomModel roomModel)
        {
            RoomModel = roomModel;
        }
    }
}
