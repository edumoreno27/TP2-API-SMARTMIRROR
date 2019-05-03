using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class BooleanTable
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public bool Order { get; set; }
        public bool Diary { get; set; }
        public bool Email { get; set; }
        public bool StartEmail{ get; set; }
        public string UserId { get; set; }
        public bool Sesion { get; set; }
        public bool HotelService { get; set; }
        public bool Music { get; set; } = false;
        public User User { get; set; }
    }
}
