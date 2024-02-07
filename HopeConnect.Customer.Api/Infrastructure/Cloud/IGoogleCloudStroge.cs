using SixLabors.ImageSharp;
namespace HopeConnect.Customer.Api.Infrastructure.Cloud
{
	public interface IGoogleCloudStroge
	{
		Task<string> GetObjectName(string fileNameForStorage, string folderName);
		string GenerateDownloadUrl(string folderName, string fileName);
		Task<string> UploadImageWithBase64String(string fileBase64, string fileName, string folderName);
		Task<string> UploadImageAsync(Image imageFile, string fileNameForStorage, string folderName = "");
		Task<string> UploadMemoryStreamForMenuAsync(MemoryStream memoryStream, string fileNameForStorage, string folderName = "");
		Task DeleteObjectAsync(string fileNameForStorage, string folderName);
	}
}
