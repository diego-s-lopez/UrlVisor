using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlVisor.Model
{
    public class ResponseBase
    {
        public bool LoginOk { get; set; }
        public string LoginError { get; set; }
    }
}
