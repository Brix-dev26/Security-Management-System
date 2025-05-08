using Domain.IServices;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Shared.Domain;
using Shared.InfraStructure;
using System.Text;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnTemplate"))
);


// builder.Services.AddDbContext<OnlineRegistrationContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineRegistrationDb"))
// );

builder.Services.AddScoped<DbContext, ApplicationDataContext>();
// builder.Services.AddScoped<DbContext, OnlineRegistrationContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddHttpClient();

builder.Services.AddScoped<HttpClient, HttpClient>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<IEmergencyEventService, EmergencyEventService>();
builder.Services.AddScoped<IInvolvedPartyService, InvolvedPartyService>();
builder.Services.AddScoped<ISecurityStaffService, SecurityStaffService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<ILogEntryService, LogEntryService>();
builder.Services.AddScoped<IGateService, GateService>();
builder.Services.AddScoped<ICampusService, CampusService>();
builder.Services.AddScoped<IStaffManagerService, StaffManagerService>();
builder.Services.AddScoped<IStaffManagerCampusSevices , StaffManagerCampusService>();



builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new UpperCaseWordsNamingStrategy()
        };
    });

builder.Services.AddControllers(options =>
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS_Origin", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Convert.FromHexString(jwtSettings["Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = "UserRole"
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS
app.UseCors("CORS_Origin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
