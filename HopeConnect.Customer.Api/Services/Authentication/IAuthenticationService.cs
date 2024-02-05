using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.Services.Authentication
{
	public interface IAuthenticationService
	{
		Task<Response> RegisterAsync(RegisterRequestDto registerRequestDto);
		Task<Response<string>> LoginAsync (LoginRequestDto loginRequestDto);
		Task<Response<string>> DeleteAsync();
	}
}
