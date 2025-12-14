using FirebaseAdmin.Auth;
using HopeConnect.Customer.Api.BusinessUnit;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Infrastructure.Utility;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.Services.Authentication
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient _httpClient;
		private readonly IUserBusinessUnit _userBusinessUnit;
		private readonly IUserUtility _userUtility;
		public AuthenticationService(HttpClient httpClient, IUserBusinessUnit userBusinessUnit, IUserUtility userUtility)
		{
			_httpClient = httpClient;
			_userBusinessUnit = userBusinessUnit;
			_userUtility = userUtility;
		}

		public async Task<Response<string>> LoginAsync(LoginRequestDto loginRequestDto)
		{
			var credentials = new 
			{
				loginRequestDto.Email,
				loginRequestDto.Password,
				returnSecureToken = true
			};
			var response = await _httpClient.PostAsJsonAsync("", credentials);
			if (!response.IsSuccessStatusCode)
			{
				return new Response<string>(ResponseCode.InternalServerError, "User login failed");
			}
			var authFirebaseObject = await response.Content.ReadFromJsonAsync<AuthFirebase>();
			if (authFirebaseObject == null)
			{
				return new Response<string>(ResponseCode.InternalServerError, "User login failed");
			}
			return new Response<string>(ResponseCode.Success,"User Login Succeed",authFirebaseObject!.IdToken!);
		}

		public async Task<Response> RegisterAsync(RegisterRequestDto registerRequestDto)
		{
			var userArgs = new UserRecordArgs
			{
				Email = registerRequestDto.Email, 
				Password = registerRequestDto.Password 
			};
			var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);
			var user = await _userBusinessUnit.TAddAsync(new User
			{
				Email = registerRequestDto.Email,
				FullName = registerRequestDto.FullName,
				FirebaseUserId = userRecord.Uid
			});
			if(user.ResponseCode == ResponseCode.Success)
			{
				return new Response(ResponseCode.Success, "User created successfully");
			}
			return new Response(ResponseCode.BadRequest, "User not added");
		}
		public async Task<Response<string>> DeleteAsync()
		{
			var firebaseUserId = _userUtility.GetFirebaseUserId();
			if (string.IsNullOrEmpty(firebaseUserId))
			{
				return new Response<string>(ResponseCode.BadRequest, "Firebase user id is empty");
			}
			var authFirebase = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseUserId);
			if (authFirebase == null)
			{
				return new Response<string>(ResponseCode.BadRequest, "Firebase user not found");
			}
			var tokenCancellation = FirebaseAuth.DefaultInstance.RevokeRefreshTokensAsync(authFirebase.Uid);
			if (tokenCancellation == null)
			{
				return new Response<string>(ResponseCode.BadRequest, "User token not cancelled!");
			}
			var response = FirebaseAuth.DefaultInstance.DeleteUserAsync(authFirebase.Uid);
			if (response == null)
			{
				return new Response<string>(ResponseCode.BadRequest, "User not deleted");
			}
			var user = await _userBusinessUnit.TDeleteByFirebaseUserIdAsync(authFirebase.Uid);
			if (user.ResponseCode == ResponseCode.Success)
			{
				return new Response<string>(ResponseCode.Success, "User deleted successfully");
			}
			return new Response<string>(ResponseCode.BadRequest, "User not deleted");
		}
	}
}
