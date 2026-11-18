using MediatR;
using Lab11___AdhelmarFlores.Application.DTOs;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;

namespace Lab11___AdhelmarFlores.Application.UseCases.Tickets.Queries;

// DTO de respuesta para queries de tickets (lista)
// Ya definido en DTOs/TicketDto.cs - reutilizamos ese

// Query: Retorna todos los tickets
public record ListTicketsQuery : IRequest<IEnumerable<TicketDto>>;

// Handler: Consulta todos los tickets de la base de datos
internal sealed class ListTicketsQueryHandler : IRequestHandler<ListTicketsQuery, IEnumerable<TicketDto>>
{
    private readonly ITicketRepository _ticketRepository;

    public ListTicketsQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<IEnumerable<TicketDto>> Handle(ListTicketsQuery req, CancellationToken ct)
    {
        var tickets = await _ticketRepository.GetAllAsync();
        return tickets.Select(t => new TicketDto(t.TicketId, t.UserId, t.Title, t.Description,
            t.Status, t.CreatedAt, t.ClosedAt, t.User?.Username));
    }
}