using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using HomeAssistant.Helper;
using HomeAssistant.Model.Devices;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel.Devices
{
    class MiKettleViewModel : DeviceViewModelBase
    {
        public int MinTemperature
        {
            get
            {
                return MiKettleModel.MinTemperature;
            }
        }

        public int MaxTemperature
        {
            get
            {
                return MiKettleModel.MaxTemperature;
            }
        }

        public int TemperatureSet
        {
            get
            {
                return ((MiKettleModel)DeviceModel).TemperatureSet;
            }
            set
            {
                if (((MiKettleModel)DeviceModel).TemperatureSet == value)
                {
                    return;
                }

                ((MiKettleModel)DeviceModel).TemperatureSet = value;
                HomeAssistantHttpClient.PutAsync(string.Format("action/{0}/temperature/set={1}", Id, value));
                NotifyPropertyChanged(nameof(TemperatureSet));
            }
        }

        public int TemperatureCurrent
        {
            get
            {
                return ((MiKettleModel)DeviceModel).TemperatureCurrent;
            }
            set
            {
                if (((MiKettleModel)DeviceModel).TemperatureCurrent == value)
                {
                    return;
                }

                ((MiKettleModel)DeviceModel).TemperatureCurrent = value;
                NotifyPropertyChanged(nameof(TemperatureCurrent));
            }
        }

        public float MinKeepWarmTimeLimit
        {
            get
            {
                return MiKettleModel.MinKeepWarmTimeLimit;
            }
        }

        public float MaxKeepWarmTimeLimit
        {
            get
            {
                return MiKettleModel.MaxKeepWarmTimeLimit;
            }
        }

        public float KeepWarmTimeLimit
        {
            get
            {
                return ((MiKettleModel)DeviceModel).KeepWarmTimeLimit;
            }
            set
            {
                if (((MiKettleModel)DeviceModel).KeepWarmTimeLimit == value)
                {
                    return;
                }

                ((MiKettleModel)DeviceModel).KeepWarmTimeLimit = (float)Math.Round(value * 2.0f, MidpointRounding.AwayFromZero) / 2.0f;
                HomeAssistantHttpClient.PutAsync(string.Format("action/{0}/keep_warm/time_limit={1}", Id, value));
                NotifyPropertyChanged(nameof(KeepWarmTimeLimit));
            }
        }

        public float KeepWarmTime
        {
            get
            {
                return ((MiKettleModel)DeviceModel).KeepWarmTime;
            }
        }

        private Model.Devices.Action Action
        {
            get
            {
                return ((MiKettleModel)DeviceModel).Action;
            }
            set
            {
                if (((MiKettleModel)DeviceModel).Action == value)
                {
                    return;
                }

                ((MiKettleModel)DeviceModel).Action = value;
                HomeAssistantHttpClient.PutAsync(string.Format("action/{0}/action={1}", Id, (int)value));
                NotifyPropertyChanged(nameof(ActionString));
            }
        }

        private Mode Mode
        {
            get
            {
                return ((MiKettleModel)DeviceModel).Mode;
            }
            set
            {
                if (((MiKettleModel)DeviceModel).Mode == value)
                {
                    return;
                }

                ((MiKettleModel)DeviceModel).Mode = value;
                HomeAssistantHttpClient.PutAsync(string.Format("action/{0}/mode={1}", Id, (int)value));
                NotifyPropertyChanged(nameof(Mode));
            }
        }

        public KeepWarmType KeepWarmType
        {
            get
            {
                return ((MiKettleModel)DeviceModel).KeepWarmType;
            }
            set
            {
                if (((MiKettleModel)DeviceModel).KeepWarmType == value)
                {
                    return;
                }

                ((MiKettleModel)DeviceModel).KeepWarmType = value;
                HomeAssistantHttpClient.PutAsync(string.Format("action/{0}/keep_warm/type={1}", Id, (int)value));
                NotifyPropertyChanged(nameof(KeepWarmType));
            }
        }

        private BoilMode BoilMode
        {
            get
            {
                return ((MiKettleModel)DeviceModel).BoilMode;
            }
            set
            {
                if (((MiKettleModel)DeviceModel).BoilMode == value)
                {
                    return;
                }

                ((MiKettleModel)DeviceModel).BoilMode = value;
                HomeAssistantHttpClient.PutAsync(string.Format("action/{0}/boil_mode={1}", Id, (int)value));
                NotifyPropertyChanged(nameof(BoilMode));
            }
        }

        public string ActionString
        {
            get
            {
                switch (Action)
                {
                    case Model.Devices.Action.Cooling: return "Cooling.";
                    case Model.Devices.Action.KeepingWarm: return
                            string.Format("Keeping warm for {0} minutes.", ((MiKettleModel)DeviceModel).KeepWarmTime);
                    case Model.Devices.Action.Heating: return "Heating.";
                    case Model.Devices.Action.Idle:
                    default: return "Idle.";
                }
            }
        }

        public MiKettleViewModel(IDeviceModel DeviceModel) : base(DeviceModel)
        {
        }

        public override void UpdateData()
        {
            Task.Run(async () => {
                dynamic deviceData = await HomeAssistantHttpClient.GetAsync(string.Format("action/{0}/*", Id));

                if (deviceData == null)
                {
                    return;
                }

                // Avoid setting values via properties which would cause PUT requests sent to server
                TemperatureCurrent = deviceData.temperature.current;
                TemperatureSet = deviceData.temperature.set;
                KeepWarmTimeLimit = deviceData.keep_warm.time_limit;
                KeepWarmType = (KeepWarmType)deviceData.keep_warm.type;
                Action = (Model.Devices.Action)deviceData.action;
                Mode = (Mode)deviceData.mode;
                BoilMode = (BoilMode)deviceData.boil_mode;
            });
        }
    }
}
