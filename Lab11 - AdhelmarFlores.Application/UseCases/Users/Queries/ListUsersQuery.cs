using MediatR;
using Lab11___AdhelmarFlores.Application.DTOs;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;

namespace Lab11___AdhelmarFlores.Application.UseCases.Users.Queries;

// Query: Retorna todos los usuarios del sistema
public record ListUsersQuery : IRequest<IEnumerable<UserDto>>;

// Handler: Consulta todos los usuarios de la base de datos
internal sealed class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public ListUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle(ListUsersQuery req, CancellationToken ct)
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto(u.UserId, u.Username, u.Email, u.CreatedAt));
    }
}
