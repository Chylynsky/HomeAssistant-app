using HomeAssistant.Model.Devices;

namespace HomeAssistant.Model.Devices
{
    public abstract class DeviceModelBase : IDeviceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
