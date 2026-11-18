using Microsoft.EntityFrameworkCore;
using Lab11___AdhelmarFlores.Domain.Models;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;
using Lab11___AdhelmarFlores.Infrastructure.Persistence;

namespace Lab11___AdhelmarFlores.Infrastructure.Adapters.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<User?> GetByUsernameAsync(string username) =>
        await _dbSet.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<User?> GetByEmailAsync(string email) =>
        await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<bool> ExistsByUsernameOrEmailAsync(string username, string? email) =>
        await _dbSet.AnyAsync(u => u.Username == username || (email != null && u.Email == email));

    public async Task<User?> GetWithRolesAsync(Guid id) =>
        await _dbSet.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);
}
