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
        Hall,
        Basement
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
                case RoomType.LivingRoom: return "Salon";
                case RoomType.Kitchen: return "Kuchnia";
                case RoomType.Bedroom: return "Sypialnia";
                case RoomType.DiningRoom: return "Jadalnia";
                case RoomType.Bathroom: return "Łazienka";
                case RoomType.Hall: return "Korytarz";
                case RoomType.Basement: return "Piwnica";
                default: return string.Empty;
            }
        }

        public static ImageSource RoomTypeToImageSource(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.LivingRoom: return "LivingRoom0.jpg";
                case RoomType.Kitchen: return "Kitchen.jpg";
                case RoomType.Bedroom: return "Bedroom.jpg";
                case RoomType.DiningRoom: return "DiningRoom.jpg";
                case RoomType.Bathroom: return "Bathroom.jpg";
                case RoomType.Hall: return "Hall.jpg";
                case RoomType.Basement: return "Basement.jpg";
                default: return null;
            }
        }
    }
}
