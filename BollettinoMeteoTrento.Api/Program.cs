#region

using System.Text;
using BollettinoMeteoTrento.Services;
using BollettinoMeteoTrento.Services.UserServices;
using BollettinoMeteoTrento.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SoapCore;

#endregion

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<MeteoService>();
builder.Services.AddScoped<IMeteoSoapService, MeteoSoapService>();

// Configurazione JWT
string secret = builder.Configuration["Jwt:Secret"];
byte[] key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(static authenticationOptions =>
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
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<UserService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// else
// {
//     app.UseHttpsRedirection();
// }

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSoapEndpoint<IMeteoSoapService>("/MeteoSoapService.svc", new SoapEncoderOptions());

app.Run();
