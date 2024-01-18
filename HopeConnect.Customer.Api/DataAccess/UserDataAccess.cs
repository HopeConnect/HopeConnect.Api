using HopeConnect.Customer.Api.Infrastructure;
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
		Task<IList<UserListDto>> GetUserByUserFirebaseIdAsync(string userFirebaseId);
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

		public async Task<IList<UserListDto>> GetUserByUserFirebaseIdAsync(string userFirebaseId)
		{
			return await _context.Users.AsNoTracking().Where(x => x.FirebaseUserId == userFirebaseId).Select(x => new UserListDto
			{
				Email = x.Email,
				FullName = x.FullName,
				
			}).ToListAsync();
		}
		public async Task<int> UpdateAsync(User user)
		{
			_context.Users.Update(user);
			return await _context.SaveChangesAsync();
		}
	}
}
