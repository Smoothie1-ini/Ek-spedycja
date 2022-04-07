using System;

namespace Ek_spedycja.Model {
    public class Route {
        private Vehicle _vehicle;
        private double _bid;

        public const string TABLE_NAME = "route";
        private const double BASE_BID = 23;
        private const double BASE_PENALTY = 50;

        public int Id { get; set; }
        public Driver Driver { get; set; }
        public Vehicle Vehicle {
            get { return _vehicle; }
            set {
                if (value.IsAvailable == false)
                    throw new ArgumentException("Wskazany pojazd nie jest w tej chwili dostępny.");
                else
                    _vehicle = value;
            }
        }
        public DateTime DepartureDate { get; set; }
        public DateTime PlannedArrivalDate { get; set; }
        public DateTime ActualArrivalDate { get; set; }
        public decimal Length { get; set; }
        public double Bid { get { return _bid; } set { _bid = value; } }

        //ADD ROUTE
        public Route(Driver driver, Vehicle vehicle, DateTime departureDate, DateTime plannedArrivalDate, DateTime actualArrivalDate, decimal length) {
            Driver = driver;
            Vehicle = vehicle;
            DepartureDate = departureDate;
            PlannedArrivalDate = plannedArrivalDate;
            ActualArrivalDate = actualArrivalDate;
            Length = length;
            Bid = CalculateBid();
        }

        //EDIT ROUTE
        public Route(int id, Driver driver, Vehicle vehicle, DateTime departureDate, DateTime plannedArrivalDate, DateTime actualArrivalDate, decimal length) {
            Id = id;
            Driver = driver;
            Vehicle = vehicle;
            DepartureDate = departureDate;
            PlannedArrivalDate = plannedArrivalDate;
            ActualArrivalDate = actualArrivalDate;
            Length = length;
            Bid = CalculateBid();
        }

        //DELETE ROUTE
        public Route(int id) {
            Id = id;
        }

        private double CalculateBid() {
            double length_bid = GetLengthRate();
            double work_experience_bid = GetWorkExperienceRate();
            double hours_drived = GetDrivedHours();
            double penalty_hours = GetPenaltyHours();
            return (BASE_BID * hours_drived) + ((double)Length * length_bid * work_experience_bid) - penalty_hours * BASE_PENALTY;
        }

        private double GetLengthRate() {
            if (Length < 400) {
                return 0.6;
            } else if (Length < 700) {
                return 0.8;
            } else if (Length < 1300) {
                return 0.9;
            } else {
                return 1;
            }
        }

        private double GetWorkExperienceRate() {
            double YearsExperience = DateTime.Today.Subtract(Driver.HireDate).TotalDays;

            if (YearsExperience <= 2) {
                return 0.8;
            } else if (YearsExperience <= 5) {
                return 0.9;
            } else if (YearsExperience <= 10) {
                return 1;
            } else {
                return 1.1;
            }
        }

        private double GetDrivedHours() {
            return Math.Abs(ActualArrivalDate.Subtract(DepartureDate).TotalHours);
        }

        private double GetPenaltyHours() {
            double penalty_hours = ActualArrivalDate.Subtract(PlannedArrivalDate).TotalHours;
            if (penalty_hours < 0) {
                return 0;
            } else {
                return Math.Abs(penalty_hours);
            }
        }
    }
}
