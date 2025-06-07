using CarDealer.Infrastructure;
using CarDealer.WebApi.GraphQL;
using CarDealer.WebApi.GraphQL.Queries;
using CarDealer.WebApi.GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CarDealer.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarDealer API", Version = "v1" });
    // Add JWT support to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field. Example: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add DbContext
builder.Services.AddDbContext<CarDealerDbContext>(options =>
    options.UseInMemoryDatabase("CarDealerDb"));

// Add Identity
builder.Services.AddDbContext<CarDealer.Infrastructure.CarDealerDbContext>(options =>
    options.UseInMemoryDatabase("CarDealerDb"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<CarDealer.Infrastructure.CarDealerDbContext>()
.AddDefaultTokenProviders();

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();

// Add GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<CarQueries>()
    .AddTypeExtension<CustomerQueries>()
    .AddTypeExtension<SellerQueries>()
    .AddTypeExtension<TransactionQueries>()
    .AddType<CarType>()
    .AddType<CustomerType>()
    .AddType<SellerType>()
    .AddType<TransactionType>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarDealer API v1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<CustomHeaderMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Add GraphQL endpoint
app.MapGraphQL();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CarDealerDbContext>();
    DbInitializer.Initialize(context);
}

// Seed admin user and role
using (var scope = app.Services.CreateScope())
{
    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (!await roleMgr.RoleExistsAsync("Admin"))
        await roleMgr.CreateAsync(new IdentityRole("Admin"));

    var admin = await userMgr.FindByEmailAsync("admin@localhost");
    if (admin == null)
    {
        admin = new IdentityUser
        {
            UserName = "admin@localhost",
            Email = "admin@localhost",
            EmailConfirmed = true
        };
        var result = await userMgr.CreateAsync(admin, "Admin123!");
        if (result.Succeeded)
        {
            await userMgr.AddToRoleAsync(admin, "Admin");
        }
    }
}

app.Run();
