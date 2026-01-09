using Microsoft.Extensions.Options;
using PedidosProcessamento.Application.Interfaces.Messaging;
using PedidosProcessamento.Domain.Entities;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PedidosProcessamento.Infrastructure.Messaging;

public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly RabbitMqSettings _settings;

    public RabbitMqEventPublisher(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;
    }

    public async Task PublicarPedidoCriadoAsync(Pedido pedido)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.User,
            Password = _settings.Password
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var payload = new
        {
            PedidoId = pedido.Id,
            ClienteId = pedido.ClienteId,
            ValorTotal = pedido.ValorTotal,
            DataCriacao = pedido.DataCriacao
        };

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload));

        var props = new BasicProperties
        {
            Persistent = false
        };

        await channel.BasicPublishAsync<BasicProperties>(
            exchange: "",
            routingKey: _settings.QueueName,
            mandatory: false,
            basicProperties: props,
            body: body);
    }
}
