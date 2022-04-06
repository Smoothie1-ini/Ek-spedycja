using System;
using System.Collections.Generic;

namespace Ek_spedycja.Model {
    public class Route {
        public int Id { get; set; }
        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime PlannedArrivalDate { get; set; }
        public DateTime ActualArrivalDate { get; set; }
        public decimal Length { get; set; }
        private double _bid;
        public double Bid { get { return _bid; } set { _bid = value; } }

        public const string TABLE_NAME = "route";

        private const double const_bid = 23;
        private const double const_penalty = 50;

        List<Cost> Costs { get; set; }
        static List<Route> routes { get; set; }

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
            return (const_bid * hours_drived) + ((double)Length * length_bid * work_experience_bid) - penalty_hours * const_penalty;
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
