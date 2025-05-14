using ContactManager.Domain.Interfaces;
using ContactManager.Persistence.Data;
using ContactManager.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructureRepositoriesServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddAppPsqlDbContext(services, configuration);
        services.AddScoped<IContactRepository, ContactRepository>();

    }

    private static void AddAppPsqlDbContext(this IServiceCollection services,  IConfiguration configuration)
    {
        var postgresConnection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContextFactory<ContactManagerDbContext>(options => options.UseNpgsql(postgresConnection));

    }
    
}