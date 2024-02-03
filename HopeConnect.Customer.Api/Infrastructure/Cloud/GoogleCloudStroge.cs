
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace HopeConnect.Customer.Api.Infrastructure.Cloud
{
	public class GoogleCloudStroge : IGoogleCloudStroge
	{
		private readonly GoogleCredential _googleCredential;
		private readonly StorageClient _storageClient;
		private readonly string _bucketName;

		public GoogleCloudStroge(IConfiguration configuration)
		{
			_googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
			_storageClient = StorageClient.Create(_googleCredential);
			_bucketName = configuration["GoogleCloudStroge:BucketName"];
		}

		public string GenerateDownloadUrl(string folderName, string fileName)
		{
			return $"https://storage.googleapis.com/{_bucketName}/{folderName}/{fileName}";
		}

		public async Task<string> GetObjectName(string fileNameForStorage, string folderName)
		{
			return await Task.FromResult(string.IsNullOrWhiteSpace(folderName) ? fileNameForStorage : $"{folderName}/{fileNameForStorage}");
		}
	}
}
