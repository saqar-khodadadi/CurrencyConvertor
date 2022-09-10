using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Commands.ClearCurrencies
{
    public class ClearCurrenciesCommandHandler: IRequestHandler<ClearCurrenciesCommand>
    {
        private readonly ICurrencyConverter _currencyConverter;

        public ClearCurrenciesCommandHandler(ICurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
        }

        public async Task<Unit> Handle(ClearCurrenciesCommand request, CancellationToken cancellationToken)
        {
          await _currencyConverter.ClearConfiguration();
          return Unit.Value;
        }
    }
}
