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
    class NavigationService
    {
        /// <summary>
        /// NavigationService singleton instance.
        /// </summary>
        public static NavigationService Navigation { get; } = new NavigationService();

        /// <summary>
        /// Navigate to the View corresponding to the given ViewModel.
        /// </summary>
        /// <param name="viewModel">ViewModel object.</param>
        /// <returns></returns>
        public async Task NavigateToAsync(IThemedViewModelBase viewModel)
        {
            var view = CreateView(viewModel.GetType());
            view.BindingContext = viewModel;
            await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(view);
        }

        /// <summary>
        /// Navigate to the View corresponding to the given ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel">Type of ViewModel to be navigated to.</typeparam>
        /// <returns></returns>
        public async Task NavigateToAsync<TViewModel>() where TViewModel : IThemedViewModelBase
        {
            await InternalNavigateToAsync(typeof(TViewModel), null);
        }

        /// <summary>
        /// Navigate to the View corresponding to the given ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel">Type of ViewModel to be navigated to.</typeparam>
        /// <param name="parameter">Parameter passed to the ViewModel constructor.</param>
        /// <returns></returns>
        public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : IThemedViewModelBase
        {
            await InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        /// <summary>
        /// Navigate to the View corresponding to the given ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel">Type of ViewModel to be navigated to.</typeparam>
        /// <param name="parameters">Arguments passed to ViewModel constructor.</param>
        /// <returns></returns>
        public async Task NavigateToAsync<TViewModel>(params object[] parameters) where TViewModel : IThemedViewModelBase
        {
            await InternalNavigateToAsync(typeof(TViewModel), parameters);
        }

        public async Task NavigateBackAsync()
        {
            await (Application.Current.MainPage as NavigationPage).Navigation.PopAsync();
        }

        private async Task InternalNavigateToAsync(Type viewModelType, params object[] parameters)
        {
            ContentPage view = CreateView(viewModelType);
            view.BindingContext = Activator.CreateInstance(viewModelType, parameters);

            if (!(Application.Current.MainPage is NavigationPage))
            {
                Application.Current.MainPage = new NavigationPage(view);
                return;
            }

            await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(view);
        }

        private Type GetViewForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;

            var viewAssemblyName = string.Format(
                        CultureInfo.InvariantCulture, 
                        "{0}, {1}", 
                        viewName, 
                        viewModelAssemblyName);

            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private ContentPage CreateView(Type viewModelType)
        {
            Type viewType = GetViewForViewModel(viewModelType);

            if (viewType == null)
            {
                throw new InvalidOperationException($"Could not locate View for {viewModelType}");
            }

            return Activator.CreateInstance(viewType) as ContentPage;
        }
    }
}
