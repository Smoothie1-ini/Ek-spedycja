using System;
using System.Collections.Generic;

namespace Ek_spedycja.Model {
    public class Vehicle {
        private DateTime _serviceDate;

        public const string TABLE_NAME = "vehicle";

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Number { get; set; }
        public DateTime ServiceDate {
            get { return _serviceDate; }
            set {
                if (value > DateTime.Now)
                    throw new ArgumentException("Wprowadzona data serwisowania pojazdu jest niepoprawna.");
                else
                    _serviceDate = value;
            }
        }
        public bool IsAvailable { get; set; }

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
