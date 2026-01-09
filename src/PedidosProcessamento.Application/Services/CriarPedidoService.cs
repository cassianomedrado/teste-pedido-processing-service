using PedidosProcessamento.Application.Common;
using PedidosProcessamento.Application.DTOs.Inputs;
using PedidosProcessamento.Application.Interfaces;
using PedidosProcessamento.Application.Interfaces.Messaging;
using PedidosProcessamento.Application.Interfaces.Repositories;
using PedidosProcessamento.Domain.Entities;

namespace Processamento.Application.Services;

public class CriarPedidoService : ICriarPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IEventPublisher _eventPublisher;

    public CriarPedidoService(
        IPedidoRepository pedidoRepository,
        IEventPublisher eventPublisher)
    {
        _pedidoRepository = pedidoRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result<Guid>> ExecutarAsync(CriarPedidoRequest request)
    {
        try
        {
            var pedido = new Pedido(request.ClienteId, request.ValorTotal);

            await _pedidoRepository.AdicionarAsync(pedido);
            await _eventPublisher.PublicarPedidoCriadoAsync(pedido);

            return Result<Guid>.Success(pedido.Id);
        }
        catch
        {
            return Result<Guid>.Failure(new("ERROR", "Ocorreu um erro ao criar pedido."));
        }
    }
}
