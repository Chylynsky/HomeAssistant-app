using HomeAssistant.Helper;
using HomeAssistant.Model;
using HomeAssistant.View.DeviceViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistant.ViewModel.DeviceViewModels
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
                return ((MiKettleModel)deviceModel).TemperatureSet;
            }
            set
            {
                ((MiKettleModel)deviceModel).TemperatureSet = value;
                HomeAssistantClient.PutAsync(string.Format("action/{0}/temperature/set={1}", Id, value));
                NotifyPropertyChanged(nameof(TemperatureSet));
            }
        }

        public int TemperatureCurrent
        {
            get
            {
                return ((MiKettleModel)deviceModel).TemperatureCurrent;
            }
            set
            {
                ((MiKettleModel)deviceModel).TemperatureCurrent = value;
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
                return ((MiKettleModel)deviceModel).KeepWarmTimeLimit;
            }
            set
            {
                ((MiKettleModel)deviceModel).KeepWarmTimeLimit = (float)Math.Round(value * 2.0f, MidpointRounding.AwayFromZero) / 2.0f;
                HomeAssistantClient.PutAsync(string.Format("action/{0}/keep_warm/time_limit={1}", Id, value));
                NotifyPropertyChanged(nameof(KeepWarmTimeLimit));
            }
        }

        public float KeepWarmTime
        {
            get
            {
                return ((MiKettleModel)deviceModel).KeepWarmTime;
            }
        }

        private Model.Action Action
        {
            get
            {
                return ((MiKettleModel)deviceModel).Action;
            }
            set
            {
                ((MiKettleModel)deviceModel).Action = value;
                HomeAssistantClient.PutAsync(string.Format("action/{0}/action={1}", Id, (int)value));
                NotifyPropertyChanged(nameof(ActionString));
            }
        }

        private Mode Mode
        {
            get
            {
                return ((MiKettleModel)deviceModel).Mode;
            }
            set
            {
                ((MiKettleModel)deviceModel).Mode = value;
                HomeAssistantClient.PutAsync(string.Format("action/{0}/mode={1}", Id, (int)value));
                NotifyPropertyChanged(nameof(Mode));
            }
        }

        public KeepWarmType KeepWarmType
        {
            get
            {
                return ((MiKettleModel)deviceModel).KeepWarmType;
            }
            set
            {
                ((MiKettleModel)deviceModel).KeepWarmType = value;
                HomeAssistantClient.PutAsync(string.Format("action/{0}/keep_warm/type={1}", Id, (int)value));
                NotifyPropertyChanged(nameof(KeepWarmType));
            }
        }

        private BoilMode BoilMode
        {
            get
            {
                return ((MiKettleModel)deviceModel).BoilMode;
            }
            set
            {
                ((MiKettleModel)deviceModel).BoilMode = value;
                HomeAssistantClient.PutAsync(string.Format("action/{0}/boil_mode={1}", Id, (int)value));
                NotifyPropertyChanged(nameof(BoilMode));
            }
        }

        public string ActionString
        {
            get
            {
                switch (Action)
                {
                    case Model.Action.Cooling: return "Cooling.";
                    case Model.Action.KeepingWarm: return
                            string.Format("Keeping warm for {0} minutes.", ((MiKettleModel)deviceModel).KeepWarmTime);
                    case Model.Action.Heating: return "Heating.";
                    case Model.Action.Idle:
                    default: return "Idle.";
                }
            }
        }

        public string KeepWarmTypeString
        {
            get
            {
                switch (KeepWarmType)
                {
                    case KeepWarmType.BoilAndCool: return "Boil then cool down to set temperature.";
                    case KeepWarmType.HeatUp: return "Heat up to set temperature.";
                    default: return "";
                }
            }
        }

        public MiKettleViewModel(DeviceModelBase deviceModel) : base(deviceModel)
        {
            Task.Run(async () => {
                dynamic deviceData = await HomeAssistantClient.GetAsync(string.Format("action/{0}/*", Id));

                if (deviceData == null)
                {
                    return;
                }

                TemperatureCurrent = deviceData.temperature.current;
                TemperatureSet = deviceData.temperature.set;
                KeepWarmTimeLimit = deviceData.keep_warm.time_limit;
                KeepWarmType = (KeepWarmType)deviceData.keep_warm.type;
                Action = (Model.Action)deviceData.action;
                Mode = (Mode)deviceData.mode;
                BoilMode = (BoilMode)deviceData.boil_mode;
            });
        }
    }
}
