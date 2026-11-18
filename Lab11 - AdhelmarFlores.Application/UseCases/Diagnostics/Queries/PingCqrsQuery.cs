using MediatR;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;

namespace Lab11___AdhelmarFlores.Application.UseCases.Diagnostics.Queries;

// Respuesta del diagnóstico CQRS
public record PingCqrsResponse(
    string Status,
    string Message,
    DateTime Timestamp,
    int TotalTickets,
    int TotalUsers);

// Query: Verifica que MediatR y los repositorios funcionan correctamente
public record PingCqrsQuery : IRequest<PingCqrsResponse>;

// Handler: Consulta conteos reales de la base de datos para diagnóstico
internal sealed class PingCqrsQueryHandler : IRequestHandler<PingCqrsQuery, PingCqrsResponse>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;

    public PingCqrsQueryHandler(ITicketRepository ticketRepository, IUserRepository userRepository)
    {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
    }

    public async Task<PingCqrsResponse> Handle(PingCqrsQuery req, CancellationToken ct)
    {
        var tickets = await _ticketRepository.GetAllAsync();
        var users = await _userRepository.GetAllAsync();

        return new PingCqrsResponse(
            Status: "OK",
            Message: "CQRS + MediatR funcionando correctamente sin tokens.",
            Timestamp: DateTime.UtcNow,
            TotalTickets: tickets.Count(),
            TotalUsers: users.Count()
        );
    }
}
