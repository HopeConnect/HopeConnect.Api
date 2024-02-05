using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Cloud;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
    public interface IAcommodationBusinessUnit
    {
        Task<Response> TAddAsync(Accommodation accommodation);
        Task<Response> TUpdateAsync(Accommodation accommodation);
        Task<Response> TDeleteAsync(int accommodationId);
        Task<Response<IList<AccommodationListDto>>> TGetAllAccommodationAsync();
    }
    public class AccommodationBusinessUnit: IAcommodationBusinessUnit
    {
        private readonly IAccommodationDataAccess _accommodationDataAccess;
        private readonly IGoogleCloudStroge _googleCloudStroge;

		public AccommodationBusinessUnit(IAccommodationDataAccess accommodationDataAccess, IGoogleCloudStroge googleCloudStroge)
		{
			_accommodationDataAccess = accommodationDataAccess;
			_googleCloudStroge = googleCloudStroge;
		}

		public async Task<Response> TAddAsync(Accommodation accommodation)
        {
            if (accommodation == null)
            {
                return new Response(ResponseCode.BadRequest, "Accommodation cannot be null");
            }
            var saveChangesValue = await _accommodationDataAccess.AddAsync(accommodation);
            if (saveChangesValue > 0)
            {
                return new Response(ResponseCode.Success, "Accommodation added successfully");
            }
            return new Response(ResponseCode.BadRequest, "Accommodation cannot be added");

        }

        public Task<Response> TDeleteAsync(int accommodationId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IList<AccommodationListDto>>> TGetAllAccommodationAsync()
        {
            var accommodationEntities = await _accommodationDataAccess.GetAllAccommodation();
            if (accommodationEntities.Any())
            {
               var accommodationList = accommodationEntities.ToList();
                foreach (var accommodationEntity in accommodationList)
                {
                    var userImageUrl = _googleCloudStroge.GenerateDownloadUrl("Accommodation", accommodationEntity.ImageName);
                    if (userImageUrl == null)
                    {
                        return new Response<IList<AccommodationListDto>>(ResponseCode.NotFound, $"User Image Not Found for {accommodationEntity.ImageName}!");
                    }
                    accommodationEntity.ImageUrl = userImageUrl;
                }
				return new Response<IList<AccommodationListDto>>(ResponseCode.Success, "Accommodation list Success.", accommodationList);
			}
            return new Response<IList<AccommodationListDto>>(ResponseCode.NotFound, "Accommodation not found");
        }

        public Task<Response> TUpdateAsync(Accommodation accommodation)
        {
            throw new NotImplementedException();
        }
    }   
}
