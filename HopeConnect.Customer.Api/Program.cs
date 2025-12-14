using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using HopeConnect.Customer.Api.BusinessUnit;
using HopeConnect.Customer.Api.DataAccess;
using HopeConnect.Customer.Api.Infrastructure;
using HopeConnect.Customer.Api.Infrastructure.Cloud;
using HopeConnect.Customer.Api.Infrastructure.Utility;
using HopeConnect.Customer.Api.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HopeConnect.Customer.Api", Version = "v1" });
});
builder.Services.AddDbContext<HopeConnectContext>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserUtility, UserUtility>();
builder.Services.AddTransient<IUserBusinessUnit, UserBusinessUnit>();
builder.Services.AddTransient<IUserDataAccess, UserDataAccess>();
builder.Services.AddTransient<IRecipientBusinessUnit,RecipientBusinessUnit>();
builder.Services.AddTransient<IRecipientDataAccess, RecipientDataAccess>();
builder.Services.AddTransient<IUserActivitiyDataAccess, UserActivitiyDataAccess>();
builder.Services.AddTransient<IUserActivitiyBusinessUnit,  UserActivitiyBusinessUnit>();
builder.Services.AddTransient<IGoogleCloudStroge,GoogleCloudStroge>();
builder.Services.AddTransient<IImageUtility, ImageUtility>();

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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hope Connect Customer API v1"));
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
