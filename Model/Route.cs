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
        public decimal Bid { get; set; }

        public readonly string tableName = "route";

        List<Cost> Costs { get; set; }
        static List<Route> routes { get; set; }

        public Route(Driver driver, Vehicle vehicle, DateTime departureDate, DateTime plannedArrivalDate, DateTime actualArrivalDate, decimal length, decimal bid, List<Cost> costs) {
            Driver = driver;
            Vehicle = vehicle;
            DepartureDate = departureDate;
            PlannedArrivalDate = plannedArrivalDate;
            ActualArrivalDate = actualArrivalDate;
            Length = length;
            Bid = bid;
            Costs = costs;
        }
    }
}
