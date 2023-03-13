using CodingExercise.Models;

namespace CodingExercise.Services
{
    public interface IUserService
    {
        public User GetUser(int userId);
    }
}
