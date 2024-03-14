using ParticipantService.Interface;
using ParticipantService.Model;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ParticipantService.Service
{
    public class ParticipantServiceImpl : IParticipantService
    {
        public event EventHandler<ParticipantChangedEventArgs>? ParticipantAdded;
        public event EventHandler<ParticipantChangedEventArgs>? ParticipantChanged;
        public event EventHandler<ParticipantChangedEventArgs>? ParticipantRemoved;

        private readonly ConcurrentDictionary<int, Participant> _participants = new ConcurrentDictionary<int, Participant>();

        public ParticipantServiceImpl()
        {
        }


        public void AddParticipant(Participant participant)
        {
            if (_participants.TryAdd(participant.Id, participant))
            {
                ParticipantAdded?.Invoke(this, new ParticipantChangedEventArgs(participant));
            }
            else 
            {
                Trace.WriteLine("Could not add participant, because key already exists");
            }
        }

        public void UpdateParticipant(Participant participant)
        {
            bool added = false;
            _participants.AddOrUpdate(participant.Id, participant, (key, val) => { added = true;  return participant; });

            if (added)
            {
                Trace.WriteLine("Participant has been added, even though update was called");
            }
        }

        public void RemoveParticipant(Participant participant)
        {
            if (_participants.TryRemove(participant.Id, out _))
            {
                ParticipantRemoved?.Invoke(this, new ParticipantChangedEventArgs(participant));
            }
            else
            {
                Trace.WriteLine("Could not remove participant, because key does not exists");
            }
        }
    }
}