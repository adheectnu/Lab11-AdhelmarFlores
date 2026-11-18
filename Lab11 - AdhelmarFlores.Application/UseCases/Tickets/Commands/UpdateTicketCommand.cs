using MediatR;
using Lab11___AdhelmarFlores.Application.DTOs;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;

namespace Lab11___AdhelmarFlores.Application.UseCases.Tickets.Commands;

// Command: Actualiza título y descripción de un ticket existente
public record UpdateTicketCommand(Guid TicketId, string Title, string? Description) : IRequest<TicketDto?>;

// Handler: Procesa la actualización del ticket
internal sealed class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketDto?>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTicketCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto?> Handle(UpdateTicketCommand req, CancellationToken ct)
    {
        var ticket = await _ticketRepository.GetWithDetailsAsync(req.TicketId);
        if (ticket == null) return null;

        ticket.Title = req.Title;
        ticket.Description = req.Description;

        await _ticketRepository.UpdateAsync(ticket);
        await _unitOfWork.CommitAsync();

        return new TicketDto(ticket.TicketId, ticket.UserId, ticket.Title, ticket.Description,
            ticket.Status, ticket.CreatedAt, ticket.ClosedAt, ticket.User?.Username);
    }
}
