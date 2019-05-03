using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class GadgetViewModel
    {
        public Guid GadgetId { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Selected { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
    }
}
