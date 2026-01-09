using PedidosProcessamento.Domain.Entities;

namespace PedidosProcessamento.Application.Interfaces.Repositories;

public interface IPedidoRepository
{
    Task AdicionarAsync(Pedido pedido);
}
