using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HomeAssistant.Model
{
    public enum RoomType
    {
        LivingRoom,
        Kitchen,
        Bedroom,
        DiningRoom,
        Bathroom,
        Other
    }

    public class RoomModel
    {
        public RoomType Type { get; set; }

        public string Name { get; set; }

        public ObservableCollection<DeviceModelBase> Devices { get; set; }
    }
}
