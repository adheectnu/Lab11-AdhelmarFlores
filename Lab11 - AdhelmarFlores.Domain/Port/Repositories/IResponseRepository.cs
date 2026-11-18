using Lab11___AdhelmarFlores.Domain.Models;

namespace Lab11___AdhelmarFlores.Domain.Port.Repositories;

public interface IResponseRepository : IGenericRepository<Response>
{
    Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId);
}
