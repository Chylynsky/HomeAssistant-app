using HomeAssistant.Model;
using System;
using System.Collections.Generic;

namespace HomeAssistant.Helper
{
    public class DeviceModelSelector : Dictionary<string, Func<DeviceModelBase>>
    {
        public DeviceModelSelector()
        {
            this["MiKettle"] = () => { return new MiKettleModel(); };
        }
    }
}
