namespace Graph;

public class DijkstraAlgorithm<T>
{
    private readonly Graphe<T> _graphe;

    public DijkstraAlgorithm(Graphe<T> graphe)
    {
        _graphe = graphe;
    }

    public List<int> TrouverChemin(int startId, int endId)
    {
        if (!_graphe.Noeuds.ContainsKey(startId) || !_graphe.Noeuds.ContainsKey(endId))
        {
            Console.WriteLine("Un des noeuds spécifiés n'existe pas.");
            return new List<int>();
        }

        var distances = new Dictionary<int, int>();
        var precedent = new Dictionary<int, int>();
        var priorityQueue = new SortedSet<(int distance, int nodeId)>();

        foreach (var node in _graphe.Noeuds.Keys)
        {
            distances[node] = int.MaxValue;
            precedent[node] = -1;
        }
        distances[startId] = 0;
        priorityQueue.Add((0, startId));

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentNode) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (currentNode == endId)
                break;

            foreach (var lien in _graphe.Noeuds[currentNode].Liens)
            {
                var voisin = lien.NoeudDepart.Equals(_graphe.Noeuds[currentNode]) ? lien.NoeudArrive : lien.NoeudDepart;
                int voisinId = _graphe.Noeuds.First(x => x.Value == voisin).Key;

                int newDistance = currentDistance + lien.Poids;

                if (newDistance < distances[voisinId])
                {
                    priorityQueue.Remove((distances[voisinId], voisinId));
                    distances[voisinId] = newDistance;
                    precedent[voisinId] = currentNode;
                    priorityQueue.Add((newDistance, voisinId));
                }
            }
        }

        return ReconstruireChemin(precedent, startId, endId);
    }

    private List<int> ReconstruireChemin(Dictionary<int, int> precedent, int startId, int endId)
    {
        var chemin = new List<int>();
        int current = endId;

        while (current != -1)
        {
            chemin.Add(current);
            current = precedent[current];
        }

        chemin.Reverse();

        if (chemin[0] != startId)
        {
            Console.WriteLine("Aucun chemin trouvé.");
            return new List<int>();
        }

        return chemin;
    }
}
