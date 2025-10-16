using EazyOnRent.Data;
using EazyOnRent.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EazyOnRentContextConnection") ?? throw new InvalidOperationException("Connection string 'EazyOnRentContextConnection' not found.");

builder.Services.AddDbContext<EazyOnRentContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<EazyOnRentContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

AppConfigModel.ImagePath = "C://HostingSpaces//Ssharad//eazyonrent.com//data//wwwroot//";
//AppConfigModel.ImagePath = "D://Software//Rajpal//EazyOnRent//EazyOnRent//wwwroot//";

app.Run();
