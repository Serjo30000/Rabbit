using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11
{
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Body { get; set; }
        public string Mail { get; set; }
        public int Port { get; set; }
    }
}
