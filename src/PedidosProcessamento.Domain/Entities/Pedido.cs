using PedidosProcessamento.Domain.Enum;

namespace PedidosProcessamento.Domain.Entities;

public class Pedido
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal ValorTotal { get; private set; }
    public OrderStatusEnum Status { get; private set; }
    public DateTime DataCriacao { get; private set; }

    protected Pedido() { }

    public Pedido(Guid clienteId, decimal valorTotal)
    {
        if (clienteId == Guid.Empty)
            throw new ArgumentException("ClienteId é obrigatório");

        if (valorTotal <= 0)
            throw new ArgumentException("Valor total deve ser maior que zero");

        Id = Guid.NewGuid();
        ClienteId = clienteId;
        ValorTotal = valorTotal;
        Status = OrderStatusEnum.Criado;
        DataCriacao = DateTime.UtcNow;
    }
}
