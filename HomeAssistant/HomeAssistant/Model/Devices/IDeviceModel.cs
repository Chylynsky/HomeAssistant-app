using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAssistant.Model.Devices
{
    public interface IDeviceModel
    {
        string Id { get; set; }

        string Name { get; set; }
    }
}
