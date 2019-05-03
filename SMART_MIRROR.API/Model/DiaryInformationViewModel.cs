using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class DiaryInformationViewModel
    {        
        public string Summary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }        
        public int Index { get; set; }        
    }
}
