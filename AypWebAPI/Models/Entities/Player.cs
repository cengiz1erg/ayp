using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AypWebAPI.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BackNumber { get; set; } 
        public DateTime? SavedDate { get; set; }
        public DateTime? UpdatedDate { get; set; } 

        public int? TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
