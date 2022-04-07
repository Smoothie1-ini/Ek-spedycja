namespace Ek_spedycja.Model {
    class Cost {
        public const string TABLE_NAME = "cost";

        public int Id { get; set; }
        public Route Route { get; set; }
        public int CostType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        //ADD
        public Cost(Route route, int costType, string description, decimal amount) {
            Route = route;
            CostType = costType;
            Description = description;
            Amount = amount;
        }

        //EDIT
        public Cost(int id, Route route, int costType, string description, decimal amount) {
            Id = id;
            Route = route;
            CostType = costType;
            Description = description;
            Amount = amount;
        }

        //DELETE
        public Cost(int id, Route route) {
            Id = id;
            Route = route;
        }

        //GETDATA
        public Cost(Route route) {
            Route = route;
        }
    }
}
