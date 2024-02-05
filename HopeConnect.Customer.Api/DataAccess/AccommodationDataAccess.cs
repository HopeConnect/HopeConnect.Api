using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace HopeConnect.Customer.Api.DataAccess
{
    public interface IAccommodationDataAccess
    {
        Task<int> AddAsync(Accommodation accommodation);
        Task<int> DeleteAsync(Accommodation accommodation);
        Task<int> UpdateAsync(Accommodation accommodation);
        Task<IList<AccommodationListDto>> GetAllAccommodation();
    }
    public class AccommodationDataAccess : IAccommodationDataAccess
    {
        private readonly HopeConnectContext _context;

        public AccommodationDataAccess(HopeConnectContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Accommodation accommodation)
        {
            await _context.Accommodations.AddAsync(accommodation);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(Accommodation accommodation)
        {
            _context.Accommodations.Remove(accommodation);
            return await _context.SaveChangesAsync();
        }
        public async Task<IList<AccommodationListDto>> GetAllAccommodation()
        {
            return await _context.Accommodations.AsNoTracking().OrderByDescending(x => x.Id).Select(x => new AccommodationListDto
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                Name = x.Name,
                Location = x.Location,
                Description = x.Description,
            }).ToListAsync();
        }
        public async Task<int> UpdateAsync(Accommodation accommodation)
        {
            _context.Accommodations.Update(accommodation);
            return await _context.SaveChangesAsync();
        }
    }
}
