using FluentAssertions;
using Moq;
using PedidosProcessamento.Application.DTOs.Inputs;
using PedidosProcessamento.Application.Interfaces.Messaging;
using PedidosProcessamento.Application.Interfaces.Repositories;
using PedidosProcessamento.Domain.Entities;
using Processamento.Application.Services;

namespace PedidosProcessamento.UnitTests.Services
{
    public class CriarPedidoServiceTests
    {
        [Fact]
        public async Task Deve_criar_pedido_com_sucesso()
        {
            // Arrange
            var repoMock = new Mock<IPedidoRepository>();
            var publisherMock = new Mock<IEventPublisher>();

            var service = new CriarPedidoService(
                repoMock.Object,
                publisherMock.Object);

            var request = new CriarPedidoRequest
            {
                ClienteId = Guid.NewGuid(),
                ValorTotal = 100
            };

            // Act
            var result = await service.ExecutarAsync(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            repoMock.Verify(r => r.AdicionarAsync(It.IsAny<Pedido>()), Times.Once);
            publisherMock.Verify(p => p.PublicarPedidoCriadoAsync(It.IsAny<Pedido>()), Times.Once);
        }
    }
}