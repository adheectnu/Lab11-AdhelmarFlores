using Lab11___AdhelmarFlores.Domain.Port.Repositories;
using Lab11___AdhelmarFlores.Infrastructure.Persistence;

namespace Lab11___AdhelmarFlores.Infrastructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context) => _context = context;

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
