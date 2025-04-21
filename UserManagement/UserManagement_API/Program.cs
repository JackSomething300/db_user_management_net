using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using UserManagement_API.Data;
using UserManagement_Application;
using UserManagement_Application.Interfaces;
using UserManagement_Application.Services;
using UserManagement_Core.Entities;
using UserManagement_Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

if (builder.Environment.IsDevelopment())
{
    // Use InMemory database for development/testing
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseInMemoryDatabase("TestDatabase"));
}
else
{
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    if (builder.Environment.IsDevelopment())
    {
        SeedDatabase(dbContext);
    }
    else
    {
        if (!dbContext.Database.CanConnect())
        {
            dbContext.Database.Migrate();
        }
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Seed method for in-memory database
void SeedDatabase(DataContext context)
{
    context.Users.Add(new User { Id = 1, Name = "John Doe" });
    context.Users.Add(new User { Id = 2, Name = "Jane Doe" });
    context.Users.Add(new User { Id = 3, Name = "Jack Doe" });
    context.Users.Add(new User { Id = 4, Name = "Jake Doe" });
    context.SaveChanges();
}
