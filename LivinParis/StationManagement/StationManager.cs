using System;
using DBConnectLibrary;
using Graph;
using LivinParis.LivraisonManagement;
using Org.BouncyCastle.Math.EC;
using Spectre.Console;

namespace LivinParis.StationManagement;

public class StationManager : GlobalDataAccess
{
    public static Dictionary<int, StationLP> remplirMetroLP()
    {
        Dictionary<int, StationLP> Metro = new Dictionary<int, StationLP>();
        string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).ToArray();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim('"');
            }
            bool alreadyExists = false;

            foreach (StationLP stationLP in Metro.Values)
            {
                if (stationLP.libelle == parts[1])
                {
                    alreadyExists = true;
                    stationLP.lignes.Add(int.Parse(parts[7]));
                }
            }

            if (alreadyExists == false)
            {
                StationLP stationLP = new StationLP();
                stationLP.id = int.Parse(parts[0]);
                stationLP.libelle = parts[1];
                stationLP.lignes = new List<int>();
                stationLP.lignes.Add(int.Parse(parts[7]));
                
                Metro.Add(int.Parse(parts[0]), stationLP);
            }
        }

        return Metro;
    }
    
    public static Dictionary<int, StationLP> remplirMetroLPRed()
    {
        Dictionary<int, StationLP> Metro = new Dictionary<int, StationLP>();
        string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).ToArray();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim('"');
            }

            StationLP stationLP = new StationLP();
            stationLP.id = int.Parse(parts[0]);
            stationLP.libelle = parts[1];
            stationLP.lignes = new List<int>();
            stationLP.lignes.Add(int.Parse(parts[7]));
            
            Metro.Add(int.Parse(parts[0]), stationLP);
            
        }

        return Metro;
    }
    
    public string CheminMetro(int startId, int endId)
    {
        Graphe<string> g1 = new Graphe<string>("g1") { Titre = "MetroParis" };
        StationSelector selector = new StationSelector();
        g1.RemplirMetro();
        g1.LiensMetro();
        DijkstraAlgorithm<string> dijkstra = new DijkstraAlgorithm<string>(g1);
        
        var resultat = dijkstra.TrouverChemin(startId, endId);
        string s = "Voici le chemin que le cuisinier va emprunter : \n";
        foreach (string station in resultat.nomsStations)
        {
            s += selector.nomStationFromID(Convert.ToInt32(station)) + "\n";
        }
        
        return s;
    }
}