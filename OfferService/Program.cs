using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OfferService.Data;
using System.Text;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<OfferDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("PostgresConnection")));

builder.Services.AddMassTransit(x =>
{
    // Otomatik tüm publish/consumer konfigürasyonu yapýlsýn
    x.SetKebabCaseEndpointNameFormatter();

    // Eðer ileride consumer olacaksa buraya eklenebilir:
    // x.AddConsumer<MyEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(config["RabbitMq:Host"], "/", h =>
        {
            h.Username(config["RabbitMq:Username"]);
            h.Password(config["RabbitMq:Password"]);
        });

        // Eðer consumer tanýmý yapýlýrsa burada queue tanýmý yapýlmalý
        // cfg.ReceiveEndpoint("my-queue", e => { e.ConfigureConsumer<MyEventConsumer>(context); });
    });
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]!))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "OfferService", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
