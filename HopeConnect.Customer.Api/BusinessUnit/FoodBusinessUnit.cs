using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
    public interface IFoodBusinessUnit
    {
        Task<Response> TAddAsync(Food food);
        Task<Response> TUpdateAsync(Food food);
        Task<Response> TDeleteAsync(int foodId);
        Task<Response<IList<FoodListDto>>> TGetAllFoodAsync();
    }
    public class FoodBusinessUnit: IFoodBusinessUnit
    {
        private readonly IFoodDataAccess _foodDataAccess;
        public FoodBusinessUnit(IFoodDataAccess foodDataAccess)
        {
            _foodDataAccess = foodDataAccess;
        }
        public async Task<Response> TAddAsync(Food food)
        {
            if (food == null)
            {
                return new Response(ResponseCode.BadRequest, "Food cannot be null");
            }
            var saveChangesValue = await _foodDataAccess.AddAsync(food);
            if (saveChangesValue > 0)
            {
                return new Response(ResponseCode.Success, "Food added successfully");
            }
            return new Response(ResponseCode.BadRequest, "Food cannot be added");

        }

        public Task<Response> TDeleteAsync(int foodId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IList<FoodListDto>>> TGetAllFoodAsync()
        {
            var foodEntity = await _foodDataAccess.GetAllFood();
            if (foodEntity.Any())
            {
                return new Response<IList<FoodListDto>>(ResponseCode.Success, "Food list Success.", foodEntity);
            }
            return new Response<IList<FoodListDto>>(ResponseCode.NotFound, "Food not found");
        }

        public Task<Response> TUpdateAsync(Food food)
        {
            throw new NotImplementedException();
        }
    }
}
