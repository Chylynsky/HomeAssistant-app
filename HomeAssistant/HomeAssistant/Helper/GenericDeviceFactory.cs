using HomeAssistant.Model;

namespace HomeAssistant.Helper
{
    class GenericDeviceFactory<DeviceType> : DeviceFactoryBase 
        where DeviceType : DeviceBase, new()
    {
        public override DeviceBase Create()
        {
            return new DeviceType();
        }
    }
}
