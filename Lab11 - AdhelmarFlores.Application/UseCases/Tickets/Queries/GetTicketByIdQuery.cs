using MediatR;
using Lab11___AdhelmarFlores.Application.DTOs;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;

namespace Lab11___AdhelmarFlores.Application.UseCases.Tickets.Queries;

// Query: Obtiene un ticket por su ID con detalles del usuario
public record GetTicketByIdQuery(Guid TicketId) : IRequest<TicketDto?>;

// Handler: Consulta el ticket con sus detalles
internal sealed class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto?>
{
    private readonly ITicketRepository _ticketRepository;

    public GetTicketByIdQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketDto?> Handle(GetTicketByIdQuery req, CancellationToken ct)
    {
        var ticket = await _ticketRepository.GetWithDetailsAsync(req.TicketId);
        if (ticket == null) return null;

        return new TicketDto(ticket.TicketId, ticket.UserId, ticket.Title, ticket.Description,
            ticket.Status, ticket.CreatedAt, ticket.ClosedAt, ticket.User?.Username);
    }
}
