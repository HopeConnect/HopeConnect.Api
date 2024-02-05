using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Infrastructure.Utility;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
	public interface IUserActivitiyBusinessUnit
	{
		Task<Response<UserActivitiy>> TAddAsync(UserActivitiy userActivitiy);
		Task<Response<int>> TGetDonationCountAsync();
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
			userActivitiy.UserId = userId.Data;
			userActivitiy.CreateAt = DateTime.UtcNow;
			var saveChangesValue = _userActivitiyDataAccess.Add(userActivitiy);
			if (saveChangesValue > 0)
			{
				return new Response<UserActivitiy>(ResponseCode.Success, "User Donation Added Successfully!");
			}
			return new Response<UserActivitiy>(ResponseCode.BadRequest, "User Donation Failed to load successfully!");
		}

		public async Task<Response<int>> TGetDonationCountAsync()
		{
			var userFirebaseId = _userUtility.GetFirebaseUserId();
			if(userFirebaseId == null)
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
	}
}

