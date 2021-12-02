using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ChatHub : Hub 
    {
        public Task SendMessage(string user1, string user2, string message)            
        {
            return Clients.All.SendAsync("ReceiveOne", user1, user2, message);  
        }
    }
}
