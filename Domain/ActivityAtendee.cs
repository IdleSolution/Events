using System;

namespace Domain
{
    public class ActivityAtendee
    {
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public string AtendeeEmail { get; set; }
        public Atendee Atendee { get; set; }
    }
}