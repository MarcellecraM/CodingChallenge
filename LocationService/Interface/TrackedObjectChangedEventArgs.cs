using System;
using LocationService.Model;

namespace LocationService.Interface
{
    public class TrackedObjectChangedEventArgs : EventArgs
    {
        public TrackedObject[] TrackedObjects { get; private set; }

        public TrackedObjectChangedEventArgs(TrackedObject[] trackedObj)
        {
            TrackedObjects = trackedObj;
        }
    }

}
