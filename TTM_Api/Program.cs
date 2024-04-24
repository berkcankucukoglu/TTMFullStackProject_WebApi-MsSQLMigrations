using Microsoft.EntityFrameworkCore;
using TTM.DataAccess;
using System.Diagnostics;
using TTM.Business;
using TTM;
using TTM.Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;

/*
It writes the trace warnings I wrote in services with flush and produces an ErrorLogs.txt file in the project file.
It overwrites the ErrorLogs.txt file if it already exists or creates it if not.
*/
Trace.Listeners.Add(new TextWriterTraceListener("ErrorLogs.txt"));
Trace.AutoFlush = true;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//This is Swagger Authenticate button configuration.
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TTMdotnetWebAPI", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token here",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
        }
    });
});

// This is dependency injection for our DbContext to get connection string
builder.Services.AddDbContext<TTMContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSqlConnectionString"));
});

builder.Services.AddTransient<TTMContext>();
builder.Services.AddTransient<ICrudService<UserDto>, UserService>();
builder.Services.AddTransient<ICrudService<CategoryDto>, CategoryService>();
builder.Services.AddTransient<ICrudService<ProjectDto>, ProjectService>();
builder.Services.AddTransient<ICrudService<DutyDto>, DutyService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", builder => builder
    .WithOrigins("http://localhost:4200", "http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret.....veryverysecret.....")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DefaultPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
