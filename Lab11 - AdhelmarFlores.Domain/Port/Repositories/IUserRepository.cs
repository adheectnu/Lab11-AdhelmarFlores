using Lab11___AdhelmarFlores.Domain.Models;

namespace Lab11___AdhelmarFlores.Domain.Port.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsByUsernameOrEmailAsync(string username, string? email);
    Task<User?> GetWithRolesAsync(Guid id);
}
