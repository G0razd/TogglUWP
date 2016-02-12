using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace togglUWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        private string _Value = string.Empty;
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.Any())
            {
                Value = state[nameof(Value)]?.ToString();
                state.Clear();
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                state[nameof(Value)] = Value;
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }

        private RelayCommand _gotoSettings;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand MyCommand
        {
            get
            {
                return _gotoSettings
                    ?? (_gotoSettings = new RelayCommand(ExecuteGotoSettings));
            }
        }

        private void ExecuteGotoSettings()
        {
            NavigationService.Navigate(Pages.MainPage, 0);
        }

        private RelayCommand _gotoPrivacy;

        /// <summary>
        /// Gets the GotoPrivacy.
        /// </summary>
        public RelayCommand GotoPrivacy
        {
            get
            {
                return _gotoPrivacy
                    ?? (_gotoPrivacy = new RelayCommand(ExecuteGotoPrivacy));
            }
        }

        private void ExecuteGotoPrivacy()
        {
            NavigationService.Navigate(Pages.SettingsPage, 1);
        }

        private RelayCommand _gotoAbout;

        /// <summary>
        /// Gets the GotoAbout.
        /// </summary>
        public RelayCommand GotoAbout
        {
            get
            {
                return _gotoAbout
                    ?? (_gotoAbout = new RelayCommand(ExecuteGotoAbout));
            }
        }

        private void ExecuteGotoAbout()
        {
            NavigationService.Navigate(Pages.SettingsPage, 2);
        }

        //public void GotoSettings() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        //public void GotoPrivacy() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        //public void GotoAbout() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 2);
    }
}