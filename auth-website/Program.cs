using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(options => {
            builder.Configuration.GetSection("AzureADB2C").Bind(options);
            options.ClientSecret = Environment.GetEnvironmentVariable("B2CClientSecret");
            options.Instance = $"https://{Environment.GetEnvironmentVariable("B2CDomainPrefix")}.b2clogin.com";
            options.Domain = $"{Environment.GetEnvironmentVariable("B2CDomainPrefix")}.onmicrosoft.com";
            options.TenantId = Environment.GetEnvironmentVariable("B2CTenantId");
            options.ClientId = Environment.GetEnvironmentVariable("B2CClientId");
        });

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to 
    // the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages(options => {
    options.Conventions.AllowAnonymousToPage("/Index");
})
.AddMvcOptions(options => { })
.AddMicrosoftIdentityUI();

IdentityModelEventSource.ShowPII = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
