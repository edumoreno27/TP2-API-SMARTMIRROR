using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_MIRROR.ENTITIES.Models
{
    public class DiaryGoogle
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int Index { get; set; }
        public bool IsSelected { get; set; }
        public bool Map { get; set; }
        public User User { get; set; }
    }
}
