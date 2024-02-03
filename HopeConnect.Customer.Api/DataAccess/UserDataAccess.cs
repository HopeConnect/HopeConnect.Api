﻿using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace HopeConnect.Customer.Api.DataAccess
{
	public interface IUserDataAccess
	{
		Task<int> AddAsync(User user);
		Task<int> DeleteAsync(User user);
		Task<int> UpdateAsync(User user);
		Task<UserListDto> GetUserByUserFirebaseIdAsync(string userFirebaseId);
		Task<IList<User>> GetAllUser();
	}
	public class UserDataAccess : IUserDataAccess
	{
		private readonly HopeConnectContext _context;

		public UserDataAccess(HopeConnectContext context)
		{
			_context = context;
		}

		public async Task<int> AddAsync(User user)
		{
			await _context.Users.AddAsync(user);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> DeleteAsync(User user)
		{
			_context.Users.Remove(user);
			return await _context.SaveChangesAsync();
		}

		public async Task<IList<User>> GetAllUser()
		{
			return await _context.Users.AsNoTracking().OrderByDescending(x=> x.Id).ToListAsync();
		}

		public async Task<UserListDto> GetUserByUserFirebaseIdAsync(string userFirebaseId)
		{
			return await _context.Users.AsNoTracking().OrderByDescending(x=>x.Id).Take(1).Select(x => new UserListDto
			{
				FirebaseId = x.FirebaseUserId,
				Email = x.Email,
				FullName = x.FullName,
				UserImageName = x.ImageName
			}).FirstOrDefaultAsync(x=> x.FirebaseId == userFirebaseId);
		}
		public async Task<int> UpdateAsync(User user)
		{
			_context.Users.Update(user);
			return await _context.SaveChangesAsync();
		}
	}
}
