using System.Collections.Generic;

namespace Ek_spedycja.Model {
    class Salary {
        int Id { get; set; }
        Driver Driver { get; set; }
        int Month { get; set; }
        int Year { get; set; }
        decimal Amount { get; set; }

        public Salary(Driver driver, int month, int year, decimal amount) {
            Driver = driver;
            Month = month;
            Year = year;
            Amount = amount;
        }
    }
}
