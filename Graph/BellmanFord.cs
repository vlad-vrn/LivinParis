namespace Graph;

public class BellmanFord
{
    public class MetroBellmanFord
{
    public class Station
    {
        public string Nom { get; set; }
        public List<Connexion> Connexions { get; } = new List<Connexion>();
    }

    public class Connexion
    {
        public string Destination { get; set; }
        public int Temps { get; set; }
    }

    public static (List<string> chemin, int tempsTotal) TrouverItineraire(
        List<Station> reseau, 
        string depart, 
        string arrivee)
    {
        var temps = new Dictionary<string, int>();
        var precedents = new Dictionary<string, string>();
        
        foreach (var station in reseau)
        {
            temps[station.Nom] = int.MaxValue;
            precedents[station.Nom] = null;
        }
        temps[depart] = 0;
        for (int i = 1; i < reseau.Count; i++)
        {
            foreach (var station in reseau)
            {
                foreach (var connexion in station.Connexions)
                {
                    if (temps[station.Nom] != int.MaxValue && 
                        temps[connexion.Destination] > temps[station.Nom] + connexion.Temps)
                    {
                        temps[connexion.Destination] = temps[station.Nom] + connexion.Temps;
                        precedents[connexion.Destination] = station.Nom;
                    }
                }
            }
        }
        if (precedents[arrivee] == null && depart != arrivee)
            return (null, 0);

        var chemin = new List<string>();
        var current = arrivee;
        while (current != depart)
        {
            chemin.Insert(0, current);
            current = precedents[current];
        }
        chemin.Insert(0, depart);

        return (chemin, temps[arrivee]);
    }
}

}