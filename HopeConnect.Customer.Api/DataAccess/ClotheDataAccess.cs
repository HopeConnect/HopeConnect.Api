using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace HopeConnect.Customer.Api.DataAccess
{
    public interface IClotheDataAccess
    {
        Task<int> AddAsync(Clothes clothe);
        Task<int> DeleteAsync(Clothes clothe);
        Task<int> UpdateAsync(Clothes clothe);
        Task<IList<ClotheListDto>> GetAllClothe();
    }
    public class ClotheDataAccess : IClotheDataAccess
    {
        private readonly HopeConnectContext _context;

        public ClotheDataAccess(HopeConnectContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Clothes clothe)
        {
            await _context.Clothes.AddAsync(clothe);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Clothes clothe)
        {
            _context.Clothes.Remove(clothe);
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<ClotheListDto>> GetAllClothe()
        {
            return await _context.Clothes.AsNoTracking().OrderByDescending(x=> x.Id).Select(x=> new ClotheListDto
            {
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                Name = x.Name,
                Location = x.Location,
                Description = x.Description,
            }).ToListAsync();
        }

        public async Task<int> UpdateAsync(Clothes clothe)
        {
            _context.Clothes.Update(clothe);
            return await _context.SaveChangesAsync();
        }
    }
}
