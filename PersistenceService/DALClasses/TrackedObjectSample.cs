using LocationService.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceService
{
    public class TrackedObjectSample
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SampleId { get; set; }
        public ulong Timestamp { get; set; }

        public TrackedObject TrackedObject { get; set; }

    }

}
