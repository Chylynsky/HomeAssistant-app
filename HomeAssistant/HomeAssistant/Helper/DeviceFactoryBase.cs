﻿using HomeAssistant.Model;

namespace HomeAssistant.Helper
{
    abstract class DeviceFactoryBase
    {
        public abstract DeviceBase Create();
    }
}