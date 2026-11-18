using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lab11___AdhelmarFlores.Application.UseCases.Tickets.Commands;
using Lab11___AdhelmarFlores.Application.UseCases.Tickets.Queries;

namespace Lab11___AdhelmarFlores.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator) => _mediator = mediator;

    // GET /api/tickets — Lista todos los tickets (Query)
    [HttpGet]
    public async Task<IActionResult> List() =>
        Ok(await _mediator.Send(new ListTicketsQuery()));

    // GET /api/tickets/{id} — Obtiene un ticket por ID (Query)
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTicketByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    // POST /api/tickets — Crea un nuevo ticket (Command)
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.TicketId }, result);
    }

    // Modelo para actualizar solo título y descripción
    public record UpdateRequest(string Title, string? Description);

    // PUT /api/tickets/{id} — Actualiza un ticket (Command)
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRequest req)
    {
        var result = await _mediator.Send(new UpdateTicketCommand(id, req.Title, req.Description));
        return result != null ? Ok(result) : NotFound();
    }

    // POST /api/tickets/{id}/close — Cierra un ticket (Command)
    [HttpPost("{id:guid}/close")]
    public async Task<IActionResult> Close(Guid id)
    {
        var result = await _mediator.Send(new CloseTicketCommand(id));
        return result != null ? Ok(result) : NotFound();
    }
}
