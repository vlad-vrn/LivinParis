namespace Graph;

public class dijkstra <T>
{
    public dijkstra(Graphe<T> graphe)
    {
        _graphe = graphe;
    }
    private readonly Graphe<T> _graphe;

    public Dictionary<int, int> TrouverChemins(int startId)
    {
        if (!_graphe.Noeuds.ContainsKey(startId))
        {
            Console.WriteLine("Le noeud spécifié n'existe pas.");
            return new Dictionary<int, int>();
        }

        var distances = new Dictionary<int, int>();
        var precedent = new Dictionary<int, int>();
        var priorityQueue = new SortedSet<(int distance, int nodeId)>();

        // Initialisation
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

            foreach (var lien in _graphe.Noeuds[currentNode].Liens)
            {
                var voisin = lien.NoeudDepart == _graphe.Noeuds[currentNode] ? lien.NoeudArrive : lien.NoeudDepart;
                int voisinId = _graphe.Noeuds.First(x => x.Value == voisin).Key;

                int newDistance = currentDistance + lien.Poids;

                if (newDistance < distances[voisinId])
                {
                    priorityQueue.Remove((distances[voisinId], voisinId)); // Supprime l'ancien
                    distances[voisinId] = newDistance;
                    precedent[voisinId] = currentNode;
                    priorityQueue.Add((newDistance, voisinId));
                }
            }
        }

        return distances;
    }
}