using MediatR;
using Lab11___AdhelmarFlores.Application.DTOs;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;

namespace Lab11___AdhelmarFlores.Application.UseCases.Tickets.Commands;

// Command: Cierra un ticket cambiando su estado a CLOSED
public record CloseTicketCommand(Guid TicketId) : IRequest<TicketDto?>;

// Handler: Procesa el cierre del ticket
internal sealed class CloseTicketCommandHandler : IRequestHandler<CloseTicketCommand, TicketDto?>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseTicketCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto?> Handle(CloseTicketCommand req, CancellationToken ct)
    {
        var ticket = await _ticketRepository.GetWithDetailsAsync(req.TicketId);
        if (ticket == null) return null;

        ticket.Status = "CLOSED";
        ticket.ClosedAt = DateTime.UtcNow;

        await _ticketRepository.UpdateAsync(ticket);
        await _unitOfWork.CommitAsync();

        return new TicketDto(ticket.TicketId, ticket.UserId, ticket.Title, ticket.Description,
            ticket.Status, ticket.CreatedAt, ticket.ClosedAt, ticket.User?.Username);
    }
}
