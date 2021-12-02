using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFriendshipService
    {
        public  Task<IEnumerable<FriendshipDTO>> GetAllFriendshipsAsync();
        public Task<IEnumerable<UserDTO>> GetFriendsAsync(int userProfileId);
        public Task AddFriendshipAsync (int userProfileId, int friendProfileId);
    }
}
