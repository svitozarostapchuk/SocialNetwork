using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public IQueryable<User> FindAll();
        Task<User> FindByIdAsync(int id);
        Task AddAsync(User entity);
        void DeleteById(int id);
    }
}

