using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Graph;

public class Station
{
    public int id;
    public string nom;
    public int[] lignes;
    

    public Station(int id, string nom, int[] lignes)
    {
        this.id = id;
        this.nom = nom;
        this.lignes = lignes;
       

    }

    public int Id
    {
        get { return id; }
    }

    public string Nom
    {
        get { return nom; }
    }

    public int[] Lignes
    {
        get { return lignes; }
    }
}
/*
    public Station StationPrecedente
    {
        get { return stationPrecedente; }
        set { stationPrecedente = value; }
    }

    public Station StationSuivante
    {
        get { return stationSuivante; }
        set { stationSuivante = value; }
    }
   public void AfficherStation()
    {
        Console.WriteLine($"Station {Nom} (ID: {Id}) | Ligne(s): {string.Join(", ", Lignes)} | Précédente : {(stationPrecedente != null ? stationPrecedente.Nom : "Aucune")} | Suivante: {(stationSuivante != null ? stationSuivante.Nom : "Aucune")}");
    }
    /*public void AfficherStation()
    {
        Console.WriteLine($"Station {Nom} (ID: {Id}) | Ligne: {Lignes} | Précédente : {stationPrecedente} | Suivante: {
            stationsSuivante}");
    }



    // Méthode pour charger les stations depuis le fichier Excel


    public static List<Station> ChargerStations()
    {
        var stations = new List<Station>();
       /* string cheminFichier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MetroParis .csv");

        if (!File.Exists(cheminFichier))
        {
            Console.WriteLine("Erreur : Le fichier stations.csv est introuvable.");
            return stations;
        }


        string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis (1).xlsx").Skip(1).ToArray();
        foreach (string ligne in lines)

        {

            string[] tokens = ligne.Split(',');

            Console.WriteLine("mon cul");
            int id =Convert.ToInt32(tokens[0]);
            Console.WriteLine("mon cul");
            string nom = tokens[1].Trim();
            Console.WriteLine("mon cul");
            int precedente = 0;
            if (tokens[2].Trim() != "")
            {
                precedente = int.Parse(tokens[2].Trim());
            }
            Console.WriteLine("mon cul");
            int suivante = 0;
            if (tokens[3].Trim() != "")
            {
                suivante = int.Parse(tokens[3].Trim());
            }
            Console.WriteLine("mon cul");
            int[] lignesMetro = Array.ConvertAll(tokens[6].Trim().Split('/'), int.Parse);
            Console.WriteLine("mon cul");
            stations.Add(new Station(id, nom, lignesMetro, null, null));
        }


        return stations;
    }
}








/*
List<Station> stations = new List<Station>();

string cheminFichier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MetroParis.xlsx");

if (!File.Exists(cheminFichier))
{
    Console.WriteLine("Erreur : Le fichier MetroParis est introuvable.");
    return stations;
}

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

try
{
using (var stream = File.Open(cheminFichier, FileMode.Open, FileAccess.Read))
using (var reader = ExcelReaderFactory.CreateReader(stream))
{
    while (reader.Read())
    {
        if (reader.Depth == 0)
        {
            // Ignorer l'en-tête
            continue;
        }

        // Si la cellule est vide ou si l'id est invalide, on passe à la ligne suivante
        if (reader.GetValue(0) == null)
        {

            break;
        }

        int id = 0;
        string idStr = reader.GetValue(0).ToString();
        if (idStr == null || !int.TryParse(idStr, out id))
        {
            // Si l'id est invalide, on passe à la ligne suivante
            continue;
        }

        string nom = reader.GetValue(1) != null ? reader.GetValue(1).ToString() : "Nom inconnu";
        if (reader.GetValue(2) == null)
        {
            Console.WriteLine("Erreur : La ligne est vide ou incorrecte pour la station.");
            break;
        }

        string lignesStr = reader.GetValue(2).ToString();
        string[] lignesArray = lignesStr.Split(',');
        int[] lignes = new int[lignesArray.Length];
/*
        // Convertir les lignes en entiers
        for (int i = 0; i < lignesArray.Length; i++)
        {
            int ligne = 0;
            string ligneStr = lignesArray[i].Trim();
            if (ligneStr != null && int.TryParse(ligneStr, out ligne))
            {
                lignes[i] = ligne;
            }
            else
            {
                lignes[i] = 0;
            }
        }

        stations.Add(new Station(id, nom, lignes));
    }
}
}
catch (ExcelDataReader.Exceptions.HeaderException ex)
{
Console.WriteLine("Erreur : Le fichier Excel n'est pas valide ou il est dans un format incompatible.");
}
catch (Exception ex)
{
Console.WriteLine($"Erreur inconnue : {ex.Message}");
}

return stations;

}

}
*/

