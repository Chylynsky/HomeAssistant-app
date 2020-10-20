﻿using HomeAssistant.View;
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
        public async Task NavigateToAsync<TViewModel>(params object[] parameters) where TViewModel : IThemedViewModelBase
        {
            await InternalNavigateToAsync(typeof(TViewModel), parameters);
        }

        public async Task NavigateBackAsync()
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;

            await navigationPage.PopAsync();
        }

        private async Task InternalNavigateToAsync(Type viewModelType, params object[] parameters)
        {
            ContentPage view = CreateView(viewModelType);

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

            view.BindingContext = Activator.CreateInstance(viewModelType, parameters);
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

        public static NavigationService Navigation { get; } = new NavigationService();
    }
}