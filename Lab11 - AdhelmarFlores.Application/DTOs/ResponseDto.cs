namespace Lab11___AdhelmarFlores.Application.DTOs;

public record ResponseDto(
    Guid ResponseId,
    Guid TicketId,
    Guid ResponderId,
    string Message,
    DateTime? CreatedAt,
    string? ResponderUsername);
