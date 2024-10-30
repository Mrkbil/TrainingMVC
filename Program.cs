using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Training.DataAccess.Repositories;
using Training.DBAccess.Repositories;
using Training.DBAccess.Repositories.Implement;
using Training.DBAccess.Repositories.Interface;
using TrainingMVC.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Configure the DbContext with PostgreSQL
builder.Services.AddDbContext<TrainingDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLowerCaseNamingConvention());

// Register the Unit of Work and repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IDesignationRepo, DesignationRepo>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
