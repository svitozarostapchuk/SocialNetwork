using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using BLL.Interfaces;
using AutoMapper;
using BLL.DTOs;
using Microsoft.AspNetCore.Authorization;
using DAL;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {
        private UserManager<User> _userManager;
        private IMapper _mapper;
        private readonly ApplicationSettings _appSettings;

        public AccountController(UserManager<User> userManager,
            IOptions<ApplicationSettings> appSettings, IMapper mapper)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult <UserModel>> Register([FromBody] UserModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserProfile = new UserProfile()
                {
                    City = model.City,
                    School = model.School,
                    University = model.University,
                    AboutUser = model.AboutUser,
                    Age = model.Age
                },
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, model.Role);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] UserModel model) 
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions(); 

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Incorrect username or password." });
        }

        [HttpPatch]
        [Authorize]
        [Route("update")]
        public async Task<ActionResult<UserModel>> UpdateUserAsync(UserModel model)
        {
            int currentId = GetCurrentUserId();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == currentId);
            await _userManager.UpdateSecurityStampAsync(user);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserProfile.City = model.City;
            user.UserProfile.School = model.School;
            user.UserProfile.University = model.University;
            user.UserProfile.AboutUser = model.AboutUser;
            user.UserProfile.Age = model.Age;

            var result = await _userManager.UpdateAsync(user);
            return Ok(result);
        }
    }
}
