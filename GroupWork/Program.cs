using GroupWork.DAL;
using GroupWork.Data;
using GroupWork.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ManagementDataContextClass>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddScoped<UserInterface, UserRepository>();
builder.Services.AddScoped<EmployeeInterface, EmployeeRepo>();
builder.Services.AddScoped<PermissionInterface, PermissionRepo>();
builder.Services.AddScoped<CompanyInterface, CompanyRepo>();
builder.Services.AddScoped<ExperienceInterface, ExperienceRepo>();
builder.Services.AddScoped<SocialInterface, SocialRepo>();
builder.Services.AddScoped<SkillsInterface, SkillsRepo>();
builder.Services.AddScoped<QualificationInterface, QualificationRepo>();
builder.Services.AddScoped<SalaryInterface, SalaryRepo>();
var app = builder.Build();
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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
