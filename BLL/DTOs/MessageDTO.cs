using System;

namespace BLL.DTOs
{
    public class MessageDTO : BaseDTO
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeSent { get; set; }
    }
}

