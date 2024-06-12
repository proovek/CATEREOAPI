using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CatereoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CatereoAPI.Repository;
using CatereoAPI.Model;
using Microsoft.AspNetCore.Authorization;
using CatereoAPI.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at ht

builder.Services.AddCors(options =>
 {
     options.AddDefaultPolicy(
     builder =>
     {
         builder.WithOrigins("http://gosciniec.catereo.pl", "http://localhost:3031").AllowAnyHeader().AllowAnyMethod();
     });
});
// Add services to the container.tps://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DBO Connection
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("SQLConnection")), ServiceLifetime.Transient);
//builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");
builder.Services.AddHangfire(configuration => configuration
    .UsePostgreSqlStorage(hangfireConnectionString));
builder.Services.AddHangfireServer();

//Dodawaæ zawsze gdy pojawia siê ob³usga nowej tabeli
builder.Services.AddScoped<UserRepositoryInterface, UsersRepository>();
builder.Services.AddScoped<EmployeesWorkingTimeInterface, EmployeesWorkingTimeRepository>();
builder.Services.AddScoped<WarehouseRepositoryInterface, WarehouseRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuCardRepository, MenuCardRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();
builder.Services.AddScoped<ICustomerCompanyRepository, CustomerCompanyRepository>();
builder.Services.AddScoped<IOrderPaymentsRepository, OrderPaymentsRepository>();
builder.Services.AddScoped<IOrderShipmentRepository, OrderShipmentRepository>();
builder.Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
builder.Services.AddScoped<IUserDataRepositoryDTO, UserDataRepositoryDTO>();
builder.Services.AddScoped<CreditsService>();
builder.Services.AddScoped<CreditsCalculationService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CATEREO API",
        Description = "Api stworzone na rzecz dzia³ania oprogramowania dedykowanego. Oprogramowanie stworzone przez firmê IRONSOFT KAMIL PUCHA£A",
        TermsOfService = new Uri("https://relicone.pl"),
        Contact = new OpenApiContact
        {
            Name = "Portfolio",
            Url = new Uri("https://relicone.pl")
        },
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Proszê wpisaæ API Token",
        Name = "Autoryzacja",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
        }
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidAudience = configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(configuration["JWT:secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseProblemDetails();

app.UseStatusCodePages();

app.MapControllers();

app.UseHangfireDashboard();

// Skonfiguruj zaplanowane zadanie za pomoc¹ Hangfire
app.Services.GetService<IRecurringJobManager>()?.AddOrUpdate<CreditsService>(
    "RenewCredits",
    service => service.RenewCreditsForAllUsers(),
    "0 0 1 * *", // Wyra¿enie Cron: o pó³nocy pierwszego dnia ka¿dego miesi¹ca
    TimeZoneInfo.Local
);

app.Run();

