using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class MusicAction
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
