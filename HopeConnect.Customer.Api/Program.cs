using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using HopeConnect.Customer.Api.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Authority = builder.Configuration["Authentication:ValidIssuer"];
		options.Audience = builder.Configuration["Authentication:Audience"];
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
			ValidateAudience = true,
			ValidAudience = builder.Configuration["Authentication:Audience"],
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,		
		};
	});
FirebaseApp.Create(new AppOptions()
{
	Credential = GoogleCredential.FromFile("firebase.json")
});
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((sp, httpClient) =>
{
	var configuration = sp.GetRequiredService<IConfiguration>();
	httpClient.BaseAddress = new Uri(configuration["Authentication:TokenUri"]);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
