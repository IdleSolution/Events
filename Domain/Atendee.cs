using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Atendee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public ICollection<ActivityAtendee> ActivityAtendees { get; set; }

    }
}