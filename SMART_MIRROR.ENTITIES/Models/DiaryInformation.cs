using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class DiaryInformation
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
        public string UserId { get; set; }        
        public User User { get; set; }
    }
}
