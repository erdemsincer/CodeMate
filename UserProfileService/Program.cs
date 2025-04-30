using Microsoft.EntityFrameworkCore;
using UserProfileService.Data;
using UserProfileService.Services;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL baðlan
builder.Services.AddDbContext<UserProfileDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserSkillService, UserProfileService.Services.UserSkillService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
