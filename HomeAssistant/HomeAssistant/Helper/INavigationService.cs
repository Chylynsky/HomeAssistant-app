using HomeAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistant.Helper
{
    interface INavigationService
    {
        IThemedViewModelBase PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : IThemedViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : IThemedViewModelBase;
        Task NavigateBackAsync<TViewModel>() where TViewModel : IThemedViewModelBase;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
    }
}
