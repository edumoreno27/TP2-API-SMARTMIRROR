using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email  { get; set; }
        public int IdReference { get; set; } 
        public string Accesstoken { get; set; }
        public string Refreshtoken { get; set; }
        public int RoomNumber { get; set; }
        public int MirrorId { get; set; }

    }
}
