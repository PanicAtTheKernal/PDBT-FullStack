using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PDBT_CompleteStack.Data;
using PDBT_CompleteStack.Repository.Classes;
using PDBT_CompleteStack.Repository.Interfaces;
using PDBT_CompleteStack.Services.IssueService;
using PDBT_CompleteStack.Services.LabelService;
using PDBT_CompleteStack.Services.ProjectService;
using PDBT_CompleteStack.Services.UserService;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

MariaDbServerVersion serverVersion = new MariaDbServerVersion(new Version(10,5));

#region Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IIssueRepository, IssueRepository>();
builder.Services.AddTransient<ILabelRepository, LabelRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

#endregion

#region Configuration
var extraConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("extrasettings.json", true)
    .AddEnvironmentVariables()
    .Build();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("extrasettings.json", true)
    .AddEnvironmentVariables()
    .Build();
#endregion

#region Controllers
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
#endregion

#region Database context
builder.Services.AddDbContext<PdbtContext>(opt => opt
    .UseMySql(extraConfig.GetSection("ConnectionStrings:PDBT").Value, serverVersion)
    .UseValidationCheckConstraints()
    .UseEnumCheckConstraints()
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());
#endregion

#region Services
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ILabelService, LabelService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
#endregion

#region Security
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(extraConfig.GetSection("jwttoken:key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
;

app.Run();