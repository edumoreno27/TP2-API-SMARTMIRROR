using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class ListHotelServiceInformationViewModel
    {
        public string UserId { get; set; }
        public HotelServiceViewModel ObjectReference { get; set; }
    }
}
