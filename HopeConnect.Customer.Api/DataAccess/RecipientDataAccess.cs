using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Enum;
using HopeConnect.Customer.Api.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace HopeConnect.Customer.Api.DataAccess
{
    public interface IRecipientDataAccess
	{
		Task<int> AddAsync(Recipient recipient);
		Task<int> DeleteAsync(Recipient recipient);
		Task<int> UpdateAsync(Recipient recipient);
		Task<IList<RecipientListDto>> GetAllRecipient();
		Task<IList<RecipientListDto>> GetRecipientByRecipientType(int recipientType);
		Task<IList<RecipientCoordinationDto>> GetRecipientLatitudeAndLongitude();
	}
	public class RecipientDataAccess : IRecipientDataAccess
	{
		private readonly HopeConnectContext _context;
		public RecipientDataAccess(HopeConnectContext context)
		{
			_context = context;
		}
		public async Task<int> AddAsync(Recipient recipient)
		{
			await _context.Recipients.AddAsync(recipient);
			await _context.SaveChangesAsync();
			return recipient.Id;
		}
		public async Task<int> DeleteAsync(Recipient recipient)
		{
			_context.Recipients.Remove(recipient);
			return await _context.SaveChangesAsync();
		}
		public async Task<IList<RecipientListDto>> GetAllRecipient()
		{
			return await _context.Recipients.AsNoTracking().OrderByDescending(x => x.Id).Select(x => new RecipientListDto
			{
				Id = x.Id,
				RecipientId = x.Id,
				Name = x.Name,
				Location = x.Location,
				Title = x.Title,
				Description = x.Description,
				FolderName = x.FolderName,
				ImageName = x.ImageName,
				Latitude = x.Latitude,
				Longitude = x.Longitude
			}).ToListAsync();
		}

		public async Task<IList<RecipientListDto>> GetRecipientByRecipientType(int recipientType)
		{
			return await _context.Recipients.AsNoTracking().Where(x => x.RecipientType == recipientType).OrderByDescending(x => x.Id).Select(x => new RecipientListDto
			{
				Id = x.Id,
				Name = x.Name,
				Location = x.Location,
				Title = x.Title,
				Description = x.Description,
				FolderName = x.FolderName,
				ImageName = x.ImageName,
			}).ToListAsync();
		}

		public async Task<IList<RecipientCoordinationDto>> GetRecipientLatitudeAndLongitude()
		{
			return await _context.Recipients.AsNoTracking().Select(x => new RecipientCoordinationDto
			{
				Title = x.Title,
				Description = x.Description,
				Latitude = x.Latitude,
				Longitude = x.Longitude
			}).ToListAsync();
		}

		public async Task<int> UpdateAsync(Recipient recipient)
		{
			_context.Recipients.Update(recipient);
			return await _context.SaveChangesAsync();
		}
	}	
}
