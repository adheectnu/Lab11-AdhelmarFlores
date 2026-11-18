namespace Lab11___AdhelmarFlores.Application.DTOs;

public record TicketDto(
    Guid TicketId,
    Guid UserId,
    string Title,
    string? Description,
    string Status,
    DateTime? CreatedAt,
    DateTime? ClosedAt,
    string? Username);
