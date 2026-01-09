using FluentAssertions;
using FluentValidation.TestHelper;
using PedidosProcessamento.Application.DTOs.Inputs;
using PedidosProcessamento.Application.Validators;

namespace PedidosProcessamento.UnitTests.Validators
{
    public class CriarPedidoRequestValidatorTests
    {
        private readonly CriarPedidoRequestValidator _validator;

        public CriarPedidoRequestValidatorTests()
        {
            _validator = new CriarPedidoRequestValidator();
        }

        [Fact]
        public void ClienteId_NaoDeveSerVazio()
        {
            // Arrange
            var request = new CriarPedidoRequest
            {
                ClienteId = Guid.Empty,
                ValorTotal = 100
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ClienteId)
                  .WithErrorMessage("ClienteId é obrigatório");
        }

        [Fact]
        public void ValorTotal_DeveSerMaiorQueZero()
        {
            // Arrange
            var request = new CriarPedidoRequest
            {
                ClienteId = Guid.NewGuid(),
                ValorTotal = 0
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ValorTotal)
                  .WithErrorMessage("ValorTotal deve ser maior que zero");
        }

        [Fact]
        public void Request_Valido_NaoDeveTerErros()
        {
            // Arrange
            var request = new CriarPedidoRequest
            {
                ClienteId = Guid.NewGuid(),
                ValorTotal = 100
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    
    }
}