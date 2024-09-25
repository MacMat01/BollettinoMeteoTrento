#region

using BollettinoMeteoTrento.Services;
using SoapCore;

#endregion

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HttpClient
builder.Services.AddHttpClient();

// Register custom services
builder.Services.AddScoped<MeteoService>();
builder.Services.AddScoped<IMeteoSoapService, MeteoSoapService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// else
// {
//     app.UseHttpsRedirection();
// }

app.UseAuthorization();

app.MapControllers();

// Endpoint per il servizio SOAP
app.UseSoapEndpoint<IMeteoSoapService>("/MeteoSoapService.svc", new SoapEncoderOptions());

app.Run();
