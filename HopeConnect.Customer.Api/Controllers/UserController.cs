using HopeConnect.Customer.Api.BusinessUnit;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Shared.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HopeConnect.Customer.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserBusinessUnit _userBusinessUnit;
		public UserController(IUserBusinessUnit userBusinessUnit)
		{
			_userBusinessUnit = userBusinessUnit;
		}
		[HttpGet]
		[Route("GetUserByUserFirebaseId")]
		public async Task<Response<IList<UserListDto>>> GetUserByUserFirebaseIdAsync()
		{
			return await _userBusinessUnit.TGetUserByUserFirebaseIdAsync();
		}
	}
}
