using Lab11___AdhelmarFlores.Domain.Models;

namespace Lab11___AdhelmarFlores.Domain.Port.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId);
    Task<Ticket?> GetWithDetailsAsync(Guid id);
}
