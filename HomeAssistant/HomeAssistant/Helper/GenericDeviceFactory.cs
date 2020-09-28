using HomeAssistant.Model;

namespace HomeAssistant.Helper
{
    class GenericDeviceFactory<DeviceType> : DeviceFactoryBase 
        where DeviceType : DeviceModel, new()
    {
        public override DeviceModel Create()
        {
            return new DeviceType();
        }
    }
}
