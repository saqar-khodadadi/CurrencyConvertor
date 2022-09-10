using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domains
{
    public class Currency:BaseEntity
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public double Rate { get; set; }

    }
}
