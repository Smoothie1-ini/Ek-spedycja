using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Driver {
        int Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Pesel { get; set; }
        DateTime BirthDate { get; set; }
        DateTime HireDate { get; set; }

        public static List<Driver> Drivers { get; set; }

        public Driver(string name, string surname, string pesel, DateTime birthDate, DateTime hireDate) {
            Name = name;
            Surname = surname;
            Pesel = pesel;
            BirthDate = birthDate;
            HireDate = hireDate;
        }
    }
}
