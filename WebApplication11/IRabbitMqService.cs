using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11
{
    public interface IRabbitMqService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
        void SendEmail(object obj);
        void SendEmail(Email message);
    }
}
