namespace MSI2.Graphs
{
    public class EdgeDetails
    {
        public int ToId { get; set; }
        public int Distance { get; set; }
        public double Pheromone { get; set; }

        public EdgeDetails(int toId, int distance, double pheromone)
        {
            ToId = toId;
            Distance = distance;
            Pheromone = pheromone;
        }

        public override string ToString() =>
            $"to:{ToId}, d:{Distance}, p:{Pheromone:0.##}";
    }
}
