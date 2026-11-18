using Microsoft.EntityFrameworkCore;
using Lab11___AdhelmarFlores.Domain.Models;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;
using Lab11___AdhelmarFlores.Infrastructure.Persistence;

namespace Lab11___AdhelmarFlores.Infrastructure.Adapters.Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId) =>
        await _dbSet.Where(t => t.UserId == userId).ToListAsync();

    public async Task<Ticket?> GetWithDetailsAsync(Guid id) =>
        await _dbSet.Include(t => t.User).FirstOrDefaultAsync(t => t.TicketId == id);
}
