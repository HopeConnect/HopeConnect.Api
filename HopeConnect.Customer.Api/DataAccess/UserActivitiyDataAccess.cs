using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Enum;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace HopeConnect.Customer.Api.DataAccess
{
	public interface IUserActivitiyDataAccess
	{
		int Add(UserActivitiy userActivitiy);
		Task<IList<UserActivitiyListDto>> GetDonation(int userId);
		Task<IList<UserActivitiy>> GetAllUserActivityByUserId(int userId);
		Task<IList<UserDonationArchiveListDto>> GetUserDonationArchiveFoodList(int recipientId);
		Task<IList<UserDonationArchiveListDto>> GetUserDonationArchiveAccommodationList(int recipientId);
		Task<IList<UserDonationArchiveListDto>> GetUserDonationArchiveClotheList(int recipientId);
		Task<IList<UserDonationArchiveListDto>> GetUserDonationArchiveEducationList(int recipientId);
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

		public async Task<IList<UserActivitiy>> GetAllUserActivityByUserId(int userId)
		{
			var userDonation = await _hopeConnectContext.UserActivities.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
			return userDonation;
		}

		public async Task<IList<UserActivitiyListDto>> GetDonation(int userId)
		{
			return await _hopeConnectContext.UserActivities.AsNoTracking().Where(x => x.UserId == userId)
				.Select(x =>
				new UserActivitiyListDto
				{
					UserId = x.UserId,
					Name = x.Name,
					Surname = x.Surname,
					Message = x.Message,
					DonationAmount = x.DonationAmount,
					City = x.City,
				}).ToListAsync();
		}

		public async Task<IList<UserDonationArchiveListDto>> GetUserDonationArchiveFoodList(int recipientId)
		{
			var userDonationArchiveList = await _hopeConnectContext.Recipients.AsNoTracking().Where(x => x.Id == recipientId &&  x.RecipientType == (int)RecipientType.Food).
				Select(x => new UserDonationArchiveListDto
			{
				RecipientId = x.Id,
				RecipientType = RecipientType.Food.ToString(),
				Title = x.Title,
				Name = x.Name,
				Location = x.Location,
				Description = x.Description,
			}).ToListAsync();
			return userDonationArchiveList;
		}
		public async Task<IList<UserDonationArchiveListDto>> GetUserDonationArchiveAccommodationList(int recipientId)
		{
			var userDonationArchiveList = await _hopeConnectContext.Recipients.AsNoTracking().Where(x => x.Id == recipientId &&  x.RecipientType == (int)RecipientType.Accommodation).Select(x => new UserDonationArchiveListDto
			{
				RecipientId = x.Id,
				ImageUrl = x.ImageName,
				RecipientType = RecipientType.Accommodation.ToString(),
				Title = x.Title,
				Name = x.Name,
				Location = x.Location,
				Description = x.Description
			}).ToListAsync();
			return userDonationArchiveList;
		}
		public async Task<IList<UserDonationArchiveListDto>> GetUserDonationArchiveClotheList(int recipientId)
		{
			var userDonationArchiveList = await _hopeConnectContext.Recipients.AsNoTracking().Where(x => x.Id == recipientId && x.RecipientType == (int)RecipientType.Clothes).Select(x => new UserDonationArchiveListDto
			{
				RecipientId = x.Id,
				RecipientType = RecipientType.Clothes.ToString(),
				Title = x.Title,
				Name = x.Name,
				Location = x.Location,
				Description = x.Description,
			}).ToListAsync();
			return userDonationArchiveList;
		}
		public async Task<IList<UserDonationArchiveListDto>> GetUserDonationArchiveEducationList(int recipientId)
		{
			var userDonationArchiveList = await _hopeConnectContext.Recipients.AsNoTracking().
				Where(x => x.Id == recipientId && x.RecipientType == (int)RecipientType.Education).Select(x => new UserDonationArchiveListDto
			{
				RecipientId = x.Id,
				RecipientType= RecipientType.Education.ToString(),	
				Title = x.Title,
				Name = x.Name,
				Location = x.Location,
				Description = x.Description
			}).ToListAsync();
			return userDonationArchiveList;
		}
	}
}
