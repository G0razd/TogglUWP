using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using TogglUWP.Mvvm;
using TogglUWP.Services.SettingsService;
using Windows.UI.Xaml;

namespace TogglUWP.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPartViewModel SettingsPartViewModel { get; } = new SettingsPartViewModel();
        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();
    }

    public class SettingsPartViewModel : ViewModelBase
    {
        private Services.SettingsServices.SettingsService _settings;

        public SettingsPartViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime
            }
            else
            {
                _settings = Services.SettingsServices.SettingsService.Instance;
            }
        }

        public bool UseShellBackButton
        {
            get { return _settings.UseShellBackButton; }
            set { _settings.UseShellBackButton = value; base.RaisePropertyChanged(); }
        }

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set { _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; base.RaisePropertyChanged(); }
        }

        private string _BusyText = "Please wait...";

        public string BusyText
        {
            get
            {
                return _BusyText;
            }
            set
            {
                Set(ref _BusyText, value);
            }
        }

        private RelayCommand _showBusyCommand;

        /// <summary>
        /// Gets the ShowBusyCommand.
        /// </summary>
        public RelayCommand ShowBusyCommand
        {
            get
            {
                return _showBusyCommand
                    ?? (_showBusyCommand = new RelayCommand(ExecuteShowBusyCommand));
            }
        }

        private async void ExecuteShowBusyCommand()
        {
            Views.Shell.SetBusy(true, _BusyText);
            await Task.Delay(5000);
            Views.Shell.SetBusy(false);
        }
    }

    public class AboutPartViewModel : ViewModelBase
    {
        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

        public Uri RateMe => new Uri("http://bing.com");
    }
}