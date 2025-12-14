namespace HopeConnect.Customer.Api.Infrastructure.Model
{
	public class Recipient
	{
		public int Id { get; set; }
		public string FolderName { get; set; }
		public string ImageName { get; set; }
		public string Title { get; set; }
		public string Name { get; set; } 
		public string Location { get; set; }
		public string Description { get; set; }
		public int RecipientType { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
	}
}
