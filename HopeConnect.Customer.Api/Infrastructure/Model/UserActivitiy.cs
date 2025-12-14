using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HopeConnect.Customer.Api.Infrastructure.Model
{
	public class UserActivitiy
	{
		[Key]
		public int Id { get; set; }
        public int? UserId { get; set; }
		[JsonIgnore]
		public User? User { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? City { get; set; }
		public string? Message { get; set; }
		public int? HelpType { get; set; }
		public int? RecipientId { get; set; }
		public double? DonationAmount { get; set; }
		[JsonIgnore]
		public DateTime CreateAt { get; set; }
    }
}
