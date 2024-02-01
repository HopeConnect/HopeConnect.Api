using System.ComponentModel.DataAnnotations;

namespace HopeConnect.Customer.Api.Infrastructure.Model
{
    public class Accommodation
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
