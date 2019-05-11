using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class NewsInformationNoUser
    {
        public Guid Id { get; set; }

        public string Tittle { get; set; }
        public int MirrorId { get; set; }
        public string Description { get; set; }        
        public bool StartNews { get; set; }
        public bool News { get; set; }
    }
}
