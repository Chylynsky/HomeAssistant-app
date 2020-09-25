using System.Collections.Generic;
using HomeAssistant.Model;

namespace HomeAssistant.Helper
{
    // Dictionary class holding device factory object as value
    // and the produced device class type name as key.
    class DeviceFactoryDictionary : Dictionary<string, DeviceFactoryBase>
    {
        public DeviceFactoryDictionary()
        {
            this["MiKettle"] = new GenericDeviceFactory<MiKettle>();
        }
    }
}
