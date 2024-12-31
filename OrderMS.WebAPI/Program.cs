using System.Text;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.OpenApi.Models;
using Asp.Versioning;

using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;

using OrderMS.Persistence.Services;
using OrderMS.Persistence.DatabaseContext;
using OrderMS.WebAPI.Middlewares;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });


builder.Services.AddSwaggerGen(
    options => options.SwaggerDoc("v1", new OpenApiInfo{ Title = "OrderMS.WebAPI", Version = "v1"})
);

builder.Services.AddDbContext<OrderMSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrderMSConnectionString")));


builder.Services.AddScoped<ITokenRepository, TokenService>();
builder.Services.AddScoped<ICompanyRepository, CompanyService>();
builder.Services.AddScoped<IProductRepository, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderService>();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? throw new ArgumentNullException("Origins not setup");

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
        .WithOrigins(allowedOrigins)
        .AllowCredentials()
        .WithHeaders("Authorization", "origin", "accept", "content-type")
        .WithMethods("GET", "POST", "PUT", "DELETE")
        ;
    });
});



builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("OrderMS")
    .AddEntityFrameworkStores<OrderMSDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


var jwtKey = builder.Configuration[ "Jwt:Key" ]
    ?? throw new InvalidOperationException("JWT Key is not configured.");


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration[ "Jwt:Issuer" ],
    ValidAudience = builder.Configuration[ "Jwt:Audience" ],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();

app.UseHsts();
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
