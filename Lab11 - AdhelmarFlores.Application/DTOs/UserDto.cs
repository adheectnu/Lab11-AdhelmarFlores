namespace Lab11___AdhelmarFlores.Application.DTOs;

public record UserDto(Guid UserId, string Username, string? Email, DateTime? CreatedAt);
