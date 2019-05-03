using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Model
{
    public class ListEmailInformationViewModel
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public EmailInformationViewModel ObjectReference { get; set; }
    }
}
