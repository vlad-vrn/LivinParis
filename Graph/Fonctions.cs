namespace Graph;

public class Fonctions
{
    public void voirStations(Graphe<string> g1, List<Station> stations)
    {
        Console.WriteLine("Stations chargées :");
        foreach (var station in stations)
        {
            Console.WriteLine($"ID: {station.Id}, Nom: {station.Nom}, Lignes: {string.Join(", ", station.Lignes)}");
        }
    }
}