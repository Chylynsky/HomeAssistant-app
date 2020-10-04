using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAssistant.Model
{
    class MiKettle : DeviceModel
    {
        public enum Action
        {
            Idle = 0,
            Heating = 1,
            Cooling = 2,
            KeepingWarm = 3
        }

        public enum Mode
        {
            None = 255,
            Boil = 1,
            KeepWarm = 2
        }

        public enum KeepWarmType
        {
            BoilAndCool = 0,
            HeatUp = 1
        }

        public enum BoilMode
        {
            NoTurnOffAfterBoil = 0,
            TurnOffAfterBoil = 1
        }

        public int TemperatureCurrent { get; set; }

        public int TemperatureSet { get; set; }

        public MiKettle()
        {
            
        }
    }
}
