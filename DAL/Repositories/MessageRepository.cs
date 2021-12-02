using DAL.Contexts;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(SocialNetworkDbContext context) : base(context)
        {
        }
    }
}
