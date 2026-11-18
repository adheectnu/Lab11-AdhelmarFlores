namespace Lab11___AdhelmarFlores.Domain.Port.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
}
