using CodingExercise.Models;

namespace CodingExercise.Repositories
{
    public interface IUserRepository
    {
        public User GetUser(int userId);
    }
}
