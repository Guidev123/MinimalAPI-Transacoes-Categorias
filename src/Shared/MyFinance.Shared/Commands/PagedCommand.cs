using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Shared.Requests
{
    public abstract class PagedCommand : Command
    {
        public int PageSize { get; set; } = Config.DefaultPageSize;
        public int PageNumber { get; set; } = Config.DefaultPageNumer;
    }
}
