using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lab11___AdhelmarFlores.Domain.Port.Repositories;
using Lab11___AdhelmarFlores.Infrastructure.Adapters.Repositories;
using Lab11___AdhelmarFlores.Infrastructure.Persistence;

namespace Lab11___AdhelmarFlores.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=lab10;Username=postgres;Password=admin";

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connStr));
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
