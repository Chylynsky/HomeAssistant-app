using HomeAssistant.Model;
using HomeAssistant.View.DeviceViews;
using System;
using System.Collections.Generic;
using System.Text;

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

        public string Action
        {
            get
            {
                switch (((MiKettleModel)deviceModel).Action)
                {
                    case Model.Action.Cooling: return "Cooling";
                    case Model.Action.KeepingWarm: return 
                            string.Format("Keeping warm for {0} minutes", ((MiKettleModel)deviceModel).KeepWarmTime);
                    case Model.Action.Heating: return "Heating";
                    case Model.Action.Idle:
                    default: return "Idle";
                }
            }
        }

        public MiKettleViewModel(DeviceModelBase deviceModel) : base(deviceModel)
        {
            TemperatureCurrent = 60;
            TemperatureSet = TemperatureCurrent;
            KeepWarmTimeLimit = 3.0f;
        }
    }
}
