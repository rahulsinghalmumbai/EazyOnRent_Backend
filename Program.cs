using EazyOnRent.Data;
using EazyOnRent.Model;
using EazyOnRent.UserChatHub;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("EazyOnRentContextConnection")
    ?? throw new InvalidOperationException("Connection string 'EazyOnRentContextConnection' not found.");

builder.Services.AddDbContext<EazyOnRentContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EazyOnRentContext>();

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//AppConfigModel.ImagePath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");

AppConfigModel.ImagePath = @"C:\HostingSpaces\Ssharad\eazyonrent.com\data";

if (!Directory.Exists(AppConfigModel.ImagePath))
{
    Directory.CreateDirectory(AppConfigModel.ImagePath);
}

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(AppConfigModel.ImagePath));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(AppConfigModel.ImagePath),
    RequestPath = "/images",  
    ServeUnknownFileTypes = true
});

app.Run();
