using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lab11___AdhelmarFlores.Application.UseCases.Diagnostics.Queries;

namespace Lab11___AdhelmarFlores.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiagnosticsController(IMediator mediator) => _mediator = mediator;

    // GET /api/diagnostics/ping-cqrs — Verifica que CQRS + MediatR funcionan con la BD
    [HttpGet("ping-cqrs")]
    public async Task<IActionResult> Ping() =>
        Ok(await _mediator.Send(new PingCqrsQuery()));
}
