using HopeConnect.Customer.Api.BusinessUnit;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.Concrete;
using Microsoft.AspNetCore.Authorization;
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
		public async Task<Response<UserListDto>> GetUserByUserFirebaseIdAsync()
		{
			return await _userBusinessUnit.TGetUserByUserFirebaseIdAsync();
		}
		[HttpGet]
		[Route("GetAllUser")]
		public async Task<Response<IList<User>>> GetAllUserAsync()
		{
			return await _userBusinessUnit.TGetAllUserAsync();
		}
		[HttpPut]
		[Route("UpdateUserImage")]
		public async Task<Response> UpdateUserImageAsync(UserImageUploadDto userImageUploadDto)
		{
			return await _userBusinessUnit.TUpdateUserImageAsync(userImageUploadDto);
		}
		[HttpPut]
		[Route("UpdateProfile")]
		public async Task<Response> UpdateProfileAsync(User user)
		{
			return await _userBusinessUnit.TUpdateAsync(user);
		}
	}
}
