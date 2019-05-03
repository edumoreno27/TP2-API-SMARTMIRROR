using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class EmailInformationViewModel
    {       
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SenderAt { get; set; }        
        public int Index { get; set; }        
    }
    
}
