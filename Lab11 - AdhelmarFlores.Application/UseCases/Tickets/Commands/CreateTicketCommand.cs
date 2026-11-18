using MediatR;
using Lab11___AdhelmarFlores.Application.DTOs;
using Lab11___AdhelmarFlores.Domain.Models;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;

namespace Lab11___AdhelmarFlores.Application.UseCases.Tickets.Commands;

// Command: Recibe los datos desde el controlador y retorna el TicketDto creado
public record CreateTicketCommand(string Title, string? Description, Guid UserId) : IRequest<TicketDto>;

// Handler: Procesa la creación del ticket (internal sealed)
internal sealed class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> Handle(CreateTicketCommand req, CancellationToken ct)
    {
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = req.UserId,
            Title = req.Title,
            Description = req.Description,
            Status = "OPEN",
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
        };

        await _ticketRepository.AddAsync(ticket);
        await _unitOfWork.CommitAsync();

        return new TicketDto(ticket.TicketId, ticket.UserId, ticket.Title, ticket.Description,
            ticket.Status, ticket.CreatedAt, ticket.ClosedAt, null);
    }
}