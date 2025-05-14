using ContactManager.Application.Interfaces;
using ContactManager.Application.Mappings;
using ContactManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Application.Extensions;

/// <summary>
/// Класс расширений для добавления сервисов в контейнер зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистация основных сервисов приложения в коллекции служб.
    /// </summary>
    /// <param name="services">Коллекция служб <see cref="IServiceCollection"/> для конфигурации зависимостей.</param>
    public static void AddCoreApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();
        services.AddAutoMapper(typeof(ContactMappingProfile));
    }
}