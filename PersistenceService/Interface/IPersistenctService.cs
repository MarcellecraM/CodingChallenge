using ParticipantService.Model;
using System.Collections.Generic;

namespace PersistenceService.Interface
{
    public interface IPersistenctService
    {
        bool AddParticipant(int TrainingSession, Participant participant);
        bool AddTrackedObjectSample(int TrainingSession, TrackedObjectSample[] trackeObjectSamples);
        bool Connect();
        IEnumerable<Participant> GetParticipants(int TrainingSession);
        IEnumerable<TrackedObjectSample> GetTrackedObjects(int TrainingSession, ulong startTime, ulong endTime);
        int SaveChanges();
    }
}