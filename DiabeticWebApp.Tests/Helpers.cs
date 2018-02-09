using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticWebApp.Tests
{
    class Helpers
    {
        private static readonly Random random = new Random();
        public string RandomStringGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public int RandomNumberGenerator(int rangeFrom, int rangeTo)
        {
            return random.Next(rangeFrom, rangeTo);
        }

        public DateTime RandomDateGenerator(DateTime? startDate = null, DateTime? endDate = null)
        {
            var dateFrom = startDate ?? DateTime.MinValue;
            var dateTo = endDate ?? DateTime.MaxValue;
            var range = (dateTo - dateFrom).Days;
            return dateFrom.AddDays(random.Next(range));
        }
    }
}
