using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }
        IFriendshipRepository FriendshipRepository { get; }

        public Task<int> SaveAsync();
    }
}
