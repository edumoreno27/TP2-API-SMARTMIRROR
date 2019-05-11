using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class NewsInformation
    {
        public Guid Id { get; set; }
        
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
