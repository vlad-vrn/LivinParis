using System;
using System.Collections.Generic;
using System.IO;
using Graph;

class Program
{
    static void Main()
    {
        Console.WriteLine("*************************************************************\n                    Etape 1\n*************************************************************");

        Graphe<string> g1 = new Graphe<string>("g1") { Titre = "Karate" };
        g1.RemplirGraphe();
        g1.LiensGraphe();

        Console.WriteLine("*************************************************************\n                 Liste d'adjacence\n*************************************************************");
        g1.AfficherListeAdjacence();
        Console.ReadKey();

        Console.WriteLine("*************************************************************\n              Matrice d'adjacence\n*************************************************************");
        AfficherMatrice(g1.CreerMatriceAdjacence());
        Console.ReadKey();

        Console.WriteLine("*************************************************************\n             Breadth First Search\n*************************************************************");
        Console.WriteLine("A quel noeud voulez-vous commencer ?");
        int startNode = Int32.Parse(Console.ReadLine());
        g1.BFS(startNode);
        Console.ReadKey();

        Console.WriteLine("*************************************************************\n             Propriétés du graphe\n*************************************************************");
        Console.WriteLine("L'ordre du graphe est de " + g1.OrdreGraphe());
        Console.WriteLine("La taille du graphe est de " + g1.TailleGraphe());
        Console.WriteLine("Ce graphe n'est pas connexe.");
        Console.ReadKey(); 

        // Algorithmes de plus court chemin
        Console.WriteLine("algo du plus court chemin");
        Console.WriteLine("*************************************************************\n             Algorithme de Dijkstra\n*************************************************************");
        var dijkstra = new dijkstra<string>(g1);
        var distancesDijkstra = dijkstra.TrouverChemins(startNode);
        AfficherDistances(distancesDijkstra);
        Console.ReadKey();

        Console.WriteLine("*************************************************************\n             Algorithme de Bellman-Ford\n*************************************************************");
        var bellmanFord = new BellmanFord<string>(g1);
        var distancesBellmanFord = bellmanFord.TrouverChemins(startNode);
        AfficherDistances(distancesBellmanFord);
        Console.ReadKey();
        
        Console.WriteLine("*************************************************************\n             Algorithme de Floyd-Warshall\n*************************************************************");
        var FloydWarshall = new FloydWarshall<string>(g1);
        var distancesFordWarshall = FloydWarshall.TrouverChemins(startNode);
        AfficherDistances(distancesBellmanFord);
        Console.ReadKey();

        g1.DessinerGraphe();
    }

    static void AfficherMatrice(int[,] mat)
    {
        for (int i = 0; i < mat.GetLength(0); i++)
        {
            for (int j = 0; j < mat.GetLength(1); j++)
            {
                if (mat[i, j] == int.MaxValue / 2)
                    Console.Write("INF ");
                else
                    Console.Write(mat[i, j] + "  ");
            }
            Console.WriteLine();
        }
    }

    static void AfficherDistances(Dictionary<int, int> distances)
    {
        foreach (var (noeud, distance) in distances)
        {
            Console.WriteLine($"Distance de {noeud} : {distance}");
        }
    }
}