using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Shared
{
    public static class Config
    {
        public const int DefaultStatusCode = 200;
        public const int DefaultPageSize = 25;
        public const int DefaultPageNumer = 1;

        public static string ConnectionString { get; set; } = string.Empty;
        public static string BackendUrl { get; set; } = "http://localhost:44389";
        public static string FrontendUrl { get; set; } = "http://localhost:44300";

    }
}
