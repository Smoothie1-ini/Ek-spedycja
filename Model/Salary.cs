using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Salary {
        int Id { get; set; }
        Driver Driver { get; set; }
        int Month { get; set; }
        int Year { get; set; }
        decimal Amount { get; set; }

        static List<Salary> salaries { get; set; }

        public Salary(Driver driver, int month, int year, decimal amount) {
            Driver = driver;
            Month = month;
            Year = year;
            Amount = amount;
        }
    }
}
