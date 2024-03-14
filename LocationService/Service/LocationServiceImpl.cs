using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using LocationService.Interface;
using LocationService.Model;
using System.Threading.Tasks.Dataflow;
using System.Linq;
using System.Threading;
using Timer = System.Threading.Timer;

namespace LocationService.Service
{

    public class LocationServiceImpl : ILocationService
    {
        private readonly ConcurrentDictionary<int, TrackedObject> _trackedObjects = new ConcurrentDictionary<int, TrackedObject>();
        private readonly BufferBlock<KeyValuePair<int, TrackedObject>> _updateBuffer = new BufferBlock<KeyValuePair<int, TrackedObject>>();
        private Timer _timer;

        public event EventHandler<TrackedObjectChangedEventArgs>? TrackedObjectAdded;
        public event EventHandler<TrackedObjectChangedEventArgs>? TrackedObjectChanged;
        public event EventHandler<TrackedObjectChangedEventArgs>? TrackedObjectRemoved;

        public LocationServiceImpl()
        {
        }

        private void ProcessLocationUpdate(object? state)
        {
            if (_updateBuffer.TryReceiveAll(out var buffer))
            {
                TrackedObjectChanged?.Invoke(this, new TrackedObjectChangedEventArgs(buffer.Select(e => e.Value).ToArray()));
            }
        }

        public void StartTracking()
        {
            TimerCallback timerCallback = new TimerCallback(ProcessLocationUpdate);
            _timer = new Timer(timerCallback, null, 0, 500); // Update every 500 milliseconds 
        }

        public void StopTracking()
        {
            _timer.Dispose();
        }

        /// <summary>
        /// Add or update the tracked object in the colletion 
        /// </summary>
        /// <param name="trackedObj">New update from the location provider.</param>
        public void AddOrUpdateTrackedObject(TrackedObject trackedObj)
        {
            _trackedObjects[trackedObj.Id] = trackedObj;
            _updateBuffer.Post(new KeyValuePair<int, TrackedObject>(trackedObj.Id, trackedObj));
        }

        public void Clear()
        {
            _trackedObjects.Clear();
            _updateBuffer.TryReceiveAll(out _);
        }
    }

}
