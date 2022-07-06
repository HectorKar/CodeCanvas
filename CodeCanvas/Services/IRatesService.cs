using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCanvas.Entities;

namespace CodeCanvas.Services {
    public interface IRatesService {
        public Task<IList<CurrencyRateEntity>> GetRatesByDate (DateTime date);
        public Task<IList<CurrencyRateEntity>> GetRatesAfterSpecificDate (DateTime date);
    }
}