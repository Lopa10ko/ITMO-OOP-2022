using ApplicationTier.Services;
using ApplicationTier.Services.Implementations;
using DataAccessTier.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationTier.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IEmployee, EmployeeService>();
        collection.AddScoped<IMessage, MessageService>();
        collection.AddScoped<IReport, ReportService>();
        return collection;
    }
}