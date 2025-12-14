using System.Text.Json.Serialization;

namespace HopeConnect.Customer.Api.Infrastructure.Dto
{
	public class StrogeDto
	{
		public string? FileBase64 { get; set; }
		[JsonIgnore]
		public string? FileName { get; set; }
	}
}
