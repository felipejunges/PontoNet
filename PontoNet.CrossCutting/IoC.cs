using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PontoNet.Domain.Interfaces;
using PontoNet.Domain.Interfaces.Repositories;
using PontoNet.Domain.Notifications;
using PontoNet.Domain.Notifications.Interfaces;
using PontoNet.Infra.Context;
using PontoNet.Infra.Repositories;

namespace PontoNet.CrossCutting;

public static class Ioc
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFechamentoMesRepository, FechamentoMesRepository>()
                .AddScoped<IRegistroRepository, RegistroRepository>();

        return services;
    }

    public static IServiceCollection InjectDatabase(this IServiceCollection services)
    {
        services.AddDbContext<PontoContext>();

        return services;
    }

    public static IServiceCollection InjectUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection InjectMediator(this IServiceCollection services)
    {
        var domainAssembly = AppDomain.CurrentDomain.Load("PontoNet.Domain");

        services.AddMediatR(domainAssembly);

        return services;
    }

    public static IServiceCollection InjectNotification(this IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();

        return services;
    }
}
