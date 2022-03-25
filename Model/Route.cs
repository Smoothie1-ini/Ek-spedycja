using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Route {
        int Id { get; set; }
        Driver Driver { get; set; }
        Vehicle Vehicle { get; set; }
        DateTime DepartureDate { get; set; }
        DateTime PlannedArrivalDate { get; set; }
        DateTime ActualArrivalDate { get; set; }
        decimal Length { get; set; }
        decimal Compensation { get; set; }
        List<Cost> Costs { get; set; }

        static List<Route> routes { get; set; }

        public Route(Driver driver, Vehicle vehicle, DateTime departureDate, DateTime plannedArrivalDate, DateTime actualArrivalDate, decimal length, decimal compensation, List<Cost> costs) {
            Driver = driver;
            Vehicle = vehicle;
            DepartureDate = departureDate;
            PlannedArrivalDate = plannedArrivalDate;
            ActualArrivalDate = actualArrivalDate;
            Length = length;
            Compensation = compensation;
            Costs = costs;
        }
    }
}
