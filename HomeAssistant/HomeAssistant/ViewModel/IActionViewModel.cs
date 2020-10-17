using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HomeAssistant.ViewModel
{
    interface IActionViewModel : INotifyPropertyChanged
    {
        string Title { get; }
    }
}
