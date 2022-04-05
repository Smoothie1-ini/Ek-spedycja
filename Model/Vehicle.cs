using System;
using System.Collections.Generic;

namespace Ek_spedycja.Model {
    class Vehicle {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Number { get; set; }
        public DateTime ServiceDate { get; set; }
        public bool IsAvailable { get; set; }

        public const string TABLE_NAME = "vehicle";

        static List<Vehicle> Vehicles { get; set; }

        // ADD VEHICLE
        public Vehicle(string brand, string model, string number, DateTime serviceDate, bool isAvailable) {
            Brand = brand;
            Model = model;
            Number = number;
            ServiceDate = serviceDate;
            IsAvailable = isAvailable;
        }

        // EDIT VEHICLE
        public Vehicle(int id, string brand, string model, string number, DateTime serviceDate, bool isAvailable) {
            Id = id;
            Brand = brand;
            Model = model;
            Number = number;
            ServiceDate = serviceDate;
            IsAvailable = isAvailable;
        }
        // DELETE VEHICLE
        public Vehicle(int id) {
            Id = id;
        }

        public override string ToString() {
            return $"{Brand} {Model} ({Number})";
        }
    }
}
