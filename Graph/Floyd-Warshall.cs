namespace Graph;

public class Floyd_Warshall <T>
{
    
    private readonly Graphe<T> _graphe;

    public Floyd_Warshall(Graphe<T> graphe)
    {
        _graphe = graphe;
    }

    public int[,] TrouverChemins()
    {
        int nbNoeuds = _graphe.Noeuds.Count;
        var indexMap = new Dictionary<int, int>(); // Map ID -> Index
        var ids = new List<int>(_graphe.Noeuds.Keys);
        
        for (int i = 0; i < ids.Count; i++)
            indexMap[ids[i]] = i;

        int[,] distances = new int[nbNoeuds, nbNoeuds];

        // Initialisation
        for (int i = 0; i < nbNoeuds; i++)
        {
            for (int j = 0; j < nbNoeuds; j++)
            {
                if (i == j)
                    distances[i, j] = 0;
                else
                    distances[i, j] = int.MaxValue / 2; // Evite les débordements
            }
        }

        // Remplissage avec les liens existants
        foreach (var noeud in _graphe.Noeuds.Values)
        {
            foreach (var lien in noeud.Liens)
            {
                int u = indexMap[_graphe.Noeuds.First(x => x.Value == lien.NoeudDepart).Key];
                int v = indexMap[_graphe.Noeuds.First(x => x.Value == lien.NoeudArrive).Key];
                distances[u, v] = lien.Poids;
            }
        }

        // Algorithme de Floyd-Warshall
        for (int k = 0; k < nbNoeuds; k++)
        {
            for (int i = 0; i < nbNoeuds; i++)
            {
                for (int j = 0; j < nbNoeuds; j++)
                {
                    if (distances[i, j] > distances[i, k] + distances[k, j])
                    {
                        distances[i, j] = distances[i, k] + distances[k, j];
                    }
                }
            }
        }

        return distances;
    }
}