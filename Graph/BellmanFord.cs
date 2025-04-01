namespace Graph;


    public class BellmanFord<T>
    {
        private readonly Graphe<T> _graphe;

        public BellmanFord(Graphe<T> graphe)
        {
            _graphe = graphe;
        }

        public Dictionary<int, int> TrouverChemins(int startId)
        {
            var distances = new Dictionary<int, int>();
            foreach (var noeud in _graphe.Noeuds.Keys)
            {
                distances[noeud] = int.MaxValue;
            }

            distances[startId] = 0;

            int nombreNoeuds = _graphe.Noeuds.Count;
            for (int i = 0; i < nombreNoeuds - 1; i++)
            {
                bool updated = false;

                foreach (var kvp in _graphe.Noeuds)
                {
                    Noeud<T> noeud = kvp.Value;
                    foreach (var lien in noeud.Liens)
                    {
                        Noeud<T> voisin = lien.NoeudDepart.Equals(noeud) ? lien.NoeudArrive : lien.NoeudDepart;
                        int voisinId = _graphe.Noeuds.First(x => x.Value == voisin).Key;

                        if (distances[kvp.Key] != int.MaxValue && distances[kvp.Key] + lien.Poids < distances[voisinId])
                        {
                            distances[voisinId] = distances[kvp.Key] + lien.Poids;
                            updated = true;
                        }
                    }
                }
                if (!updated)
                    break;
            }

            return distances;
        }
    }
