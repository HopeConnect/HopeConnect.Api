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
	public class RecipientController : ControllerBase
	{
		private readonly IRecipientBusinessUnit _recipientBusinessUnit;

		public RecipientController(IRecipientBusinessUnit recipientBusinessUnit)
		{
			_recipientBusinessUnit = recipientBusinessUnit;
		}

		[HttpGet]
		[Route("GetAllRecipient")]
		public async Task<Response<IList<RecipientListDto>>> GetAllRecipientAsync()
		{
			return await _recipientBusinessUnit.TGetAllRecipientAsync();
		}
		[HttpGet]
		[Route("GetRecipientByRecipientType")]
		public async Task<Response<IList<RecipientListDto>>> GetRecipientByRecipientType(int recipientType)
		{
			return await _recipientBusinessUnit.GetRecipientByRecipientType(recipientType);
		}
	}
}
