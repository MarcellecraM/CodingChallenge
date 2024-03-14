using LocationService.Interface;
using LocationService.Model;
using LocationService.Service;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocationService.Tests;

public class LocationServiceImpl
{
    [Fact]
    public void LocationService_Ctor()
    {
        // Arrange
        var locationService = new Service.LocationServiceImpl();

        // Act
        locationService.StartTracking();
        locationService.StopTracking();

        // Assert
        Assert.NotNull(locationService);
    }

    [Fact]
    public void LocationService_AddOrUpdateTrackedObject()
    {
        // Arrange
        var locationService = new Service.LocationServiceImpl();
        locationService.StartTracking();
        
        ConcurrentDictionary<int, TrackedObject> received = new ConcurrentDictionary<int, TrackedObject>();
        var res = new AutoResetEvent(false);
        locationService.TrackedObjectChanged += (object? sender, TrackedObjectChangedEventArgs e) => 
        {
            foreach (var elem in e.TrackedObjects) received.TryAdd(elem.Id, elem);
            if (received.Count > 3) res.Set();
        };

        // Act
        locationService.AddOrUpdateTrackedObject(new TrackedObject(1, 1.0, 2.0));
        locationService.AddOrUpdateTrackedObject(new TrackedObject(2, 1.0, 2.0));
        locationService.AddOrUpdateTrackedObject(new TrackedObject(3, 1.0, 2.0));
        locationService.AddOrUpdateTrackedObject(new TrackedObject(4, 1.0, 2.0));

        // Assert
        res.WaitOne(1000);
        Assert.Equal(4, received.Count);
        locationService.StopTracking();
    }

    [Fact]
    public void LocationService_AddOrUpdateTrackedObjectMt()
    {
        // Arrange
        const int count = 100;
        const int taskCount = 10;
        const int total = taskCount * count;

        var locationService = new Service.LocationServiceImpl();
        locationService.StartTracking();

        ConcurrentDictionary<int, TrackedObject> received = new ConcurrentDictionary<int, TrackedObject>();
        var res = new AutoResetEvent(false);

        locationService.TrackedObjectChanged += (object? sender, TrackedObjectChangedEventArgs e) =>
        {
            foreach (var elem in e.TrackedObjects) received.TryAdd(elem.Id, elem);
            if (received.Count > total-1) res.Set();
        };

        // Act
        Task[] tasks = Enumerable.Range(0, taskCount).Select(taskIndex =>
        Task.Run(async () =>
        {
            await Task.Delay(20);
            for (int i = taskIndex * count; i < (taskIndex + 1) * count; i++)
            {
                locationService.AddOrUpdateTrackedObject(new TrackedObject(i, 1.0, 2.0));
            }
        })).ToArray();

        Task.WaitAll(tasks);

        // Assert
        res.WaitOne(5000);
        Assert.Equal(total, received.Count);
        locationService.StopTracking();
    }

}