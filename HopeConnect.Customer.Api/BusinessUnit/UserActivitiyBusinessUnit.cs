using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Infrastructure.Utility;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
	public interface IUserActivitiyBusinessUnit
	{
		Task<Response<UserActivitiy>> TAddAsync(UserActivitiy userActivitiy);
		Task<Response<UserActivitiy>> TAddUserRecipientNotificationActivity(UserActivitiy userActivitiy);
		Task<Response<int>> TGetDonationCountAsync();
		Task<Response<int>> TGetUserHelpNotificationCount();

		Task<Response<IList<UserActivitiy>>> TGetAllUserActivityByUserFirabaseId();
		Task<Response<IList<UserDonationArchiveListDto>>> TGetUserDonationArchiveList();

	}
	public class UserActivitiyBusinessUnit : IUserActivitiyBusinessUnit
	{
		private readonly IUserUtility _userUtility;
		private readonly IUserActivitiyDataAccess _userActivitiyDataAccess;
		private readonly IUserBusinessUnit _userBusinessUnit;
		public UserActivitiyBusinessUnit(IUserUtility userUtility, IUserActivitiyDataAccess userActivitiyDataAccess, IUserBusinessUnit userBusinessUnit)
		{
			_userUtility = userUtility;
			_userActivitiyDataAccess = userActivitiyDataAccess;
			_userBusinessUnit = userBusinessUnit;
		}
		public async Task<Response<UserActivitiy>> TAddAsync(UserActivitiy userActivitiy)
		{
			if (userActivitiy == null)
			{
				return new Response<UserActivitiy>(ResponseCode.BadRequest, "Error! Please User Activity Information Added!");
			}
			var userFirebaseID = _userUtility.GetFirebaseUserId();
			if (userFirebaseID == null)
			{
				return new Response<UserActivitiy>(ResponseCode.BadRequest, "User Id Not Found!");
			}
			var userId = await _userBusinessUnit.TGetUserIdByUserFirebaseIdAsync();
			userActivitiy.HelpType = 1;
			userActivitiy.UserId = userId.Data;
			userActivitiy.CreateAt = DateTime.UtcNow;
			var saveChangesValue = _userActivitiyDataAccess.Add(userActivitiy);
			if (saveChangesValue > 0)
			{
				return new Response<UserActivitiy>(ResponseCode.Success, "User Donation Added Successfully!");
			}
			return new Response<UserActivitiy>(ResponseCode.BadRequest, "User Donation Failed to load successfully!");
		}

		public async Task<Response<UserActivitiy>> TAddUserRecipientNotificationActivity(UserActivitiy userActivitiy)
		{
			var userFirebaseId = _userUtility.GetFirebaseUserId();
			if (userFirebaseId == null)
			{
				return new Response<UserActivitiy>(ResponseCode.BadRequest, "User Firebase Id Not Found!");
			}
			var userId = await _userBusinessUnit.TGetUserIdByUserFirebaseIdAsync();
			if (userFirebaseId == null)
			{
				return new Response<UserActivitiy>(ResponseCode.BadRequest, "User Id Not Found!");
			}
			if(userActivitiy == null)
			{
				return new Response<UserActivitiy>(ResponseCode.BadRequest, "User Activity Not Found!");
			}
			userActivitiy.UserId = userId.Data;
			userActivitiy.CreateAt = DateTime.UtcNow;
			var saveChangesValue = _userActivitiyDataAccess.Add(userActivitiy);
			if (saveChangesValue > 0)
			{
				return new Response<UserActivitiy>(ResponseCode.Success, "User Donation Added Successfully!");
			}
			return new Response<UserActivitiy>(ResponseCode.BadRequest, "User Donation Failed to load successfully!");

		}

		public async Task<Response<IList<UserActivitiy>>> TGetAllUserActivityByUserFirabaseId()
		{
			var userFirebaseId = _userUtility.GetFirebaseUserId();
			if (userFirebaseId == null)
			{
				return new Response<IList<UserActivitiy>>(ResponseCode.BadRequest, "User Firebase Id Not Found!");
			}
			var userId = await _userBusinessUnit.TGetUserIdByUserFirebaseIdAsync();
			var userDonation = await _userActivitiyDataAccess.GetAllUserActivityByUserId(userId.Data);
			if (userDonation == null)
			{
				return new Response<IList<UserActivitiy>>(ResponseCode.NotFound, "User Donation Not Found!");
			}
			if (userDonation.Count() >= 0)
			{
				return new Response<IList<UserActivitiy>>(ResponseCode.Success, "Successful in bringing the number of donations!", userDonation);
			}
			return new Response<IList<UserActivitiy>>(ResponseCode.Success, "Failed to fetch donation count!");
		}
		public async Task<Response<int>> TGetDonationCountAsync()
		{
			var userFirebaseId = _userUtility.GetFirebaseUserId();
			if (userFirebaseId == null)
			{
				return new Response<int>(ResponseCode.BadRequest, "User Firebase Id Not Found!");
			}
			var userId = await _userBusinessUnit.TGetUserIdByUserFirebaseIdAsync(); ;
			var userDonation = await _userActivitiyDataAccess.GetDonation(userId.Data);
			if (userDonation == null)
			{
				return new Response<int>(ResponseCode.NotFound, "User Donation Not Found!");
			}
			var userDonationCount = userDonation.Count();
			if (userDonationCount >= 0)
			{
				return new Response<int>(ResponseCode.Success, "Successful in bringing the number of donations!", userDonationCount);
			}
			return new Response<int>(ResponseCode.Success, "Failed to fetch donation count!");
		}
		public async Task<Response<IList<UserDonationArchiveListDto>>> TGetUserDonationArchiveList()
		{
			var userFirebaseId = _userUtility.GetFirebaseUserId();
			if (userFirebaseId == null)
			{
				return new Response<IList<UserDonationArchiveListDto>>(ResponseCode.BadRequest, "User Firebase Id Not Found!");
			}
			var userId = await _userBusinessUnit.TGetUserIdByUserFirebaseIdAsync();
			if(userId == null)
			{
				return new Response<IList<UserDonationArchiveListDto>>(ResponseCode.BadRequest, "User Id Not Found!");
			}
			var userDonationArchive = await _userActivitiyDataAccess.GetAllUserActivityByUserId(userId.Data);
			if (userDonationArchive == null)
			{
				return new Response<IList<UserDonationArchiveListDto>>(ResponseCode.NotFound, "User Donation Archive Not Found!");
			}
			var userDonationArchiveList = new List<UserDonationArchiveListDto>();
			foreach (var item in userDonationArchive)
			{
				var userDonationArchiveFoodList = await _userActivitiyDataAccess.GetUserDonationArchiveFoodList(item.RecipientId.GetValueOrDefault(), item.CreateAt);
				var userDonationArchiveAccommodationList = await _userActivitiyDataAccess.GetUserDonationArchiveAccommodationList(item.RecipientId.GetValueOrDefault(), item.CreateAt);
				var userDonationArchiveClotheList = await _userActivitiyDataAccess.GetUserDonationArchiveClotheList(item.RecipientId.GetValueOrDefault(), item.CreateAt);
				var userDonationArchiveEducationList = await _userActivitiyDataAccess.GetUserDonationArchiveEducationList(item.RecipientId.GetValueOrDefault(), item.CreateAt);
				userDonationArchiveList.AddRange(userDonationArchiveFoodList);
				userDonationArchiveList.AddRange(userDonationArchiveAccommodationList);
				userDonationArchiveList.AddRange(userDonationArchiveClotheList);
				userDonationArchiveList.AddRange(userDonationArchiveEducationList);
			}
			return new Response<IList<UserDonationArchiveListDto>>(ResponseCode.Success, userDonationArchiveList);
		}

		public async Task<Response<int>> TGetUserHelpNotificationCount()
		{
			var userFirebaseId = _userUtility.GetFirebaseUserId();
			if (userFirebaseId == null)
			{
				return new Response<int>(ResponseCode.BadRequest, "User Firebase Id Not Found!");
			}
			var userId = await _userBusinessUnit.TGetUserIdByUserFirebaseIdAsync(); ;
			var userHelpNotification = await _userActivitiyDataAccess.GetHelpNotification(userId.Data);
			if (userHelpNotification == null)
			{
				return new Response<int>(ResponseCode.NotFound, "User Donation Not Found!");
			}
			var userHelpNotificationCount = userHelpNotification.Count();
			if (userHelpNotificationCount >= 0)
			{
				return new Response<int>(ResponseCode.Success, "Successful in bringing the number of donations!", userHelpNotificationCount);
			}
			return new Response<int>(ResponseCode.Success, "Failed to fetch donation count!");
		}
	}
}