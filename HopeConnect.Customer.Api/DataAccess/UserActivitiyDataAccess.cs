using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HopeConnect.Customer.Api.DataAccess
{
	public interface IUserActivitiyDataAccess
	{
		int Add(UserActivitiy userActivitiy);
		Task<IList<UserActivitiyListDto>> GetDonation(int userId);
	}
	public class UserActivitiyDataAccess : IUserActivitiyDataAccess
	{
		private readonly HopeConnectContext _hopeConnectContext;

		public UserActivitiyDataAccess(HopeConnectContext hopeConnectContext)
		{
			_hopeConnectContext = hopeConnectContext;
		}

		public int Add(UserActivitiy userActivitiy)
		{
			_hopeConnectContext.Add(userActivitiy);
			return _hopeConnectContext.SaveChanges();
		}

		public async Task<IList<UserActivitiyListDto>> GetDonation(int userId)
		{
			return await _hopeConnectContext.UserActivities.AsNoTracking().Where(x => x.UserId == userId)
				.Select(x=> 
				new UserActivitiyListDto
				{ 
					ActivityId = x.ActivityId,
					UserId = x.UserId,
					Name = x.Name,
					Surname = x.Surname,
					Message = x.Message,	
					DonationAmount = x.DonationAmount,
					City = x.City,
				}).ToListAsync();
		}
	}
}
