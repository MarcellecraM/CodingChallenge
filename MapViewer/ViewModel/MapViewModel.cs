using LocationService.Interface;
using LocationService.Model;
using ParticipantService.Interface;
using ParticipantService.Model;
using ParticipantService.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace MapViewer.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        public ObservableCollection<MapViewItem> MapViewItems { get; set; }
        private MapViewItem _selectedItem;
        private ILocationService _locationService;
        private IParticipantService _participantService;
        private Dispatcher _dispatcher;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand PushpinCommand { get; }

        public MapViewItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }            
        }

        public MapViewModel(ILocationService locService, ParticipantServiceImpl participantService)
        {
            _dispatcher = Application.Current.Dispatcher;
            _locationService = locService;
            _participantService = participantService;

            MapViewItems = new ObservableCollection<MapViewItem>();
            PushpinCommand = new RelayCommand(ShowDetails);
  
            _participantService.ParticipantAdded += OnParticipantAdded;
            _participantService.ParticipantChanged += OnParticipantChanged;
            _participantService.ParticipantRemoved += OnParticipantRemoved;

            _locationService.TrackedObjectChanged += OnLocationChanged;
        }

        private static void FillData(Participant participant, MapViewItem mapItem)
        {
            var details = new Dictionary<string, string>();
            details.Add(nameof(participant.Name), participant.Name);
            details.Add(nameof(participant.Rank), participant.Rank);
            details.Add(nameof(participant.Country), participant.Country);
            mapItem.Details = details;
            mapItem.Marking = $"S{participant.Id}";
        }

        private void OnParticipantAdded(object? sender, ParticipantChangedEventArgs e)
        {
            _dispatcher.Invoke(() =>
            {
                var mapItem = MapViewItems.FirstOrDefault(m => m.Id == e.Participant.Id);
                if (mapItem == null)
                {
                    mapItem = new MapViewItem(e.Participant.Id);
                    FillData(e.Participant, mapItem);
                    MapViewItems.Add(mapItem);
                }
                else
                {
                    Trace.WriteLine($"Participant with Id: {e.Participant.Id} already added.");
                }
            });
        }

        private void OnParticipantChanged(object? sender, ParticipantChangedEventArgs e)
        {
            _dispatcher.Invoke(() =>
            {
                var mapItem = MapViewItems.FirstOrDefault(m => m.Id == e.Participant.Id);
                if (mapItem != null)
                {
                    FillData(e.Participant, mapItem);
                }
                else
                {
                    Trace.WriteLine($"Participant with Id: {e.Participant.Id} not found.");
                }
            });
        }

        private void OnParticipantRemoved(object? sender, ParticipantChangedEventArgs e)
        {
            _dispatcher.Invoke(() =>
            {
                var mapItem = MapViewItems.FirstOrDefault(m => m.Id == e.Participant.Id);
                if (mapItem != null)
                {
                    MapViewItems.Remove(mapItem);
                }
                else
                {
                    Trace.WriteLine($"Participant with Id: {e.Participant.Id} not found.");
                }
            });
        }

        private static void FillData(TrackedObject trackedObject, MapViewItem mapItem)
        {
            var newLocation = trackedObject.Location;            
            mapItem.Location = new Location(newLocation.Latitude, newLocation.Longitude);
        }

        private void OnLocationChanged(object? sender, TrackedObjectChangedEventArgs e)
        {
            _dispatcher.Invoke( () =>
            {
                foreach (var to in e.TrackedObjects)
                {
                    var mapItem = MapViewItems.FirstOrDefault(e => e.Id == to.Id);
                    if (mapItem != null) 
                    {
                        FillData(to, mapItem);
                    }
                }
                // Ignore location update for unknon participants
            });
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void ShowDetails(object parameter)
        {
            var soldier = parameter as MapViewItem;
            // Logic to show details for the clicked soldier
            SelectedItem = soldier;
        }
    }

}