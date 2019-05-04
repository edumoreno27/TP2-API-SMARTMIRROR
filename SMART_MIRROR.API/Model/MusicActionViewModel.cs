using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class MusicActionViewModel
    {
        public string UserId { get; set; }
        public string Action { get; set; }
        public int MirrorId { get; set; }
    }
}
