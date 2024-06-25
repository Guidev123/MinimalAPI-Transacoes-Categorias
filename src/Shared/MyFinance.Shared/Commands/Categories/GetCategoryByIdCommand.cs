using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Shared.Requests.Categories
{
    public class GetCategoryByIdCommand : Command
    {
        public long Id { get; set; }
    }
}
