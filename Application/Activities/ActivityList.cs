using System;
using System.Collections.Generic;
using Domain;

namespace Application.Activities
{
    public class ActivityList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public ICollection<Atendee> Atendees { get; set; }
    }
}