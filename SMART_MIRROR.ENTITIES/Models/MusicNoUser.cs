using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class MusicNoUser
    {
        public Guid Id { get; set; }
        public string Action { get; set; }

        public int MirrorId { get; set; }        

        public bool MusicBool { get; set; }
        
    }
}
