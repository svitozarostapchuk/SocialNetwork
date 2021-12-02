using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MessageService : BaseService, IMessageService
    {
        public MessageService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<MessageDTO>> GetAllAsync()
        {
            var result = await Task.Run(() => UnitOfWork.MessageRepository.FindAll());
            var resultDTOs = Mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(result);
            return resultDTOs;
        }
        public async Task<MessageDTO> GetByIdAsync(int id)
        {
            var result = await UnitOfWork.MessageRepository.FindByIdAsync(id);
            var resultDTO = Mapper.Map<Message, MessageDTO>(result);
            return resultDTO;
        }
        public async Task AddAsync(MessageDTO entity) 
        {
            var messageMapped = Mapper.Map<MessageDTO, Message>(entity);
            await UnitOfWork.MessageRepository.AddAsync(messageMapped);
            await UnitOfWork.SaveAsync();

        }
        public Task DeleteByIdAsync(int entityId)
        {
            UnitOfWork.MessageRepository.DeleteByIdAsync(entityId);
            return UnitOfWork.SaveAsync();
        }
    }
}

