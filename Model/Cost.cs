using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Cost {
        int Id { get; set; }
        public string CostType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public readonly string tableName = "cost";

        public Cost(string costType, string description, decimal amount) {
            CostType = costType;
            Description = description;
            Amount = amount;
        }
    }
}
