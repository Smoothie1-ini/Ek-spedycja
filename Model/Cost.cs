using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ek_spedycja.Model {
    class Cost {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Value { get; set; }

        public Cost(string name, string description, decimal value) {
            Name = name;
            Description = description;
            Value = value;
        }
    }
}
