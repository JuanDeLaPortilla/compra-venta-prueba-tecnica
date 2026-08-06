namespace CompraVenta.Auth.Infrastructure.Repositories.UserRepository;

public interface IUserRepository
{
    Task<Domain.Entities.User?> GetByUsername(string username);
}