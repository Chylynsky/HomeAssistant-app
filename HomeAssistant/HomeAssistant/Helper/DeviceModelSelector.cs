using HomeAssistant.Model;
using System;
using System.Collections.Generic;

namespace HomeAssistant.Helper
{
    // Class matching device type string with its in-code model.
    public class DeviceModelSelector : Dictionary<string, Func<DeviceModelBase>>
    {
        public DeviceModelSelector()
        {
            this["MiKettle"] = () => { return new MiKettleModel(); };
        }
    }
}
