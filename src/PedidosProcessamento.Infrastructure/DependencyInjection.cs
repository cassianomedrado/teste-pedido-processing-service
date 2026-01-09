using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PedidosProcessamento.Application.Interfaces.Messaging;
using PedidosProcessamento.Application.Interfaces.Repositories;
using PedidosProcessamento.Infrastructure.Messaging;
using PedidosProcessamento.Infrastructure.Persistence;
using PedidosProcessamento.Infrastructure.Repositories;

namespace PedidosProcessamento.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PedidoDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));

        //Repositorios
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        //Menssageria
        services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();

        return services;
    }
}
