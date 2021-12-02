using DAL.Contexts;
using DAL.EF.Exceptions;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly SocialNetworkDbContext _socialNetworkDbContext;

        public UserRepository(SocialNetworkDbContext context)
        {
            _socialNetworkDbContext = context;
        }

        public IQueryable<User> FindAll() 
        {
            var allUsers = _socialNetworkDbContext.Users;
            var castAllUsers = new List<User>();
            foreach (var element in allUsers)
            {
                var castElement = (User)element;
                castAllUsers. Add(castElement);
            }
            if (castAllUsers == null)
            {
                throw new NoRecordFoundException();
            }
            var withDetails = castAllUsers.AsQueryable().Include(user => user.UserProfile);
            return withDetails;
        }
        public async Task<User> FindByIdAsync(int id)
        {
            var users = await Task.Run(() => FindAll());
            var user = users.First(user => user.Id == id);
            if (user == null)
            {
                throw new NoRecordFoundException();
            }
            return user;
        }
        public User FindById(int id)
        {
            var user = _socialNetworkDbContext.Users.First(user => user.Id == id);
            if (user == null)
            {
                throw new NoRecordFoundException();
            }
            return (User)user;
        }
        public Task AddAsync(User entity)
        {
            if (entity == null)
            {
                throw new NullParameterException();
            }
            return _socialNetworkDbContext.Users.AddRangeAsync(entity);
        }

        public void DeleteById(int id) 
        {
            if (id < 0)
            {
                throw new NoRecordFoundException("Invalid id entered - should be not less than 0");
            }
            var entity = FindById(id);
            if (entity == null)
            {
                throw new NoRecordFoundException();
            }
            _socialNetworkDbContext.Users.Remove(entity);
        }
    }
}
