using HomeAssistant.Model;
using HomeAssistant.ViewModel.DeviceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View.DeviceViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiKettleView : ContentView
    {
        public static readonly BindableProperty KeepWarmTypeProperty = BindableProperty.Create(
            nameof(KeepWarmType),
            typeof(KeepWarmType),
            typeof(MiKettleView),
            default(KeepWarmType),
            BindingMode.TwoWay);

        public KeepWarmType KeepWarmType
        {
            get
            {
                return (KeepWarmType)GetValue(KeepWarmTypeProperty);
            }
            set
            {
                SetValue(KeepWarmTypeProperty, value);
            }
        }

        public MiKettleView()
        {
            InitializeComponent();
            SetBinding(KeepWarmTypeProperty, new Binding("KeepWarmType"));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == KeepWarmTypeProperty.PropertyName)
            {
                keepWarmTypePicker.SelectedIndex = (int)KeepWarmType;
            }
        }

        private void keepWarmTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeepWarmType = (KeepWarmType)((Picker)sender).SelectedIndex;
        }
    }
}