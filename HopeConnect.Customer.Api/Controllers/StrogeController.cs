using Google.Cloud.Storage.V1;
using HopeConnect.Customer.Api.Infrastructure.Cloud;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HopeConnect.Customer.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StrogeController : ControllerBase
	{
		private readonly IGoogleCloudStroge _googleCloudStroge;

		public StrogeController(IGoogleCloudStroge googleCloudStroge)
		{
			_googleCloudStroge = googleCloudStroge;
		}
		[HttpGet]
		[Route("GetFile")]
		public async Task<IActionResult> GetFile(string fileName)
		{
			var client = StorageClient.Create();
			var stream = new MemoryStream();
			var obj = await client.DownloadObjectAsync("hopeconnect", fileName, stream);
			stream.Position = 0;
			return File(stream, obj.ContentType, fileName);
		}
		[HttpGet]
		[Route("GetFileUrl")]
		public async Task<IActionResult> GetFileUrl(string folderName, string fileName)
		{
			return Ok(_googleCloudStroge.GenerateDownloadUrl(folderName, fileName));
		}
		[HttpPost]
		[Route("UploadFile")]
		public async Task<IActionResult> UploadFile([FromBody] FileUpload file)
		{
			var client = StorageClient.Create();
			await client.UploadObjectAsync("hopeconnect", file.FileName, file.FileType, new MemoryStream(file.File));
			return Ok();
		}
		[HttpPost]
		[Route("UploadImageWithBase64String")]
		public async Task<string> UploadImageWithBase64String(string fileBase64, string fileName, string folderName)
		{
			return await _googleCloudStroge.UploadImageWithBase64String(fileBase64, fileName, folderName);
		}
		public class FileUpload
		{
			public string FileName { get; set; }
			public string FileType { get; set; }
			public byte[] File { get; set; }
		}
	}
}
