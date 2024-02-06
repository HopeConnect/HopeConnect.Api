using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure.Dto;
using HopeConnect.Customer.Api.Infrastructure.Model;
using HopeConnect.Customer.Api.Shared.ComplexTypes;
using HopeConnect.Customer.Api.Shared.Concrete;

namespace HopeConnect.Customer.Api.BusinessUnit
{
	public interface IRecipientBusinessUnit
	{
		Task<Response> AddAsync(Recipient recipient);
		Task<Response<IList<RecipientListDto>>> TGetAllRecipientAsync();
		Task<Response<IList<RecipientListDto>>> GetRecipientByRecipientType(int recipientType);
	}
	public class RecipientBusinessUnit : IRecipientBusinessUnit
	{
		private readonly IRecipientDataAccess _recipientDataAccess;
		public RecipientBusinessUnit(IRecipientDataAccess recipientDataAccess)
		{
			_recipientDataAccess = recipientDataAccess;
		}
		public async Task<Response> AddAsync(Recipient recipient)
		{
			if (recipient == null)
			{
				return new Response(ResponseCode.BadRequest, "Recipient cannot be null");
			}
			var saveChangesValue = await _recipientDataAccess.AddAsync(recipient);
			if (saveChangesValue > 0)
			{
				return new Response(ResponseCode.Success, "Recipient added successfully");
			}
			return new Response(ResponseCode.BadRequest, "Recipient cannot be added");
		}
		public async Task<Response<IList<RecipientListDto>>> TGetAllRecipientAsync()
		{
			var recipientEntity = await _recipientDataAccess.GetAllRecipient();
			if (recipientEntity.Any())
			{
				return new Response<IList<RecipientListDto>>(ResponseCode.Success, "Recipient list Success.", recipientEntity);
			}
			return new Response<IList<RecipientListDto>>(ResponseCode.NotFound, "Recipient not found");
		}

		public async Task<Response<IList<RecipientListDto>>> GetRecipientByRecipientType(int recipientType)
		{
			if (recipientType == 0)
			{
				return new Response<IList<RecipientListDto>>(ResponseCode.BadRequest, "RecipientType cannot be null");
			}
			var recipientEntity = await _recipientDataAccess.GetRecipientByRecipientType(recipientType);
			if (recipientEntity.Any())
			{
				return new Response<IList<RecipientListDto>>(ResponseCode.Success, "Recipient list Success.", recipientEntity);
			}
			return new Response<IList<RecipientListDto>>(ResponseCode.NotFound, "Recipient not found");

		}
	}
}
