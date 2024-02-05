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
    public class EducationController : ControllerBase
    {
        private readonly IEducationBusinessUnit _educationBusinessUnit;
        public EducationController(IEducationBusinessUnit educationBusinessUnit)
        {
            _educationBusinessUnit = educationBusinessUnit;
        }
        [HttpGet]
        [Route("GetAllEducation")]
        public async Task<Response<IList<EducationListDto>>> GetAllEducationAsync()
        {
            return await _educationBusinessUnit.TGetAllEducationAsync();
        }
        [HttpPost]
        [Route("AddEducation")]
        public async Task<Response> AddEducationAsync([FromBody] Education education)
        {
            return await _educationBusinessUnit.TAddAsync(education);
        }
    }
}
