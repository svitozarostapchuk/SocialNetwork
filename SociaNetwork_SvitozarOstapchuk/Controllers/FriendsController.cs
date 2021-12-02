using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/friends")]
    public class FriendsController : BaseController
    {
        private readonly IFriendshipService _friendshipService;

        public FriendsController(IFriendshipService friendshipService, IUserService userService, IMapper mapper, UserManager<User> userManager)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet]
        [Authorize]
        [Route("user")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetFriendsByUserIdAsync()
        {
            var userProfileId = GetCurrentUserId();
            var friends = await _friendshipService.GetFriendsAsync(userProfileId);
            return Ok(friends);
        }
        
        [HttpPost]
        [Authorize]
        [Route("addfriend")]
        public async Task<ActionResult> AddFriendAsync([FromBody] UserModel friend)
        {
            var userProfileId = GetCurrentUserId();
            await _friendshipService.AddFriendshipAsync(userProfileId, friend.Id);
            return Ok();
        }
    }
}

