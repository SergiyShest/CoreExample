using Microsoft.AspNetCore.Authentication.Cookies;
//using Npgsql;

//var Configuration = new ConfigurationBuilder()
//.SetBasePath(Directory.GetCurrentDirectory())
//.AddJsonFile("appsettings.json")
//.Build();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});

builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme, (options) =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

builder.Services.AddHttpContextAccessor();

//var connectionString = Configuration.GetConnectionString("WebApiDatabase");
//var ds = new NpgsqlDataSourceBuilder(connectionString).Build();
//NpgsqlConnection GetConnection() => ds.CreateConnection();
//builder.Services.AddSingleton(GetConnection());
//builder.Services.AddAggregateStore<PostgresStore>();



////builder.WebHost.UseIISIntegration(); options =>
////{
////    options.AllowSynchronousIO = true;
////});
///
//builder.WebHost.UseIISIntegration();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.WebHost.UseKestrel(options =>
 {
     options.AllowSynchronousIO = true;
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();



app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
   pattern: "{controller=Questionnaire2}/{action=Index}/{id?}");

app.Run();
