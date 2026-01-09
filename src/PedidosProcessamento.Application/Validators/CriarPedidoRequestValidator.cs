using FluentValidation;
using PedidosProcessamento.Application.DTOs.Inputs;

namespace PedidosProcessamento.Application.Validators;

public class CriarPedidoRequestValidator : AbstractValidator<CriarPedidoRequest>
{
    public CriarPedidoRequestValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotEmpty()
            .WithMessage("ClienteId é obrigatório");

        RuleFor(x => x.ValorTotal)
            .GreaterThan(0)
            .WithMessage("ValorTotal deve ser maior que zero");
    }
}
