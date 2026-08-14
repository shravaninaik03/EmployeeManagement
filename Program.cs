using Emp.Data;
using Emp.Services;
using Emp.Repositories;
using Emp.Interfaces;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Emp.Mappings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens.Experimental;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/app.log").CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => { },typeof(EmployeeUpdateProfile)); // Empty config required by AutoMapper 16

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
AddJwtBearer(options=>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey= true,
        IssuerSigningKey =key,
          ValidateIssuer = false,
        ValidateAudience = false,

        ValidateLifetime = true
    };
} );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

 builder.Host.UseSerilog();
var connectionString= builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();


var app = builder.Build();
app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler("/error");
app.Map("/error", ((HttpContext context, ILogger<Program> logger) =>
{
     var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
    logger.LogError(exception, "An unhandled exception occurred.");

    return Results.Problem(
        title: "Something went wrong",
        detail: "An unexpected error occurred. Please try again later :).",
        statusCode: 500
    );
}));

app.MapControllers();

app.Run();