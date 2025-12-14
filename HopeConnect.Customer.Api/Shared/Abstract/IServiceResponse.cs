using HopeConnect.Customer.Api.Shared.ComplexTypes;

namespace HopeConnect.Customer.Api.Shared.Abstract
{
	public interface IServiceResponse
	{
		ResponseCode ResponseCode { get;}
		string Message { get; }
	}
}
