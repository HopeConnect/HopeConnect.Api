using HopeConnect.Customer.Api.BusinessUnit;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace HopeConnect.Customer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly IAcommodationBusinessUnit _acommodationBusinessUnit;
        public AccommodationController(IAcommodationBusinessUnit acommodationBusinessUnit)
        {
            _acommodationBusinessUnit = acommodationBusinessUnit;
        }
        [HttpGet]
        [Route("GetAllAccommodation")]
        public Task<Response<IList<AccommodationListDto>>> GetAllAccommodationAsync()
        {
            return _acommodationBusinessUnit.TGetAllAccommodationAsync();
        }
        [HttpPost]
        [Route("AddAccommodation")]
        public Task<Response> AddAccommodationAsync([FromBody] Accommodation accommodation)
        {
            return _acommodationBusinessUnit.TAddAsync(accommodation);
        }
    }
}
