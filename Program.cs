using AutoMapper;
using Microsoft.EntityFrameworkCore;
using truckCity_api;
using truckCity_api.Data;
using truckCity_api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Mapper Service
IMapper mapper = MappingConfiguration.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DbContext Service
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);
// Repositories Services
builder.Services.AddScoped<IPartRepository, PartRepository>();
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<ITruckRepository, TruckRepository>();

// Controllers Service
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
