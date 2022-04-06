namespace Ek_spedycja.Model {
    class Cost {
        public int Id { get; set; }
        public Route Route { get; set; }
        public string CostType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public const string TABLE_NAME = "cost";

        //ADD
        public Cost(Route route, string costType, string description, decimal amount) {
            Route = route;
            CostType = costType;
            Description = description;
            Amount = amount;
        }

        //EDIT
        public Cost(int id, Route route, string costType, string description, decimal amount) {
            Id = id;
            Route = route;
            CostType = costType;
            Description = description;
            Amount = amount;
        }

        //DELETE
        public Cost(int id) {
            Id = id;
        }
    }
}
