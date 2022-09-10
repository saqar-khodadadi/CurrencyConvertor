using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands.AddConfigurations
{
    public class AddConfigurationsCommand:IRequest
    {
        public AddConfigurationsCommand(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            this.conversionRates = conversionRates;
        }

        public IEnumerable<Tuple<string, string, double>> conversionRates { get; set; }
    }
}
