namespace MSI2.Graphs
{
    public class Node
    {
        public int id, demand;
        public bool visited;

        public Node(int id, int demand)
        {
            this.id = id;
            this.demand = demand;
            visited = false;
        }

        public override string ToString() =>
            $"id:{id}, d:{demand}, v:{(visited ? 1 : 0)}";

    }
}
