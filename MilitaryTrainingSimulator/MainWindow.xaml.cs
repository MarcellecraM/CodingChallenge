using LocationService.Emulator;
using LocationService.Interface;
using LocationService.Service;
using MapViewer.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using ParticipantService.Model;
using ParticipantService.Service;
using System.Windows;
using System.Windows.Input;

namespace MilitaryTrainingSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILocationProviderAdapter _locationProvider;
        private const int ParticipantCount = 1000;
        public MainWindow()
        {
            InitializeComponent();
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
