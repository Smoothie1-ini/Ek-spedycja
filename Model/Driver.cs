using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Driver {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }

        public readonly string tableName = "driver";

        public static List<Driver> Drivers { get; set; }

        //ADD DRIVER
        public Driver(string name, string surname, string pesel, DateTime birthDate, DateTime hireDate) {
            Name = name;
            Surname = surname;
            Pesel = pesel;
            BirthDate = birthDate;
            HireDate = hireDate;
        }

        //EDIT DRIVER
        public Driver(int id, string name, string surname, string pesel, DateTime birthDate, DateTime hireDate) : this(id) {
            Id = id;
            Name = name;
            Surname = surname;
            Pesel = pesel;
            BirthDate = birthDate;
            HireDate = hireDate;
        }

        //DELETE DRIVER
        public Driver(int id) {
            Id = id;
        }
    }
}
