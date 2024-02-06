namespace HopeConnect.Customer.Api.Infrastructure.Dto
{
	public class UserDonationArchiveRequestDto
	{
		public List<UserDonationArchiveListDto> UserDonationArchiveFoodList { get; set; }
		public List<UserDonationArchiveListDto> UserDonationArchiveClothesList { get; set; }
		public List<UserDonationArchiveListDto> UserDonationArchiveAccommodationList { get; set; }
		public List<UserDonationArchiveListDto> UserDonationArchiveEducationList { get; set; }
	}
}
