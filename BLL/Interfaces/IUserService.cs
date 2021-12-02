using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IService<UserDTO>
    {
        public Task<IEnumerable<UserDTO>> GetFilteredUsersAsync(UserSearchFilterDTO filter);
    }
}
