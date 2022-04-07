using System;

namespace Ek_spedycja.Model {
    public class Driver {
        private string _name, _surname, _pesel;
        private DateTime _birthDate, _hireDate;

        public const string TABLE_NAME = "driver";

        public int Id { get; set; }
        public string Name {
            get { return _name; }
            set {
                if (value.Length < 3)
                    throw new ArgumentException("Imię musi się składać z co najmniej 3 znaków");
                else
                    _name = char.ToUpper(value[0]) + value.Substring(1);
            }
        }
        public string Surname {
            get { return _surname; }
            set {
                if (value.Length < 3)
                    throw new ArgumentException("Nazwisko musi się składać z co najmniej 3 znaków");
                else
                    _surname = char.ToUpper(value[0]) + value.Substring(1);
            }
        }
        public string Pesel {
            get { return _pesel; }
            set {
                if (value.Length < 11)
                    throw new ArgumentException("Numer PESEL musi się składać z dokładnie 11 cyfr.");
                else
                    _pesel = value;
            }
        }
        public DateTime BirthDate {
            get { return _birthDate; }
            set {
                if (value.AddYears(18) > DateTime.Now)
                    throw new ArgumentException("Kierowca musi mieć ukończone 18 lat życia.");
                else
                    _birthDate = value;
            }
        }
        public DateTime HireDate {
            get { return _hireDate; }
            set {
                if (value > DateTime.Now)
                    throw new ArgumentException("Data zatrudnienia kierowcy nie może być późniejsza od daty dzisiejszej.");
                else
                    _hireDate = value;
            }
        }

        //ADD DRIVER
        public Driver(string name, string surname, string pesel, DateTime birthDate, DateTime hireDate) {
            Name = name;
            Surname = surname;
            Pesel = pesel;
            BirthDate = birthDate;
            HireDate = hireDate;
        }

        //EDIT DRIVER
        public Driver(int id, string name, string surname, string pesel, DateTime birthDate, DateTime hireDate) {
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

        public override string ToString() {
            return $"{Name} {Surname} ({Pesel})";
        }
    }
}
