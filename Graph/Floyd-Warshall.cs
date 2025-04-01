namespace Graph;

public class FloydWarshall <T>
{

    public FloydWarshall(Graphe<T> graphe)
    {
        _graphe = graphe;
    }
    private readonly Graphe<T> _graphe;
    public Dictionary<(int, int), int> TrouverChemins(int startId)
    {
        var indexMap = new Dictionary<int, int>();
        var ids = new List<int>(_graphe.Noeuds.Keys);
        int nbNoeuds = _graphe.Noeuds.Count;
        var distances = new Dictionary<(int, int), int>();
        foreach (var i in _graphe.Noeuds.Keys)
        {
            foreach (var j in _graphe.Noeuds.Keys)
            {
                if (i == j)
                    distances[(i, j)] = 0; // Distance d'un nœud à lui-même
                else
                    distances[(i, j)] = int.MaxValue; // Distance infinie par défaut
            }
        }
        foreach (var noeud in _graphe.Noeuds)
        {
            foreach (var lien in noeud.Value.Liens)
            {
                int u = _graphe.Noeuds.First(x => x.Value == lien.NoeudDepart).Key;
                int v = _graphe.Noeuds.First(x => x.Value == lien.NoeudArrive).Key;

                distances[(u, v)] = lien.Poids;
                distances[(v, u)] = lien.Poids; 
            }
        }
        foreach (var k in _graphe.Noeuds.Keys)
        {
            foreach (var i in _graphe.Noeuds.Keys)
            {
                foreach (var j in _graphe.Noeuds.Keys)
                {
                    if (distances[(i, k)] != int.MaxValue && distances[(k, j)] != int.MaxValue)
                    {
                        int newDist = distances[(i, k)] + distances[(k, j)];
                        if (newDist < distances[(i, j)])
                        {
                            distances[(i, j)] = newDist;
                        }
                    }
                }
            }
        }

        return distances;
    }
}