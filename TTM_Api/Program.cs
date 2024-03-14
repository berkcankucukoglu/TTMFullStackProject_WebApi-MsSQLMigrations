using Microsoft.EntityFrameworkCore;
using TTM.DataAccess;

/*
It writes the trace warnings I wrote in services with flush and produces an ErrorLogs.txt file in the project file.
It overwrites the ErrorLogs.txt file if it already exists or creates it if not.
*/
using System.Diagnostics;
using TTM.Business;
using TTM;
using TTM.Business.Services;
Trace.Listeners.Add(new TextWriterTraceListener("ErrorLogs.txt"));
Trace.AutoFlush = true;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This is dependency injection for our DbContext to get connection string
builder.Services.AddDbContext<TTMContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSqlConnectionString"));
});

builder.Services.AddTransient<TTMContext>();
builder.Services.AddTransient<ICrudService<UserDto>, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
