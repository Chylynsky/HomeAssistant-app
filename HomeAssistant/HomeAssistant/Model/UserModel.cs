using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAssistant.Model
{
    public class DeviceEntry
    {
        public string Id { get; set; }
    }

    public class RoomEntry
    {
        public List<DeviceEntry> Devices { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }

    public class UserModel
    {
        public List<RoomEntry> Rooms { get; set; }
    }
}
