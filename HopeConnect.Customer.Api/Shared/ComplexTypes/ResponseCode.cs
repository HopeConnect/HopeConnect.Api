namespace HopeConnect.Customer.Api.Shared.ComplexTypes
{
	public enum ResponseCode
	{
		Success = 200,
		NotFound = 404,
		InternalServerError = 500,
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403,
		Conflict = 409,
		ValidationError = 422,
		NoContent = 204,
		NotModified = 304,
		NotImplemented = 501,
		PreconditionFailed = 412,
		UnsupportedMediaType = 415,
		TooManyRequests = 429,

	}
}
