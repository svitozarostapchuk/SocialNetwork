using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        { 
            var result = await Task.Run(() => UnitOfWork.UserRepository.FindAll());
            var resultDTOs = Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(result);
            return resultDTOs;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await UnitOfWork.UserRepository.FindByIdAsync(id);
            return Mapper.Map<User, UserDTO>(user);
        }

        public async Task AddAsync(UserDTO entity)
        { 
            var user = Mapper.Map<UserDTO, User>(entity);
            await UnitOfWork.UserRepository.AddAsync(user);
            await UnitOfWork.SaveAsync();
        }

        public Task DeleteByIdAsync(int entityId)
        {
            UnitOfWork.UserRepository.DeleteById(entityId);
            return UnitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetFilteredUsersAsync(UserSearchFilterDTO filter)
        {
            var filteredUsers = new List<UserDTO>();
            var users = await GetAllAsync();
            if (filter.City != null) {
                filteredUsers.AddRange(users.Where(x => x.City == filter.City).ToList());
            }
            if (filter.School != null) {
                filteredUsers.AddRange(users.Where(x => x.School == filter.School).ToList());
            }
            if (filter.University != null) {
                filteredUsers.AddRange(users.Where(x => x.University == filter.University).ToList());
            }
            if (filter.AgeMin != 0 && filter.AgeMax != 0) {
                filteredUsers.AddRange(users.Where(x => x.Age >= filter.AgeMin && x.Age <= filter.AgeMax).ToList());
            }
            return filteredUsers; 
        }
    }
}

