// See https://aka.ms/new-console-template for more information

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Graph;

/*
Console.WriteLine("*************************************************************\n                    Etape 1\n*************************************************************");
///List<Point> points = new List<Point>();
///List<Noeud<string>> noeuds = new List<Noeud<string>>();
///Graphe g1 = new Graphe<T>("titre", noeuds, points);


static void AfficherMatrice(int[,] mat)
{
    for (int i = 0; i < mat.GetLength(0); i++)
    {
        for (int j = 0; j < mat.GetLength(1); j++)
        {
            if (mat[i, j] < 10)
            {
                Console.Write(mat[i, j] + "  ");
            }
            else
            {
                Console.Write(mat[i, j] + " ");
            }
        }
        Console.WriteLine();
    }
}

Console.ReadKey();
Console.WriteLine("\n\n");
static void AfficherFichier()
{
    string[] lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(24).ToArray();
    foreach (string line in lines)
    {
        Console.WriteLine(line);
        Console.ReadKey();
    }
}

//AfficherFichier();

Graphe<string> g1 = new Graphe<string>("g1") { Titre = "Karate" };

g1.RemplirGraphe();
g1.LiensGraphe();
Console.WriteLine("*************************************************************\n                 Liste d'adjacence\n*************************************************************");
g1.AfficherListeAdjacence();
Console.WriteLine("\n\n");
Console.ReadKey();

Console.WriteLine("*************************************************************\n              Matrice d'adjacence\n*************************************************************");

AfficherMatrice(g1.CreerMatriceAdjacence());
Console.ReadKey();
Console.WriteLine("\n\n");

Console.WriteLine("*************************************************************\n             Breadth First Search\n*************************************************************");
Console.WriteLine("A quelle noeud voulez-vous commencer ?");
int i = Int32.Parse(Console.ReadLine());
g1.BFS(i);
Console.ReadKey();
Console.WriteLine("\n\n");

Console.WriteLine("*************************************************************\n             Propriétés du graphe\n*************************************************************");

Console.WriteLine("L'ordre du graphe est de " + g1.OrdreGraphe());
Console.WriteLine("La taille du graphe est de " + g1.TailleGraphe());
Console.WriteLine("Ce graphe n'est pas connexe.");
Console.ReadKey();
Console.WriteLine("\n\n");

g1.DessinerGraphe();
 
*/


Console.WriteLine("Chargement des stations...");

        // Charger les stations en appelant la méthode ChargerStations
        List<Station> stations = Graphe<string>.ChargerStations();
/*
        // Afficher les stations chargées
        Console.WriteLine("Stations chargées :");
        foreach (var station in stations)
        {
            Console.WriteLine($"ID: {station.Id}, Nom: {station.Nom}, Lignes: {string.Join(", ", station.Lignes)}");
        }
*/
        // Attente d'une touche pour fermer la console
      //  Console.WriteLine("Appuyez sur une touche pour quitter...");
       // Console.ReadKey();


//AfficherFichier();

        Graphe<string> g1 = new Graphe<string>("g1") { Titre = "MetroParis" };

        g1.RemplirMetro();

        g1.LiensMetro();
        Console.WriteLine("LienMetro ok");
        g1.AfficherListeAdjacence();
        Console.Write("Entrez l'ID de la station de départ : ");
        if (!int.TryParse(Console.ReadLine(), out int startId))
        {
            Console.WriteLine("ID de départ invalide.");
            return;
        }

        Console.Write("Entrez l'ID de la station d'arrivée : ");
        if (!int.TryParse(Console.ReadLine(), out int endId))
        {
            Console.WriteLine("ID d'arrivée invalide.");
            return;
        }
        /// <summary>
        /// Vérification si les stations existent dans le graphe
        /// <summary>
        if (!g1.Noeuds.ContainsKey(startId) || !g1.Noeuds.ContainsKey(endId))
        {
            Console.WriteLine("Une ou plusieurs stations n'existent pas.");
            return;
        }
        /// <summary>
        /// Exécution de l'algorithme de Dijkstra
        /// <summary>
        DijkstraAlgorithm<string> dijkstra = new DijkstraAlgorithm<string>(g1);
        var resultat = dijkstra.TrouverChemin(startId, endId);

        /// <summary>
        /// Affichage du résultat
        /// <summary>
        Console.WriteLine($"Distance totale: {resultat.distanceTotale} minutes");
        Console.WriteLine("Itinéraire:");
        for (int i = 0; i < resultat.idsStations.Count; i++)
        {
            Console.WriteLine($"{resultat.idsStations[i]}");
        }

        g1.DessinerGraphe();
        

            void AfficherMatriceAdjacence(int[,] mat)
            {
                int taille = mat.GetLength(0);

                Console.WriteLine("Matrice d'adjacence du graphe :\n");

                for (int i = 0; i < taille; i++)
                {
                    for (int j = 0; j < taille; j++)
                    {
                        Console.Write(mat[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }

            /*AfficherMatriceAdjacence(g1.CreerMatriceAdjacence());
            Console.ReadKey();
            Console.WriteLine("\n\n");
            */
            Console.ReadKey();
        
       // g1.DessinerGraphe();
        