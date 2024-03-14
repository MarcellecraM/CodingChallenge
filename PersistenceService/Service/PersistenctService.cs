using ParticipantService.Model;
using PersistenceService.Database;
using PersistenceService.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersistenceService.Service
{
    public class PersistenctService : IPersistenctService
    {
        ApplicationDbContext _dbCtxt;

        public PersistenctService()
        {
        }

        public bool Connect()
        {
            _dbCtxt = new ApplicationDbContext();

            return true;
        }

        public IEnumerable<Participant> GetParticipants(int TrainingSession)
        {
            return _dbCtxt.Participants;
        }

        public bool AddParticipant(int TrainingSession, Participant participant)
        {
            // TODO check training session
            _dbCtxt.Participants.Add(participant);
            return true;
        }

        public IEnumerable<TrackedObjectSample> GetTrackedObjects(int TrainingSession, ulong startTime, ulong endTime)
        {
            return _dbCtxt.TrackedObjectSamples;
        }

        public bool AddTrackedObjectSample(int TrainingSession, TrackedObjectSample[] trackeObjectSamples)
        {
            // TODO check training session
            _dbCtxt.TrackedObjectSamples.AddRange(trackeObjectSamples);
            return true;
        }

        public int SaveChanges()
        {
            return _dbCtxt.SaveChanges();
        }
    }
}
