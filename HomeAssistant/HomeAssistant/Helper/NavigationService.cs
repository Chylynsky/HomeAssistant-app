using HomeAssistant.View;
using HomeAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

namespace HomeAssistant.Helper
{
    class NavigationService : INavigationService
    {
        public IThemedViewModelBase PreviousPageViewModel => throw new NotImplementedException();

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateBackAsync<TViewModel>() where TViewModel : IThemedViewModelBase
        {
            throw new NotImplementedException();
        }

        public async Task NavigateToAsync<TViewModel>() where TViewModel : IThemedViewModelBase
        {
            await InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : IThemedViewModelBase
        {
            throw new NotImplementedException();
        }

        public Task RemoveBackStackAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }
        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            ContentPage view = CreateView(viewModelType, parameter);

            if (view is LoginView)
            {
                Application.Current.MainPage = new NavigationPage(view);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as NavigationPage;

                if (navigationPage == null)
                {
                    Application.Current.MainPage = new NavigationPage(view);
                }
                else
                {
                    await navigationPage.PushAsync(view);
                }
            }

            view.BindingContext = Activator.CreateInstance(viewModelType);
        }

        private Type GetViewForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(
                        CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private ContentPage CreateView(Type viewModelType, object parameter)
        {
            Type viewType = GetViewForViewModel(viewModelType);

            if (viewType == null)
            {
                throw new InvalidOperationException($"Could not locate View for {viewModelType}");
            }

            return Activator.CreateInstance(viewType) as ContentPage;
        }

        private static readonly NavigationService NavigationServiceInstance = new NavigationService();

        public static NavigationService Instance 
        { 
            get 
            { 
                return NavigationServiceInstance; 
            } 
        }
    }
}
