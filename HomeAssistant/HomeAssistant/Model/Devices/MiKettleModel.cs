using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAssistant.Model.Devices
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

    public class MiKettleModel : DeviceModelBase
    {
        public static readonly int MinTemperature = 40;

        public static readonly int MaxTemperature = 95;

        public static readonly float MinKeepWarmTimeLimit = 1.0f;

        public static readonly float MaxKeepWarmTimeLimit = 12.0f;

        public int TemperatureCurrent { get; set; }

        public int TemperatureSet { get; set; }

        public Action Action { get; set; }

        public Mode Mode { get; set; }

        public KeepWarmType KeepWarmType { get; set; }

        public float KeepWarmTime { get; set; }

        public float KeepWarmTimeLimit { get; set; }

        public BoilMode BoilMode { get; set; }
    }
}
