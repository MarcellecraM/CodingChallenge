using System;
using System.Collections.Generic;
using LocationService.Interface;
using LocationService.Model;
using System.Threading;
using Timer = System.Threading.Timer;

namespace LocationService.Emulator
{
    /// <summary>
    /// Emulator class generates tracked object updates
    /// Number of items can be passed on the Ctor
    /// </summary>
    public class LocationProviderAdapterEmu : ILocationProviderAdapter
    {
        ILocationService _locationService;
        List<TrackedObject> _trackedObjects = new List<TrackedObject>();
        private Timer? _timer;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="locationService">LocationService to fed emu data in</param>
        /// <param name="count">Number of elemet to be emulated</param>
        public LocationProviderAdapterEmu(ILocationService locationService, int count)
        {
            _locationService = locationService;
            for (int i = 0; i < count; i+=2)
            {
                _trackedObjects.Add(new TrackedObject(i, 46.77522, 7.6537));
                _trackedObjects.Add(new TrackedObject(i+1, 40.7128, -74.0060));
            }
        }

        private void ProvideLocation(object? state)
        {
            var rnd = new Random();
            foreach (var item in _trackedObjects)
            {
                // Simulate location change
                item.Location.Latitude += rnd.NextDouble() * 0.01 - 0.005;
                item.Location.Longitude += rnd.NextDouble() * 0.01 - 0.005;

                // Raise the LocationChanged event
                _locationService.AddOrUpdateTrackedObject(new TrackedObject(item));
            }
        }

        /// <summary>
        /// Start processing updates and notify bulk updates
        /// </summary>
        public void Start()
        {
            TimerCallback timerCallback = new TimerCallback(ProvideLocation);
            _timer = new Timer(timerCallback, null, 0, 500); // Update every 500 milliseconds 
        }

        /// <summary>
        /// Stop processing notification.
        /// </summary>
        public void Stop()
        {
            _timer?.Dispose();
        }
    }

}
