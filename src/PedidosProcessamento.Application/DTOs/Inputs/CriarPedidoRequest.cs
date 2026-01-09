namespace PedidosProcessamento.Application.DTOs.Inputs
{
    public class CriarPedidoRequest
    {
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
    }
}