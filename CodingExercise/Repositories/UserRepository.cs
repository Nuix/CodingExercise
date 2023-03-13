using CodingExercise.Data;
using CodingExercise.Models;

namespace CodingExercise.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CodingExerciseContext _context;

        public UserRepository(CodingExerciseContext context)
        {
            _context = context;
        }

        public User GetUser(int userId)
        {
            return _context.Users.SingleOrDefault(user => user.Id == userId);
        }
    }
}
