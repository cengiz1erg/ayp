using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AypWebAPI.Models.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Player>? Players { get; set; }
    }
}
