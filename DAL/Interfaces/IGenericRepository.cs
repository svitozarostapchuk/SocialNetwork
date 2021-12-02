using DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> FindAll();
        Task<TEntity> FindByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(int id);

    }
}
