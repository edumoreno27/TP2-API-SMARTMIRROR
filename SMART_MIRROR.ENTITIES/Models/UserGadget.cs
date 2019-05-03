using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class UserGadget
    {
        public Guid Id { get; set; }

        public Guid GadgetId { get; set; }
        public string UserId { get; set; }
        
        public int Order { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; }
        public Gadget Gadget { get; set; }
    }
}
