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
        List<Cost> Costs { get; set; }
        DateTime Leave { get; set; }
        DateTime PlannedArrival { get; set; }
        DateTime ActualArrival { get; set; }
        decimal Length { get; set; }
        decimal Compensation { get; set; }

        static List<Route> routes { get; set; }

        public Route(Driver driver, Vehicle vehicle, List<Cost> costs, DateTime leave, DateTime plannedArrival, DateTime actualArrival, decimal length) {
            Driver = driver;
            Vehicle = vehicle;
            Costs = costs;
            Leave = leave;
            PlannedArrival = plannedArrival;
            ActualArrival = actualArrival;
            Length = length;
        }
    }
}
