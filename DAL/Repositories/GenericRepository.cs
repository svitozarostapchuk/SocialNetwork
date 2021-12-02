using DAL.Contexts;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected SocialNetworkDbContext _socialNetworkDbContext;

        protected DbSet<T> DbSet { get; }

        protected GenericRepository(SocialNetworkDbContext context)
        {
            DbSet = context.Set<T>();
            _socialNetworkDbContext = context;
        }

        public IQueryable<T> FindAll() => DbSet;

        public async Task<T> FindByIdAsync(int id) => await DbSet.FirstOrDefaultAsync(entity => entity.Id == id);

        public async Task AddAsync(T entity) => await DbSet.AddRangeAsync(entity); 

        public void Delete(T entity) => DbSet.Remove(entity);

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            await Task.Run(() => Delete(entity));
        }

        public void Update(T entity) => _socialNetworkDbContext.Entry(entity).State = EntityState.Modified;
    }
}