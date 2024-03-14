namespace ParticipantService.Model
{
    public class Participant
    {
        public Participant(int id)
        {
            Id = id;
            Name = string.Empty;
            Rank = string.Empty;
            Country = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Country { get; set; }
    }
}