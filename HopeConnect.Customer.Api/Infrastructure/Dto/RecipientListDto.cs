namespace HopeConnect.Customer.Api.Infrastructure.Dto
{
	public class RecipientListDto
    {
        public int Id { get; set; }
		public string FolderName { get; set; }
		public string ImageName { get; set; }
		public string Title { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public int RecipientId { get; set; }
		public string RecipientType { get; set; }
		public string ImageUrl { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
    }
}