using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TogglUWP.Views
{
    public sealed partial class SettingsPage : Page
    {
        TogglUWP.Services.SerializationService.ISerializationService _SerializationService;

        public SettingsPage()
        {
            InitializeComponent();
            _SerializationService = TogglUWP.Services.SerializationService.SerializationService.Json;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var index = _SerializationService.Deserialize<int>(e.Parameter?.ToString());
            MyPivot.SelectedIndex = index;
        }
    }
}
