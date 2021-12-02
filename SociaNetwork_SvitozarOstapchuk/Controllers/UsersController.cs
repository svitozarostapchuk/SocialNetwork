using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsersAsync() 
        {
            var res = await _userService.GetAllAsync();

            if (res != null) 
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<ActionResult<UserModel>> GetCurrentUserByIdAsync() 
        {
            var userId = GetCurrentUserId();
            var res = await _userService.GetByIdAsync(userId);
            var mappedRes = _mapper.Map<UserDTO, UserModel>(res);
            if (mappedRes != null)
            {
                return Ok(mappedRes);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserByIdAsync(int id)
        {
            var res = await _userService.GetByIdAsync(id);
            var mappedRes = _mapper.Map<UserDTO, UserModel>(res);

            if (mappedRes != null)
            {
                return Ok(mappedRes);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteUserByIdAsync(int id) 
        {
            await _userService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPost("filtered")] 
        [Authorize]
        public async Task<ActionResult<UserModel>> GetFilteredUsersAsync(UserSearchFilterModel filter)
        {
            var dtoFilter = _mapper.Map<UserSearchFilterModel, UserSearchFilterDTO>(filter);
            var res = await _userService.GetFilteredUsersAsync(dtoFilter);

            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }
    }
}
