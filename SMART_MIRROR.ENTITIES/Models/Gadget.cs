using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class Gadget
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int StaticNumber { get; set; }
        public string ContentHtml { get; set; }

    }
}
