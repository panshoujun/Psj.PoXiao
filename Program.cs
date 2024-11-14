using Microsoft.EntityFrameworkCore;
using Psj.PoXiao.EFModel;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Á´½Ó SQLServe
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Á´½Ó Postgresql
builder.Services.AddDbContext<UserServiceDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
