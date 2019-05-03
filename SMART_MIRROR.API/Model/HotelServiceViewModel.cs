using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class HotelServiceViewModel
    {
        public List<DataHotelServices> ListHotelServices { get; set; }
        public string UserId { get; set; }
        public int Order { get; set; }
    }

    public class DataHotelServices {               
        public int Index { get; set; }
        public int ServiceId { get; set; }        
    }
}
