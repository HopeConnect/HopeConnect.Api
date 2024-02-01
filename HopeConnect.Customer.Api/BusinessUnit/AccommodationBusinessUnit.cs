using HopeConnect.Customer.Api.DataAccess;
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

        public AccommodationBusinessUnit(IAccommodationDataAccess accommodationDataAccess)
        {
            _accommodationDataAccess = accommodationDataAccess;
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
            var accommodationEntity = await _accommodationDataAccess.GetAllAccommodation();
            if (accommodationEntity.Any())
            {
                return new Response<IList<AccommodationListDto>>(ResponseCode.Success, "Accommodation list Success.", accommodationEntity);
            }
            return new Response<IList<AccommodationListDto>>(ResponseCode.NotFound, "Accommodation not found");
        }

        public Task<Response> TUpdateAsync(Accommodation accommodation)
        {
            throw new NotImplementedException();
        }
    }   
}
