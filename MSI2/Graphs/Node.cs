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

        public override bool Equals(object obj) =>
            obj is Node node2 && node2.id == id;
    }
}
