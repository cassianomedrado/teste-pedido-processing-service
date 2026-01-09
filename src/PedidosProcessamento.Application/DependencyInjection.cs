using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PedidosProcessamento.Application.Interfaces;
using PedidosProcessamento.Application.Validators;
using Processamento.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CriarPedidoRequestValidator>();

        //Services
        services.AddScoped<ICriarPedidoService, CriarPedidoService>();

        return services;
    }
}
