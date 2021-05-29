using System.Collections.Generic;

namespace Domain
{
    public class Atendee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<ActivityAtendee> Activities { get; set; }



    }
}