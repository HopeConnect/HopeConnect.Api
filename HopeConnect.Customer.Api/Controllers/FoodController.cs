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
    public class FoodController : ControllerBase
    {
        private readonly IFoodBusinessUnit _foodBusinessUnit;
        public FoodController(IFoodBusinessUnit foodBusinessUnit)
        {
            _foodBusinessUnit = foodBusinessUnit;
        }
        [HttpGet]
        [Route("GetAllFood")]
        public async Task<Response<IList<FoodListDto>>> GetAllFoodAsync()
        {
            return await _foodBusinessUnit.TGetAllFoodAsync();
        }
        [HttpPost]
        [Route("AddFood")]
        public async Task<Response> AddFoodAsync([FromBody] Food food)
        {
            return await _foodBusinessUnit.TAddAsync(food);
        }
    }
}
