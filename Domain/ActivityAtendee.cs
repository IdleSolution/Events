using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ActivityAtendee
    {

        public Guid ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }

        public string AtendeeEmail { get; set; }
        [ForeignKey("AtendeeEmail")]
        public Atendee Atendee { get; set; }
    }
}