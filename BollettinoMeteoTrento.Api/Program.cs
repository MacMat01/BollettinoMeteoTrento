#region

using System.DirectoryServices.ActiveDirectory;
using System.Text;
using BollettinoMeteoTrento.Data;
using BollettinoMeteoTrento.Data.DTOs;
using BollettinoMeteoTrento.Services;
using BollettinoMeteoTrento.Services.MeteoServices;
using BollettinoMeteoTrento.Services.UserServices;
using BollettinoMeteoTrento.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoapCore;
using User = BollettinoMeteoTrento.Domain.User;

#endregion
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<MeteoService>();
builder.Services.AddScoped<IMeteoSoapService, MeteoSoapService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddSingleton<IJwtUtils, JwtUtils>();

ConfigureJwtAuthentication(builder.Services, builder.Configuration);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("UserPolicy", static policy => policy.RequireAuthenticatedUser());

builder.Services.AddDbContext<PostgresContext>(dbContextOptionsBuilder => dbContextOptionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSoapEndpoint<IMeteoSoapService>("/MeteoSoapService.svc", new SoapEncoderOptions());

app.Run();
return;

static void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
    string jwtSecret = configuration["Jwt:Secret"]!;
    byte[] signingKey = Encoding.ASCII.GetBytes(jwtSecret);

    services.AddAuthentication(static authenticationOptions =>
    {
        authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(jwtBearerOptions =>
    {
        jwtBearerOptions.RequireHttpsMetadata = false;
        jwtBearerOptions.SaveToken = true;
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(signingKey),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
        jwtBearerOptions.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = static context =>
            {
                Console.WriteLine("Authentication failed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = static context =>
            {
                Console.WriteLine("Token validated: " + context.SecurityToken);
                return Task.CompletedTask;
            },
            OnMessageReceived = static context =>
            {
                Console.WriteLine("Message received from: " + context.Request.Path);
                return Task.CompletedTask;
            }
        };
    });
}
