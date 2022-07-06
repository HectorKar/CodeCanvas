using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCanvas.Database;
using CodeCanvas.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeCanvas.Services
{
    public class RatesService : IRatesService {

        private readonly ApplicationDbContext _db;

        public RatesService(ApplicationDbContext db) {
            _db = db;
        }

        public async Task<IList<CurrencyRateEntity>> GetRatesByDate (DateTime date) {
            var result = await _db.CurrencyRates.Where(x => x.CreatedAt.Date == date.Date).ToListAsync();
            return result;
        }

        public async Task<IList<CurrencyRateEntity>> GetRatesAfterSpecificDate(DateTime date) {
            IList<CurrencyRateEntity> result = new List<CurrencyRateEntity>();
            var dateWithValues = _db.CurrencyRates.FirstOrDefault(x => x.CreatedAt.Date > date.Date)?.CreatedAt.Date;
            
            if (dateWithValues != null) 
                result = await _db.CurrencyRates.Where(x => x.CreatedAt.Date == dateWithValues).ToListAsync();

            return result;
        }
    }
}
