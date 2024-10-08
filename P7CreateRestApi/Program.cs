using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Services;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Configure Serilog to write logs to a file
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("P7CreateRestApi.log", rollingInterval: RollingInterval.Day) // One file per day
    .CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "P7CreateRestApi",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Scheme = "Bearer",
        Name = "Authorization",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    // Get the XML comments file path
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Configure DbContext and Identity
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<LocalDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
var jwt = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwt["SecretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
        policy.RequireAuthenticatedUser();
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
    });

    options.AddPolicy("User", policy =>
    {
        policy.RequireRole("User", "Admin");
        policy.RequireAuthenticatedUser();
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
    });
});

// Register repositories and services
builder.Services.AddScoped<ICurvePointRepository, CurvePointRepository>();
builder.Services.AddScoped<ICurvePointService, CurvePointService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRuleNameRepository, RuleNameRepository>();
builder.Services.AddScoped<IRuleNameService, RuleNameService>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IBidListRepository, BidListRepository>();
builder.Services.AddScoped<IBidListService, BidListService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Ensure database is migrated in Development mode
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbcontext = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
        dbcontext.Database.Migrate();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Serilog to record logs
app.UseSerilogRequestLogging();

// Call the role and administrator initialization method
await InitializeRolesAndAdminUserAsync(app.Services);

// Apply authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


// Method for initializing roles and admin user
static async Task InitializeRolesAndAdminUserAsync(IServiceProvider serviceProvider)
{
    try
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole<int> { Name = "User" });
            await roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });

            var admins = await userManager.GetUsersInRoleAsync("Admin");

            if (admins.Count == 0)
            {
                var user = new User
                {
                    UserName = "admin",
                    FullName = "Super Admin",
                    Role = "Admin",
                };

                var result = await userManager.CreateAsync(user, "Ls7N0U7tmZ48!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, user.Role);
                    logger.LogInformation("Admin user created successfully.");
                }
                else
                {
                    logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                logger.LogInformation("Admin user already exists.");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing roles and admin user.");
    }
}