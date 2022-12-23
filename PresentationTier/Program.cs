using System.Data;
using ApplicationTier.Extensions;
using DataAccessTier;
using DataAccessTier.Extensions;
using DataAccessTier.Models;
using Microsoft.EntityFrameworkCore;

/*var builder = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source=test.db").UseLazyLoadingProxies();
var dbcontext = new DatabaseContext(builder.Options);
/*dbcontext.Employees.Add(new Employee(Guid.NewGuid(), "sss"));#1#

var employee = new Employee(Guid.NewGuid(), "KS");
dbcontext.Employees.Add(employee);
dbcontext.SaveChangesAsync();
var boss = dbcontext.Employees.First(x => x.Id.Equals(Guid.Parse("A7B21E1F-3D5B-4029-BD49-2555EDD86537")));
boss.LedEmployees.Add(employee);
dbcontext.SaveChangesAsync();
Console.WriteLine(dbcontext.Employees.First(x => x.Name.Equals("sss")));*/

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container via extension methods
builder.Services.AddApplication();
builder.Services.AddDataAccess(x => x.UseLazyLoadingProxies().UseSqlite("Data Source=database.db"));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Close including process, build application object
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use safe version Https (only on Win)
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run application with request handling
app.Run();