﻿using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
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
			return await _context.SaveChangesAsync();
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
				Name = x.Name,
				Location = x.Location,
				Title = x.Title,
				Description = x.Description,
				FolderName = x.FolderName,
				ImageName = x.ImageName,
				RecipientType = x.RecipientType	
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
				RecipientType = x.RecipientType
			}).ToListAsync();
		}
		public async Task<int> UpdateAsync(Recipient recipient)
		{
			_context.Recipients.Update(recipient);
			return await _context.SaveChangesAsync();
		}
	}	
}