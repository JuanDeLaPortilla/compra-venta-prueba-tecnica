using CompraVenta.Domain.Entities;
using CompraVenta.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CompraVenta.Auth.Infrastructure.Repositories.UserRepository;

public class UserRepository(CompraVentaDbContext db) : IUserRepository
{
    public async Task<User?> GetByUsername(string username)
    {
        return await db.Users
            .AsNoTracking()
            .Where(x => x.Username == username)
            .FirstOrDefaultAsync();
    }
}