using DAL.Contexts;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(SocialNetworkDbContext context) : base(context)
        {
        }
    }
}