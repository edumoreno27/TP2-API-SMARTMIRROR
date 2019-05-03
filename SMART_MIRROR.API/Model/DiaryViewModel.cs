using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class DiaryViewModel
    {
        public List<int> List { get; set; }
        public string UserId { get; set; }
        public int Order { get; set; }
    }
}
