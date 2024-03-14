using LocationService.Emulator;
using LocationService.Interface;
using LocationService.Service;
using MapViewer.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using ParticipantService.Model;
using ParticipantService.Service;
using System.Windows.Controls;
using System.Windows.Input;

namespace MapViewer.View
{
    /// <summary>
    /// Interaction logic for MapViewer.xaml
    /// </summary>
    public partial class MapViewer : UserControl
    {
        private ILocationProviderAdapter _locationProvider;
        private const int ParticipantCount = 1000;
        public MapViewer()
        {
            InitializeComponent();

            ILocationService locationService = new LocationServiceImpl();
            var participantService = new ParticipantServiceImpl();

            _locationProvider = new LocationProviderAdapterEmu(locationService, ParticipantCount);
            _locationProvider.Start();

            // Set the DataContext programmatically
            var mapViewModel = new MapViewModel(locationService, participantService);

            DataContext = mapViewModel;

            for (int i = 0; i < ParticipantCount; i += 2)
            {
                participantService.AddParticipant(new Participant(i) { Name = "John Doe", Rank = "Private", Country = "USA" });
                participantService.AddParticipant(new Participant(i + 1) { Name = "Jane Doe", Rank = "Corporal", Country = "USA" });
            }

            locationService.StartTracking();
        }

        private void Pushpin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pushpin = sender as Pushpin;
            if (pushpin != null && pushpin.DataContext is MapViewItem mapViewItem)
            {
                // Assuming your ViewModel is accessible as the DataContext of the Window or a parent container
                var viewModel = DataContext as MapViewModel;
                viewModel?.PushpinCommand.Execute(mapViewItem);

                e.Handled = true; // Mark the event as handled
            }
        }
    }
}
