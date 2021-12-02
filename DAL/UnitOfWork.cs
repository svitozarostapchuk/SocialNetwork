using DAL.Contexts;
using DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly SocialNetworkDbContext _context;

        public IUserRepository UserRepository { get; }
        public IMessageRepository MessageRepository { get; }
        public IFriendshipRepository FriendshipRepository { get; }

        public UnitOfWork(SocialNetworkDbContext context, IUserRepository userRepository, 
            IMessageRepository messageRepository, IFriendshipRepository friendshipRepository)
        {
            _context = context;
            UserRepository = userRepository;
            MessageRepository = messageRepository;
            FriendshipRepository = friendshipRepository;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}