using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Model
{
    public class TrackedObject
    {
        public TrackedObject(TrackedObject item)
        {
            Id = item.Id;
            Location = new Location(item.Location.Latitude,
                item.Location.Longitude,
                item.Location.Altitude);
        }

        public TrackedObject(int id, double latitude, double longitude)
        {
            Id = id;
            Location = new Location(latitude, longitude);
        }

        public int Id { get; set; }
        public Location Location { get; set; }
    }

}
