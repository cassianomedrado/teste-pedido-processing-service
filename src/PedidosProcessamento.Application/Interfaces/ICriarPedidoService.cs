using PedidosProcessamento.Application.Common;
using PedidosProcessamento.Application.DTOs.Inputs;

namespace PedidosProcessamento.Application.Interfaces
{
    public interface ICriarPedidoService
    {
        Task<Result<Guid>> ExecutarAsync(CriarPedidoRequest request);
    }
}
