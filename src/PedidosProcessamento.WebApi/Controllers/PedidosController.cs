using Microsoft.AspNetCore.Mvc;
using PedidosProcessamento.Application.Common;
using PedidosProcessamento.Application.DTOs.Inputs;
using PedidosProcessamento.Application.Interfaces;

namespace PedidosProcessamento.WebApi.Controllers;

[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly ICriarPedidoService _criarPedidoService;
    private readonly ILogger<PedidosController> _logger;

    public PedidosController(
        ICriarPedidoService criarPedidoService,
        ILogger<PedidosController> logger)
    {
        _criarPedidoService = criarPedidoService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest request)
    {
        _logger.LogInformation("Recebendo solicitação de criação de pedido");

        var result = await _criarPedidoService.ExecutarAsync(request);
        return result.ToRestResult(this);
    }

}
