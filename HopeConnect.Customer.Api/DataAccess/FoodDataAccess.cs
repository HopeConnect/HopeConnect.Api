using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace HopeConnect.Customer.Api.DataAccess
{
    public interface IFoodDataAccess
    {
        Task<int> AddAsync(Food food);
        Task<int> DeleteAsync(Food food);
        Task<int> UpdateAsync(Food food);
        Task<IList<FoodListDto>> GetFoodByFirebaseIdAsync(string foodFirebaseId);
        Task<IList<FoodListDto>> GetAllFood();
    }
    public class FoodDataAccess : IFoodDataAccess
    {
        private readonly HopeConnectContext _context;

        public FoodDataAccess(HopeConnectContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Food food)
        {
            await _context.Foods.AddAsync(food);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Food food)
        {
            _context.Foods.Remove(food);
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<FoodListDto>> GetAllFood()
        {
            return await _context.Foods.AsNoTracking().OrderByDescending(x => x.Id).Select(x => new FoodListDto
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
            }).ToListAsync();

        }

        public async Task<IList<FoodListDto>> GetFoodByFirebaseIdAsync(string foodFirebaseId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(Food food)
        {
            _context.Foods.Update(food);
            return await _context.SaveChangesAsync();
        }
    }
}
