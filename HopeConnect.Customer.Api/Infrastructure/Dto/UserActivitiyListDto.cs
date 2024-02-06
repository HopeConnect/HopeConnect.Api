namespace HopeConnect.Customer.Api.Infrastructure.Dto
{
	public class UserActivitiyListDto
	{
        public int? UserId { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? City { get; set; }
		public string? Message { get; set; }
		public double? DonationAmount { get; set; }
	}
}
