using DAL.Entities;
using System;

namespace DAL
{
    public class Message : BaseEntity
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeSent { get; set; }
    }
}

