using SixLabors.ImageSharp;
namespace HopeConnect.Customer.Api.Infrastructure.Utility
{
	public interface IImageUtility
	{
		Image Base64StringToImage(string base64String);
	}
	public class ImageUtility : IImageUtility
	{
		public Image Base64StringToImage(string base64String)
		{
			byte[] byteArray = Convert.FromBase64String(base64String);
			using (MemoryStream ms = new MemoryStream(byteArray))
			{
				Image image = Image.Load(ms);
				return image;
			}
		}
	}
}