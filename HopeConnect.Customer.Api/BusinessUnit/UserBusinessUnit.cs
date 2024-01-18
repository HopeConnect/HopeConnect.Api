using HopeConnect.Customer.Api.DataAccess;
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
		Task<Response> TDeleteAsync(int userId);
		Task<Response<IList<UserListDto>>> TGetUserByUserFirebaseIdAsync();
		Task<Response<IList<User>>> TGetAllUserAsync();
	}
	public class UserBusinessUnit : IUserBusinessUnit
	{
		private readonly IUserDataAccess _userDataAccess;
		private readonly IUserUtility _userUtility;
		public UserBusinessUnit(IUserDataAccess userDataAccess, IUserUtility userUtility)
		{
			_userDataAccess = userDataAccess;
			_userUtility = userUtility;
		}
		public async Task<Response> TAddAsync(User user)
		{
			if (user == null)
			{
				return new Response(ResponseCode.BadRequest, "User cannot be null");
			}
			var saveChangesValue = await _userDataAccess.AddAsync(user);
			if (saveChangesValue > 0)
			{
				return new Response(ResponseCode.Success, "User added successfully");
			}
			return new Response(ResponseCode.BadRequest, "User cannot be added");

		}

		public Task<Response> TDeleteAsync(int userId)
		{
			throw new NotImplementedException();
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

		public async Task<Response<IList<UserListDto>>> TGetUserByUserFirebaseIdAsync()
		{
			var firebaseUserId = _userUtility.GetFirebaseUserId();
			if (string.IsNullOrEmpty(firebaseUserId))
			{
				return new Response<IList<UserListDto>>(ResponseCode.BadRequest, "Firebase user id cannot be empty");
			}
			var userEntity = await _userDataAccess.GetUserByUserFirebaseIdAsync(firebaseUserId);
			if (userEntity.Any())
			{
				return new Response<IList<UserListDto>>(ResponseCode.Success, userEntity);
			}
			return new Response<IList<UserListDto>>(ResponseCode.NotFound, "User not found");
		}

		public Task<Response> TUpdateAsync(User user)
		{
			throw new NotImplementedException();
		}
	}
}
