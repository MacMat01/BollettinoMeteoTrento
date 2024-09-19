#region

using bollettino_meteo_trento.web.Services;
using SoapCore;

#endregion
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrazione di MeteoService
builder.Services.AddHttpClient<MeteoService>();

// Registrazione e configurazione di MeteoService come un servizio WCF
builder.Services.AddScoped<IMeteoSoapService, MeteoSoapService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

// Endpoint per il servizio SOAP
app.UseSoapEndpoint<IMeteoSoapService>("/MeteoSoapService.svc", new SoapEncoderOptions());

app.Run();
