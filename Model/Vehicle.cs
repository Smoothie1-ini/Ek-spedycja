using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Vehicle {
        int Id { get; set; }
        string Brand { get; set; }
        string Model { get; set; }
        string Number { get; set; }
        DateTime ServiceDate { get; set; }
        bool IsAvailable { get; set; }

        static List<Vehicle> Vehicles { get; set; }

        public Vehicle(string brand, string model, string number, DateTime serviceDate, bool isAvailable) {
            Brand = brand;
            Model = model;
            Number = number;
            ServiceDate = serviceDate;
            IsAvailable = isAvailable;
        }
    }
}
