using PedidosProcessamento.Application.Interfaces.Repositories;
using PedidosProcessamento.Domain.Entities;
using PedidosProcessamento.Infrastructure.Persistence;

namespace PedidosProcessamento.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly PedidoDbContext _context;

    public PedidoRepository(PedidoDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
    }
}
