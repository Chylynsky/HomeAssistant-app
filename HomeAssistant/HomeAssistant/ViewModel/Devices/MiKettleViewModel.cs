using System;
using System.Threading.Tasks;
using HomeAssistant.Helper;
using HomeAssistant.Model.Devices;

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
                ((MiKettleModel)DeviceModel).TemperatureCurrent = deviceData.temperature.current;
                ((MiKettleModel)DeviceModel).TemperatureSet = deviceData.temperature.set;
                ((MiKettleModel)DeviceModel).KeepWarmTimeLimit = deviceData.keep_warm.time_limit;
                ((MiKettleModel)DeviceModel).KeepWarmType = (KeepWarmType)deviceData.keep_warm.type;
                ((MiKettleModel)DeviceModel).Action = (Model.Devices.Action)deviceData.action;
                ((MiKettleModel)DeviceModel).Mode = (Mode)deviceData.mode;
                ((MiKettleModel)DeviceModel).BoilMode = (BoilMode)deviceData.boil_mode;

                NotifyPropertyChanged(nameof(TemperatureCurrent));
                NotifyPropertyChanged(nameof(TemperatureSet));
                NotifyPropertyChanged(nameof(KeepWarmTimeLimit));
                NotifyPropertyChanged(nameof(KeepWarmType));
                NotifyPropertyChanged(nameof(Action));
                NotifyPropertyChanged(nameof(Mode));
                NotifyPropertyChanged(nameof(BoilMode));
            });
        }
    }
}
