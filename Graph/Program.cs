// See https://aka.ms/new-console-template for more information

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Graph;



        List<Station> stations = Graphe<string>.ChargerStations();
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
            Console.ReadKey();