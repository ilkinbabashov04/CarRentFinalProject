using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.Dependency.Autofac;
using Core.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using FluentValidation.AspNetCore;
using System.Reflection;
using CarRentalAPI.Hubs;
using Business.Concrete;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: "Server=DESKTOP-V1MHL8O\\MSSQLSERVER01;Database=CarRentalDatabase;Integrated Security=True;TrustServerCertificate=True",
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: LogEventLevel.Information
    )
    .CreateLogger();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
//{
//	opt.RequireHttpsMetadata = false;
//	opt.TokenValidationParameters = new TokenValidationParameters
//	{
//		ValidAudience = JwtTokenDefaults.ValidAudience,
//		ValidIssuer = JwtTokenDefaults.ValidIssuer,
//		ClockSkew = TimeSpan.Zero,
//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
//		ValidateLifetime = true,
//		ValidateIssuerSigningKey = true,
//	};
//});
builder.Services.AddHttpClient();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});
builder.Services.AddSignalR();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddControllers();


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en"),
        new CultureInfo("az")
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = JwtTokenDefaults.ValidAudience,
            ValidIssuer = JwtTokenDefaults.ValidIssuer,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RoleClaimType = ClaimTypes.Role
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                // Log the error for debugging purposes
                Console.WriteLine("Authentication failed: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });
Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
builder.Services.AddAuthorization();

// Swagger konfiqurasiyası
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // JWT Bearer üçün Swagger konfiqurasiyası
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token daxil edin. Format: Bearer {your-token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging();

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CarHub>("/carhub");


app.Run();