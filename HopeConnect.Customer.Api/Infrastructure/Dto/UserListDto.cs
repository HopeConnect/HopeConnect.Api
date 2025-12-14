namespace HopeConnect.Customer.Api.Infrastructure.Dto
{
	public class UserListDto
	{
		public string? FirebaseId { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? Age { get; set; }
		public string? UserImageName { get; set; }
		public string? UserImageUrl { get; set; }

	}
}
