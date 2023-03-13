using CodingExercise.Data;
using CodingExercise.Models;
using CodingExercise.Repositories;

namespace CodingExercise.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(CodingExerciseContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public User GetUser(int userId)
        {
            return _userRepository.GetUser(userId);
        }
    }
}
