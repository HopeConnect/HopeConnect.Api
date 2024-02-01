using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
    public interface IEducationBusinessUnit
    {
        Task<Response> TAddAsync(Education education);
        Task<Response> TUpdateAsync(Education education);
        Task<Response> TDeleteAsync(int educationId);
        Task<Response<IList<EducationListDto>>> TGetAllEducationAsync();
    }
    public class EducationBusinessUnit: IEducationBusinessUnit
    {
        private readonly IEducationDataAccess _educationDataAccess;
        public EducationBusinessUnit(IEducationDataAccess educationDataAccess)
        {
            _educationDataAccess = educationDataAccess;
        }
        public async Task<Response> TAddAsync(Education education)
        {
            if (education == null)
            {
                return new Response(ResponseCode.BadRequest, "Education cannot be null");
            }
            var saveChangesValue = await _educationDataAccess.AddAsync(education);
            if (saveChangesValue > 0)
            {
                return new Response(ResponseCode.Success, "Education added successfully");
            }
            return new Response(ResponseCode.BadRequest, "Education cannot be added");

        }

        public Task<Response> TDeleteAsync(int educationId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IList<EducationListDto>>> TGetAllEducationAsync()
        {
            var educationEntity = await _educationDataAccess.GetAllEducation();
            if (educationEntity.Any())
            {
                return new Response<IList<EducationListDto>>(ResponseCode.Success, "Education list Success.", educationEntity);
            }
            return new Response<IList<EducationListDto>>(ResponseCode.NotFound, "Education not found");
        }

        public Task<Response> TUpdateAsync(Education education)
        {
            throw new NotImplementedException();
        }

    }
}
