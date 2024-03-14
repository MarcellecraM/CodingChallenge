using ParticipantService.Model;

namespace ParticipantService.Interface
{
    public interface IParticipantService
    {
        event EventHandler<ParticipantChangedEventArgs> ParticipantAdded;
        event EventHandler<ParticipantChangedEventArgs> ParticipantChanged;
        event EventHandler<ParticipantChangedEventArgs> ParticipantRemoved;

        void AddParticipant(Participant participant);
        void RemoveParticipant(Participant participant);
    }
}