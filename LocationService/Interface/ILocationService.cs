using LocationService.Model;
using System;
using System.Timers;

namespace LocationService.Interface
{
    public interface ILocationService
    {
        event EventHandler<TrackedObjectChangedEventArgs>? TrackedObjectAdded;
        event EventHandler<TrackedObjectChangedEventArgs>? TrackedObjectChanged;
        event EventHandler<TrackedObjectChangedEventArgs>? TrackedObjectRemoved;

        void AddOrUpdateTrackedObject(TrackedObject trackedObj);
        void Clear();
        void StartTracking();
        void StopTracking();
    }
}