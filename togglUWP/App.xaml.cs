using System;
using System.Threading.Tasks;
using TogglUWP.Services.SettingsServices;
using TogglUWP.ViewModels;
using TogglUWP.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace TogglUWP
{
    /// Documentation on APIs used in this page: https://github.com/Windows-XAML/TogglUWP/wiki

    sealed partial class App : TogglUWP.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion App settings
        }

        // runs even if restored from state
        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            var keys = PageKeys<Pages>();
            keys.Add(Pages.MainPage, typeof(MainPage));
            keys.Add(Pages.SettingsPage, typeof(SettingsPage));

            // content may already be shell when resuming
            if ((Window.Current.Content as Views.Shell) == null)
            {
                // setup hamburger shell
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                Window.Current.Content = new Views.Shell(nav);
            }
            return Task.CompletedTask;
        }

        // runs only when not restored from state
        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(Views.MainPage));
            return Task.CompletedTask;
        }
    }
}