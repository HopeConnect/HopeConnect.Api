using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Cloud;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Infrastructure.Utility;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
	public interface IUserBusinessUnit
	{
		Task<Response> TAddAsync(User user);
		Task<Response> TUpdateAsync(User user);
		Task<Response> TDeleteByFirebaseUserIdAsync(string firebaseUserId);
		Task<Response<UserListDto>> TGetUserByUserFirebaseIdAsync();
		Task<Response<IList<User>>> TGetAllUserAsync();
		Task<Response<int>> TGetUserIdByUserFirebaseIdAsync();
		Task<Response> TUpdateUserImageAsync(UserImageUploadDto userImageUploadDto);
	}
	public class UserBusinessUnit : IUserBusinessUnit
	{
		private readonly IUserDataAccess _userDataAccess;
		private readonly IUserUtility _userUtility;
		private readonly IGoogleCloudStroge _googleCloudStroge;
		public UserBusinessUnit(IUserDataAccess userDataAccess, IUserUtility userUtility, IGoogleCloudStroge googleCloudStroge)
		{
			_userDataAccess = userDataAccess;
			_userUtility = userUtility;
			_googleCloudStroge = googleCloudStroge;
		}
		public async Task<Response> TAddAsync(User user)
		{
			if (user == null)
			{
				return new Response(ResponseCode.BadRequest, "User cannot be null");
			}
			user.FolderName = "UserImage";
			user.ImageName = "UserProfileImage.png";
			var saveChangesValue = await _userDataAccess.AddAsync(user);
			if (saveChangesValue > 0)
			{
				return new Response(ResponseCode.Success, "User added successfully");
			}
			return new Response(ResponseCode.BadRequest, "User cannot be added");

		}
		public async Task<Response> TDeleteByFirebaseUserIdAsync(string firebaseUserId)
		{
			if (string.IsNullOrEmpty(firebaseUserId))
			{
				return new Response(ResponseCode.BadRequest, "Firebase user id is empty");
			}
			var userEntity = await _userDataAccess.GetUserIdAsync(firebaseUserId);
			if (userEntity == null)
			{
				return new Response(ResponseCode.NotFound, "User not found");
			}
			var saveChangesValue = await _userDataAccess.DeleteAsync(userEntity);
			if (saveChangesValue > 0)
			{
				return new Response(ResponseCode.Success, "User deleted successfully");
			}
			return new Response(ResponseCode.BadRequest, "User not deleted");
		}

		public async Task<Response<IList<User>>> TGetAllUserAsync()
		{
			var userEntity = await _userDataAccess.GetAllUser();
			if (userEntity.Any())
			{
				return new Response<IList<User>>(ResponseCode.Success, "User list Success.",userEntity);
			}
			return new Response<IList<User>>(ResponseCode.NotFound, "Users not found");
		}

		public async Task<Response<UserListDto>> TGetUserByUserFirebaseIdAsync()
		{
			var firebaseUserId = _userUtility.GetFirebaseUserId();
			if (string.IsNullOrEmpty(firebaseUserId))
			{
				return new Response<UserListDto>(ResponseCode.BadRequest, "Firebase user id cannot be empty");
			}
			var userEntity = await _userDataAccess.GetUserByUserFirebaseIdAsync(firebaseUserId);
			if (userEntity != null)
			{
				var userImageUrl = _googleCloudStroge.GenerateDownloadUrl("UserImage", userEntity.UserImageName);
				if(userImageUrl == null)
				{
					return new Response<UserListDto>(ResponseCode.NotFound, "User Image Not Found!");
				}
				userEntity.UserImageUrl = userImageUrl;
				return new Response<UserListDto>(ResponseCode.Success, userEntity);
			}
			return new Response<UserListDto>(ResponseCode.NotFound, "User not found");
		}

		public async Task<Response<int>> TGetUserIdByUserFirebaseIdAsync()
		{
			var firebaseUserId = _userUtility.GetFirebaseUserId();
			if (string.IsNullOrEmpty(firebaseUserId))
			{
				return new Response<int>(ResponseCode.BadRequest, "Firebase user id cannot be empty");
			}
			var user = await _userDataAccess.GetUserIdAsync(firebaseUserId);
			if(user != null )
			{	
				return new Response<int>(ResponseCode.Success,user.Id);
			}
			return new Response<int>(ResponseCode.NotFound, "User Id not found!");
		}

		public async Task<Response> TUpdateAsync(User user)
		{
			if (user == null)
			{
				return new Response(ResponseCode.BadRequest, "User cannot be null");
			}
			var userFirebaseId = _userUtility.GetFirebaseUserId();
			if (string.IsNullOrEmpty(userFirebaseId))
			{
				return new Response(ResponseCode.BadRequest, "Firebase user id cannot be empty");
			}
			var userEntity = await _userDataAccess.GetUserIdAsync(userFirebaseId);
			if (userEntity == null)
			{
				return new Response(ResponseCode.NotFound, "User not found");
			}
			userEntity.FirebaseUserId = userEntity.FirebaseUserId;
			userEntity.Email = userEntity.Email;
			userEntity.FolderName = userEntity.FolderName;
			userEntity.ImageName = userEntity.ImageName;
			userEntity.FullName = user.FullName;
			userEntity.Country = user.Country;
			userEntity.City = user.City;
			userEntity.Age = user.Age;
			var saveChangesValue = await _userDataAccess.UpdateAsync(userEntity);
			if (saveChangesValue > 0)
			{
				return new Response(ResponseCode.Success, "User updated successfully");
			}
			return new Response(ResponseCode.BadRequest, "User not updated");
		}

		public async Task<Response> TUpdateUserImageAsync(UserImageUploadDto userImageUploadDto)
		{
			var firebaseUserId = _userUtility.GetFirebaseUserId();
			if (string.IsNullOrEmpty(firebaseUserId))
			{
				return new Response(ResponseCode.BadRequest, "Firebase user id cannot be empty");
			}
			var userEntity = await _userDataAccess.GetUserIdAsync(firebaseUserId);
			if (userEntity == null)
			{
				return new Response(ResponseCode.NotFound, "User not found");
			}
			if(userImageUploadDto.ImageBase64 != null)
			{
				var userImageName = Guid.NewGuid().ToString() + ".png";
				var userImageUrl = await _googleCloudStroge.UploadImageWithBase64String(userImageUploadDto.ImageBase64,userImageName,"UserImage");
				if (userImageUrl == null)
				{
					return new Response(ResponseCode.BadRequest, "User Image not uploaded");
				}
				userEntity.FirebaseUserId = userEntity.FirebaseUserId;
				userEntity.Email = userEntity.Email;
				userEntity.FullName = userEntity.FullName;
				userEntity.FolderName = userEntity.FolderName;
				userEntity.ImageName = userImageName;
				var saveChangesValue = await _userDataAccess.UpdateAsync(userEntity);
				if (saveChangesValue > 0)
				{
					return new Response(ResponseCode.Success, "User Image updated successfully");
				}
				return new Response(ResponseCode.BadRequest, "User Image not updated");
			}
			return new Response(ResponseCode.BadRequest, "User Image not found");

		}
	}
}
