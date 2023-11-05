using Microsoft.Extensions.Configuration;
using StudentApp.Core.ApplicationServices.Services;
using StudentApp.Core.ApplicationServices;
using StudentApp.Core.DomainServices;
using StudentApp.Infrastructure.Repositories;
using StudentApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularLocalhost",
    builder => builder
         .WithOrigins("http://localhost:5163", "http://localhost:5089")
         .AllowAnyHeader()
         .AllowAnyMethod()
    .AllowCredentials());
});

builder.Services.AddDbContext<DbContextCore>(con => con.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<DbContextCore>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepostory, StudentRepostory>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
