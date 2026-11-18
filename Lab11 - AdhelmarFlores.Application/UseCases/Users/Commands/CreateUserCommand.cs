using MediatR;
using Lab11___AdhelmarFlores.Application.DTOs;
using Lab11___AdhelmarFlores.Domain.Models;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;

namespace Lab11___AdhelmarFlores.Application.UseCases.Users.Commands;

// Command: Crea un nuevo usuario en el sistema
public record CreateUserCommand(string Username, string Password, string? Email) : IRequest<UserDto>;

// Handler: Procesa la creación del usuario
internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(CreateUserCommand req, CancellationToken ct)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = req.Username,
            PasswordHash = req.Password, // Almacenado simple para pruebas directas
            Email = req.Email,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return new UserDto(user.UserId, user.Username, user.Email, user.CreatedAt);
    }
}
