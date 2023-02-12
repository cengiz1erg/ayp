namespace AypWebAPI.Models.RequestModels
{
    public class UpdatePlayer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BackNumber { get; set; }
        public int? TeamId { get; set; }
    }
}
