namespace AypWebAPI.Models.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BackNumber { get; set; }
        public int? TeamId { get; set; }
    }
}
