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
    public class ClotheController : ControllerBase
    {
        private readonly IClotheBusinessUnit _clotheBusinessUnit;
        public ClotheController(IClotheBusinessUnit clotheBusinessUnit)
        {
            _clotheBusinessUnit = clotheBusinessUnit;
        }
        [HttpGet]
        [Route("GetAllClothe")]
        public async Task<Response<IList<ClotheListDto>>> GetAllClotheAsync()
        {
            return await _clotheBusinessUnit.TGetAllClotheAsync();
        }
        [HttpPost]
        [Route("AddClothe")]
        public async Task<Response> AddClotheAsync([FromBody] Clothes clothe)
        {
            return await _clotheBusinessUnit.TAddAsync(clothe);
        }
    }
}
