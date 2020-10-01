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

        public string Name
        {
            get
            {
                return RoomTypeToString(Type);
            }
        }

        public ObservableCollection<DeviceModel> Devices { get; set; }

        public static string RoomTypeToString(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.LivingRoom: return "Living Room";
                case RoomType.Kitchen: return "Kitchen";
                case RoomType.Bedroom: return "Bedroom";
                case RoomType.DiningRoom: return "Dining Room";
                case RoomType.Bathroom: return "Bathroom";
                default: return "Other";
            }
        }
    }
}
