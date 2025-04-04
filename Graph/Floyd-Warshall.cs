namespace Graph;

public class Floyd_Warshall
{
    public class Station
    {
        public string Nom { get; set; }
    }

    public static (List<string> chemin, int tempsTotal) TrouverItineraire(
        List<string> nomsStations,
        List<Tuple<string, string, int>> connexions,
        string depart,
        string arrivee)
    {

        int n = nomsStations.Count;
        int[,] dist = new int[n, n];
        string[,] next = new string[n, n];

        var indices = new Dictionary<string, int>();
        for (int i = 0; i < n; i++)
        {
            indices[nomsStations[i]] = i;
            for (int j = 0; j < n; j++)
            {
                dist[i, j] = i == j ? 0 : int.MaxValue;
                next[i, j] = null;
            }
        }

        foreach (var c in connexions)
        {
            int i = indices[c.Item1];
            int j = indices[c.Item2];
            dist[i, j] = c.Item3;
            next[i, j] = c.Item2;
        }
        
        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (dist[i, k] != int.MaxValue && dist[k, j] != int.MaxValue &&
                        dist[i, j] > dist[i, k] + dist[k, j])
                    {
                        dist[i, j] = dist[i, k] + dist[k, j];
                        next[i, j] = next[i, k];
                    }
                }
            }
        }
        
        if (next[indices[depart], indices[arrivee]] == null)
            return (null, 0);

        var chemin = new List<string> { depart };
        int u = indices[depart];
        int v = indices[arrivee];
        
        while (u != v)
        {
            u = indices[next[u, v]];
            chemin.Add(nomsStations[u]);
        }

        return (chemin, dist[indices[depart], indices[arrivee]]);
    }
}