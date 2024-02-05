using HopeConnect.Customer.Api.BusinessUnit;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HopeConnect.Customer.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class UserActivitiyController : ControllerBase
	{
		private readonly IUserActivitiyBusinessUnit _userActivitiy;
		public UserActivitiyController(IUserActivitiyBusinessUnit userActivitiy)
		{
			_userActivitiy = userActivitiy;
		}
		[HttpPost]
		[Route("UserActivityAdd")]
		public async Task<Response<UserActivitiy>> AddAsync(UserActivitiy userActivitiy)
		{
			return await _userActivitiy.TAddAsync(userActivitiy);
		}
		[HttpGet]
		[Route("GetDonationCount")]
		public async Task<Response<int>> TGetDonationCount()
		{
			return await _userActivitiy.TGetDonationCountAsync();
		}
	}
}
