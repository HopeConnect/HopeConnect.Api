namespace HopeConnect.Customer.Api.Infrastructure.Dto
{
	public class UserDonationArchiveListDto
	{
		public int RecipientId { get; set; }
		public string RecipientType { get; set; }
		public string ImageUrl { get; set; }
		public string Title { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string DonationDate { get; set; }
	}
}
