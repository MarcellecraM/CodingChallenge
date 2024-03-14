using Microsoft.Maps.MapControl.WPF;

namespace MapViewer.ViewModel
{
    using System.Collections.Generic;

    public class MapViewItem : ViewModelBase
    {
        public int Id { get; }

        public MapViewItem(int id)
        {
            Id = id;
        }

        private string _marking = string.Empty;
        public string Marking
        {
            get => _marking;
            set { SetProperty(ref _marking, value); }
        }

        private Dictionary<string, string> _details = new Dictionary<string, string>();
        public Dictionary<string, string> Details
        {
            get => _details;
            set { SetProperty(ref _details, value); }
        }

        private Location _location = new Location();
        public Location Location
        {
            get => _location;
            set { SetProperty(ref _location, value); }
        }
    }

}
