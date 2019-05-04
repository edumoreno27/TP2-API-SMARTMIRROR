using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class User : IdentityUser
    {        
        public string Accesstoken { get; set; }
        public string Refreshtoken { get; set; }
        public int Expires_in { get; set; }
        public string Token_type { get; set; }
        //public bool IsActive { get; set; } = false;
        public int MirrorId { get; set; }
        public int IdReference { get; set; }

        public int RoomNumber { get; set; }

    }
}
