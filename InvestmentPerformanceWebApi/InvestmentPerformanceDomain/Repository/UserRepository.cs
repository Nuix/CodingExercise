using InvestmentPerformanceDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceDomain.Repository;

public interface IUserRepository
{
    Task<IList<User>> GetUsersAsync();
    Task<bool> UserExistsAsync(int userId);
}

public class UserRepository : IUserRepository
{
    private readonly InvestmentPerformanceContext _context;

    public UserRepository(InvestmentPerformanceContext context)
    {
        _context = context;
    }

    public async Task<IList<User>> GetUsersAsync()
    {
        return await _context.Users
            .OrderBy(u => u.Id)
            .ToListAsync();
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        return user != null;
    }
}

