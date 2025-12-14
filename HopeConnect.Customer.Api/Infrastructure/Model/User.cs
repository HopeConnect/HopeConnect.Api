using System.ComponentModel.DataAnnotations;

namespace HopeConnect.Customer.Api.Infrastructure.Model
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string? FirebaseUserId { get; set; }
		public string? Email { get; set; }
		public string? FullName { get; set; }
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? Age { get; set; }
		public string? FolderName { get; set; }
		public string? ImageName { get; set; }
	}
}
