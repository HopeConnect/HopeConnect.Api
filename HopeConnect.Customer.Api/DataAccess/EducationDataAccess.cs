using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace HopeConnect.Customer.Api.DataAccess
{
    public interface IEducationDataAccess
    {
        Task<int> AddAsync(Education education);
        Task<int> DeleteAsync(Education education);
        Task<int> UpdateAsync(Education education);
        Task<IList<EducationListDto>> GetAllEducation();
    }
    public class EducationDataAccess : IEducationDataAccess
    {
        private readonly HopeConnectContext _context;
        public EducationDataAccess(HopeConnectContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Education education)
        {
            await _context.Educations.AddAsync(education);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Education education)
        {
            _context.Remove(education);
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<EducationListDto>> GetAllEducation()
        {
            return await _context.Educations.AsNoTracking().OrderByDescending(x => x.Id).Select(x => new EducationListDto
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                Name = x.Name,
                Location = x.Location,
                Description = x.Description,
            }).ToListAsync();
        }

        public async Task<int> UpdateAsync(Education education)
        {
            _context.Update(education);
            return await _context.SaveChangesAsync();
        }
    }
}
