    using Backend_Task.Configurations;
using Backend_Task.Data;
using Backend_Task.Entities;
using Backend_Task.Repository;
using Backend_Task.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("TaskConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddAutoMapper(typeof(AutomapperConfiguration));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPhotoUploader, PhotoUploader>();

builder.Services.AddIdentityCore<User>()
    //Specify the user for identity
    .AddRoles<IdentityRole>()
    //Provides protection and validation for tokens
    .AddTokenProvider<DataProtectorTokenProvider<User>>("BackendTask")
    //Add Entity Framework
    .AddEntityFrameworkStores<ApplicationDbContext>()
    //Adds tokens for other operations
    .AddDefaultTokenProviders();




// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(options =>
{
    /*
     * Specify What type of scheme the app will use by default
     * **/
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Bearer 
    /*
     * The scheme that will be used to challenge users when they try to access a resource when not authenticated 
     * **/
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Validate Signing in key
        ValidateIssuerSigningKey = true, //Validate issuer signing key
        //Validate the issuer of the token
        ValidateIssuer = true, // Validate issuer of the token
        ValidateAudience = true, // Validate the audience 
        ValidateLifetime = true, //Validate the lifetime 
        ClockSkew = TimeSpan.Zero, // make sure time in present
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"], //Validate the issuer of  the token
        ValidAudience = builder.Configuration["JwtSettings:Audience"], // Specifies the audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
        (builder.Configuration["JwtSettings:Key"])) // Key 
    };

});

builder.Services.AddSwaggerGen(options =>
{
 
    //Add JWT Authentication for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the bearer scheme.
                      Enter the 'Bearer' [space] and then you token in the
                      Text Input below ex: 'Bearer abc.123.efg'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                },
                Scheme = "0auth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            }, new List<string>()
        }
    });
});


var app = builder.Build();

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

app.UseStaticFiles();

app.Run();


