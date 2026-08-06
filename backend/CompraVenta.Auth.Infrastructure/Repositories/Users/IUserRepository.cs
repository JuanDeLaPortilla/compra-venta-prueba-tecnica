using CompraVenta.Domain.Entities;

namespace CompraVenta.Auth.Infrastructure.Repositories.Users;

public interface IUserRepository
{
    Task<User?> GetByUsername(string username);
}