using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class EmailInformation
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SenderAt { get; set; }
        public string UserId { get; set; }        

        public int Index { get; set; }
        public User User { get; set; }
    }
}
