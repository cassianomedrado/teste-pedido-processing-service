using PedidosProcessamento.Domain.Entities;

namespace PedidosProcessamento.Application.Interfaces.Messaging;

public interface IEventPublisher
{
    Task PublicarPedidoCriadoAsync(Pedido pedido);
}
