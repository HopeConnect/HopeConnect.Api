using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Services.Authentication;
using HopeConnect.Customer.Api.Shared.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HopeConnect.Customer.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthenticationService _authenticationService;

		public AuthController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}
		[HttpPost]
		[Route("register")]
		public async Task<Response> Register([FromBody] RegisterRequestDto registerRequestDto)
		{
			var response = await _authenticationService.RegisterAsync(registerRequestDto);
			return response;
		}
		[HttpPost]
		[Route("login")]
		public async Task<Response<string>> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var response = await _authenticationService.LoginAsync(loginRequestDto);
			return response;
		}
		[HttpDelete]
		[Authorize]
		[Route("delete")]
		public async Task<Response<string>> Delete()
		{
			return await _authenticationService.DeleteAsync();
		}
	}
}
