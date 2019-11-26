using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Usavc.WebMVC
{
    public class AppSettings
    {
        public string PurchaseUrl { get; set; }
        public bool UseCustomizationData { get; set; }
    }

    public class Connectionstrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }
}
