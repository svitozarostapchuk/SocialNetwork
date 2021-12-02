using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatsController(IHubContext<ChatHub> hubContext, IMessageService messageService, IMapper mapper)
        {
            _hubContext = hubContext;
        }

        [Route("api/chat/send")]  
        [HttpPost]
        public async Task<IActionResult> SendRequestAsync([FromBody] MessageModel msg)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveOne", msg.User1, msg.User2, msg.MessageText);
            return Ok();
        }
    }
}
