using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParticipantService.Model;

namespace ParticipantService.Interface
{
    public class ParticipantChangedEventArgs : EventArgs
        {
            public Participant Participant { get; private set; }

            public ParticipantChangedEventArgs(Participant participant)
            {
            Participant = participant;
            }
        }

}
