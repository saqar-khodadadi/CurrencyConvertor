using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Commands.AddConfigurations
{
    internal class AddConfigurationsCommandHandler:IRequestHandler<AddConfigurationsCommand>
    {
        private readonly ICurrencyConverter _currencyConverter;
        public AddConfigurationsCommandHandler(ICurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
        }
        public async Task<Unit> Handle(AddConfigurationsCommand request, CancellationToken cancellationToken)
        {
            await _currencyConverter.UpdateConfiguration(request.conversionRates);
            return Unit.Value;
        }
    }
}
