using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using HopeConnect.Customer.Api.Infrastructure.Utility;
using SixLabors.ImageSharp;
namespace HopeConnect.Customer.Api.Infrastructure.Cloud
{
	public class GoogleCloudStroge : IGoogleCloudStroge
	{
		private readonly GoogleCredential _googleCredential;
		private readonly StorageClient _storageClient;
		private readonly string _bucketName;
		private readonly IImageUtility _imageUtility;

		public GoogleCloudStroge(IConfiguration configuration, IImageUtility imageUtility)
		{
			_googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
			_storageClient = StorageClient.Create(_googleCredential);
			_bucketName = configuration["GoogleCloudStroge:BucketName"];
			_imageUtility = imageUtility;
		}

		public string GenerateDownloadUrl(string folderName, string fileName)
		{
			return $"https://storage.googleapis.com/{_bucketName}/{folderName}/{fileName}";
		}
		public async Task<string> GetObjectName(string fileNameForStorage, string folderName)
		{
			return await Task.FromResult(string.IsNullOrWhiteSpace(folderName) ? fileNameForStorage : $"{folderName}/{fileNameForStorage}");
		}
		public async Task<string> UploadImageWithBase64String(string fileBase64, string fileName, string folderName)
		{
			var startIndex = fileBase64.IndexOf(",") + 1;
			var base64String = fileBase64.Substring(startIndex);
			using (var image = _imageUtility.Base64StringToImage(base64String))
			{
				var addCloud = UploadImageAsync(image, fileName, folderName).Result;
				return addCloud;
			}
		}
		public async Task<string> UploadImageAsync(Image imageFile, string fileNameForStorage, string folderName = "")
		{
			await using var memoryStream = new MemoryStream();
			imageFile.Save(memoryStream, imageFile.Metadata.DecodedImageFormat);
			return await UploadMemoryStreamForMenuAsync(memoryStream, fileNameForStorage, folderName);
		}
		public async Task<string> UploadMemoryStreamForMenuAsync(MemoryStream memoryStream, string fileNameForStorage, string folderName = "")
		{
			var dataObject = await _storageClient.UploadObjectAsync(_bucketName, await GetObjectName(fileNameForStorage, folderName), "image/png", memoryStream);
			return dataObject.Name;
		}

		public async Task DeleteObjectAsync(string fileNameForStorage, string folderName)
		{
			await _storageClient.DeleteObjectAsync(_bucketName, await GetObjectName(fileNameForStorage, folderName));
		}
	}
}
