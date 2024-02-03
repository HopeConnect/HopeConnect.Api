namespace HopeConnect.Customer.Api.Infrastructure.Cloud
{
	public interface IGoogleCloudStroge
	{
		Task<string> GetObjectName(string fileNameForStorage, string folderName);
		string GenerateDownloadUrl(string folderName, string fileName);
	}
}
