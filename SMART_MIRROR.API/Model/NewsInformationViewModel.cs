using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class NewsInformationViewModel
    {
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public int MirrorId { get; set; }
        public int Index { get; set; }
    }
}
