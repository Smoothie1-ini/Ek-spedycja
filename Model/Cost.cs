using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Cost {
        int Id { get; set; }
        string CostType { get; set; }
        string Description { get; set; }
        decimal Amount { get; set; }

        public Cost(string costType, string description, decimal amount) {
            CostType = costType;
            Description = description;
            Amount = amount;
        }
    }
}
