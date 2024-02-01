using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
    public interface IClotheBusinessUnit
    {
        Task<Response> TAddAsync(Clothes clothe);
        Task<Response> TUpdateAsync(Clothes clothe);
        Task<Response> TDeleteAsync(int clotheId);
        Task<Response<IList<ClotheListDto>>> TGetAllClotheAsync();
    }
    public class ClotheBusinessUnit: IClotheBusinessUnit
    {
        private readonly IClotheDataAccess _clotheDataAccess;
        public ClotheBusinessUnit(IClotheDataAccess clotheDataAccess)
        {
            _clotheDataAccess = clotheDataAccess;
        }
        public async Task<Response> TAddAsync(Clothes clothe)
        {
            if (clothe == null)
            {
                return new Response(ResponseCode.BadRequest, "Clothe cannot be null");
            }
            var saveChangesValue = await _clotheDataAccess.AddAsync(clothe);
            if (saveChangesValue > 0)
            {
                return new Response(ResponseCode.Success, "Clothe added successfully");
            }
            return new Response(ResponseCode.BadRequest, "Clothe cannot be added");

        }
        public Task<Response> TDeleteAsync(int clotheId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IList<ClotheListDto>>> TGetAllClotheAsync()
        {
            var clotheEntity = await _clotheDataAccess.GetAllClothe();
            if (clotheEntity.Any())
            {
                return new Response<IList<ClotheListDto>>(ResponseCode.Success, "Clothe list Success.", clotheEntity);
            }
            return new Response<IList<ClotheListDto>>(ResponseCode.NotFound, "Clothe not found");
        }

        public Task<Response> TUpdateAsync(Clothes clothe)
        {
            throw new NotImplementedException();
        }

    }
}
