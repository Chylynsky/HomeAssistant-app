using HomeAssistant.Model.Devices;
using System.Collections.ObjectModel;

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
        public RoomType RoomType { get; set; }

        public string Name { get; set; }

        public ObservableCollection<IDeviceModel> Devices { get; set; }
    }
}
