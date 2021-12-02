using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FriendshipService : BaseService, IFriendshipService
    {
        private readonly IUserService _userService;

        public FriendshipService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService) : base(unitOfWork, mapper)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<UserDTO>> GetFriendsAsync(int userProfileId)
        {
            var friends = new List<UserDTO>();
            var friendships = await GetAllFriendshipsAsync();
            var selectedFriendships = friendships.Where(t => t.UserProfileId == userProfileId);
            foreach (var item in selectedFriendships)
            {
                friends.Add(await _userService.GetByIdAsync(item.FriendProfileId));
            }
            return friends;
        }

        public async Task AddFriendshipAsync (int userProfileId, int friendProfileId)
        {
            var friendship = new Friendship()
            {
                FriendProfileId = friendProfileId,
                UserProfileId = userProfileId
            };
            await UnitOfWork.FriendshipRepository.AddAsync(friendship);
            await UnitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<FriendshipDTO>> GetAllFriendshipsAsync()
        {
            var friendships = await Task.Run(() => UnitOfWork.FriendshipRepository.FindAll());
            var friendshipsDTO = Mapper.Map<IEnumerable<Friendship>, IEnumerable<FriendshipDTO>>(friendships);
            return friendshipsDTO;
        }
    }
}

