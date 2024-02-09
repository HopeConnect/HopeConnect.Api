using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Cloud;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
	public interface IRecipientBusinessUnit
	{
		Task<Response> AddAsync(RecipientDto recipientDto);
		Task<Response<IList<RecipientListDto>>> TGetAllRecipientAsync();
		Task<Response<IList<RecipientListDto>>> GetRecipientByRecipientType(int recipientType);
		Task<Response<IList<RecipientCoordinationDto>>> TGetRecipientLatitudeAndLongitude();
	}
	public class RecipientBusinessUnit : IRecipientBusinessUnit
	{
		private readonly IRecipientDataAccess _recipientDataAccess;
		private readonly IGoogleCloudStroge _googleCloudStroge;
		private readonly IUserActivitiyBusinessUnit _userActivitiyBusinessUnit;
		public RecipientBusinessUnit(IRecipientDataAccess recipientDataAccess, IGoogleCloudStroge googleCloudStroge, IUserActivitiyBusinessUnit userActivitiyBusinessUnit)
		{
			_recipientDataAccess = recipientDataAccess;
			_googleCloudStroge = googleCloudStroge;
			_userActivitiyBusinessUnit = userActivitiyBusinessUnit;
		}
		public async Task<Response> AddAsync(RecipientDto recipientDto)
		{
			if (recipientDto == null)
			{
				return new Response(ResponseCode.BadRequest, "Recipient cannot be null");
			}
			var recipientEntity = new Recipient
			{
				Name = recipientDto.Name,
				RecipientType = recipientDto.RecipientType,
				Description = recipientDto.Description,
				Latitude = recipientDto.Latitude,
				Longitude = recipientDto.Longitude,
				Location = recipientDto.Location,
				Title = recipientDto.Title,
			};
			switch (recipientDto.RecipientType)
			{
				case 1:
					recipientDto.FolderName = "Food";
					break;
				case 2:
					recipientDto.FolderName = "Accommodation";
					break;
				case 3:
					recipientDto.FolderName = "Education";
					break;
				case 4:
					recipientDto.FolderName = "Clothe";
					break;
				default:
					recipientDto.FolderName = "Recipient";
					break;
			}
			recipientDto.ImageName = Guid.NewGuid().ToString() + ".png";
			if (recipientDto.Base64Image != null)
			{
				var imageName = await _googleCloudStroge.UploadImageWithBase64String(recipientDto.Base64Image, recipientDto.ImageName, recipientDto.FolderName);
				recipientEntity.ImageName = recipientDto.ImageName;
				recipientEntity.FolderName = recipientDto.FolderName;
			}
			var saveChangesValue = await _recipientDataAccess.AddAsync(recipientEntity);
			if (saveChangesValue > 0)
			{
				var userActivity = new UserActivitiy
				{
					DonationAmount = 0,
					Name = recipientDto.Name,
					Surname = recipientDto.Name,
					City = recipientDto.Location,
					Message = recipientDto.Description,
					RecipientId = saveChangesValue,
					HelpType = 2,
				};
				var response = await _userActivitiyBusinessUnit.TAddUserRecipientNotificationActivity(userActivity);
				if(response.ResponseCode != ResponseCode.Success)
				{
					return new Response(ResponseCode.BadRequest, "Recipient cannot be added");
				}
				return new Response(ResponseCode.Success, "Recipient added successfully");
			}
			return new Response(ResponseCode.BadRequest, "Recipient cannot be added");
		}
		public async Task<Response<IList<RecipientListDto>>> TGetAllRecipientAsync()
		{
			var recipientEntity = await _recipientDataAccess.GetAllRecipient();
			if (recipientEntity.Any())
			{
				return new Response<IList<RecipientListDto>>(ResponseCode.Success, "Recipient list Success.", recipientEntity);
			}
			return new Response<IList<RecipientListDto>>(ResponseCode.NotFound, "Recipient not found");
		}

		public async Task<Response<IList<RecipientListDto>>> GetRecipientByRecipientType(int recipientType)
		{
			if (recipientType == 0)
			{
				return new Response<IList<RecipientListDto>>(ResponseCode.BadRequest, "RecipientType cannot be null");
			}
			var recipientEntity = await _recipientDataAccess.GetRecipientByRecipientType(recipientType);

			if (recipientEntity.Any())
			{
				var recipientList = recipientEntity.ToList();
				foreach (var recipient in recipientList)
				{
					var recipientImageUrl = _googleCloudStroge.GenerateDownloadUrl(recipient.FolderName, recipient.ImageName);
					recipient.ImageUrl = recipientImageUrl;
				}
				return new Response<IList<RecipientListDto>>(ResponseCode.Success, "Recipient list Success.", recipientEntity);
			}
			return new Response<IList<RecipientListDto>>(ResponseCode.NotFound, "Recipient not found");

		}
		public async Task<Response<IList<RecipientCoordinationDto>>> TGetRecipientLatitudeAndLongitude()
		{
			var recipientEntity = await _recipientDataAccess.GetRecipientLatitudeAndLongitude();
			if (recipientEntity.Any())
			{
				return new Response<IList<RecipientCoordinationDto>>(ResponseCode.Success, "Recipient list Success.", recipientEntity);
			}
			return new Response<IList<RecipientCoordinationDto>>(ResponseCode.NotFound, "Recipient not found");
		}

	}
}
