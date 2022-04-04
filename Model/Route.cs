using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Route {
        int Id { get; set; }
        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime PlannedArrivalDate { get; set; }
        public DateTime ActualArrivalDate { get; set; }
        public decimal Length { get; set; }

        public double Bid { get; set; }

        private const double const_bid = 23;
        private const double const_penalty = 50;
        public readonly string tableName = "route";

        List<Cost> Costs { get; set; }
        static List<Route> routes { get; set; }

        public Route(Driver driver, Vehicle vehicle, DateTime departureDate, DateTime plannedArrivalDate, DateTime actualArrivalDate, decimal length, double bid, List<Cost> costs) {
            Driver = driver;
            Vehicle = vehicle;
            DepartureDate = departureDate;
            PlannedArrivalDate = plannedArrivalDate;
            ActualArrivalDate = actualArrivalDate;
            Length = length;
            Bid = bid;
            Costs = costs;
        }

        public Route(Driver driver, Vehicle vehicle, DateTime departureDate, DateTime plannedArrivalDate, DateTime actualArrivalDate, decimal length) {
            Driver = driver;
            Vehicle = vehicle;
            DepartureDate = departureDate;
            PlannedArrivalDate = plannedArrivalDate;
            ActualArrivalDate = actualArrivalDate;
            Length = length;
            Bid = Math.Round(CountBid(),2);
        }

        private double CountBid() {
            double length_bid = ReturnLengthBid();
            double work_experience_bid = ReturnWorkExperienceBid();
            double hours_drived = ReturnHoursDrived();
            double penalty_hours = ReturnHoursPenaltyHours();
            return (const_bid * hours_drived) + ((double)Length * length_bid * work_experience_bid) - penalty_hours * const_penalty;
        }


        private double ReturnLengthBid()
        {
            if (Length < 400)
            {
                return 0.6;
            }
            else if (Length < 700)
            {
                return 0.8;
            }
            else if (Length < 1300)
            {
                return 0.9;
            }
            else
            {
                return 1;
            }
        }

        private double ReturnWorkExperienceBid()
        {
            double YearsExperience = DateTime.Today.Subtract(Driver.HireDate).TotalDays;

            if (YearsExperience <= 2)
            {
                return 0.8;
            }
            else if (YearsExperience <= 5)
            {
                return 0.9;
            }
            else if (YearsExperience <= 10)
            {
                return 1;
            }
            else
            {
                return 1.1;
            }
        }

        private double ReturnHoursDrived()
        {
            return Math.Abs(ActualArrivalDate.Subtract(DepartureDate).TotalHours);
        }

        private double ReturnHoursPenaltyHours() {
            double penalty_hours = ActualArrivalDate.Subtract(PlannedArrivalDate).TotalHours;
            if (penalty_hours < 0)
            {
                return 0;
            }
            else
            {
                return Math.Abs(penalty_hours);
            }
        }
    }
}
