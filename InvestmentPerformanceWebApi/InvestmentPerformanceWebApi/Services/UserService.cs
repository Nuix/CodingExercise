using InvestmentPerformanceDomain.Models;
using InvestmentPerformanceDomain.Repository;

namespace InvestmentPerformanceWebApi.Services;

public interface IUserService
{
    Task<IList<User>> GetUsersAsync();
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IList<User>> GetUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }
}
