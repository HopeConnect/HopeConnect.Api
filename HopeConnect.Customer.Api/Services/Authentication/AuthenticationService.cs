using FirebaseAdmin.Auth;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.Services.Authentication
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient _httpClient;
		public AuthenticationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
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
			if (userRecord == null)
			{
				return new Response(ResponseCode.InternalServerError, "User creation failed");
			}
			return new Response(ResponseCode.Success, "User created successfully");
		}
	}
}
