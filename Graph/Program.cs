

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Graph;

Fonctions fonction = new Fonctions();

Console.WriteLine("Chargement des stations...");

        List<Station> stations = Graphe<string>.ChargerStations();

        Graphe<string> g1 = new Graphe<string>("g1") { Titre = "MetroParis" };

        g1.RemplirMetro();
        g1.LiensMetro();
        
        fonction.voirStations(g1, stations);

        Console.ReadKey();

        g1.AfficherListeAdjacence();


        Console.WriteLine("COLORATION DU GRAPHE (Algorithme Welsh-Powell)");

        var coloration = new Coloration<string>(g1);
        coloration.AppliquerWelshPowell();
        coloration.AfficherResultats();
        Console.WriteLine($"\nNombre de couleurs utilisées: {coloration.NombreMinimalCouleurs}");
        Console.WriteLine($"Est biparti: {coloration.EstBiparti()}");
        Console.WriteLine($"Est planaire: {coloration.EstPlanaire()}");
        var groupes = coloration.GetGroupesIndependants();
        foreach (var groupe in groupes)
        {
            Console.WriteLine($"\nGroupe {groupe.Key} (couleur):");
            foreach (var idStation in groupe.Value)
            {
                var station = g1.Stations[idStation];
                Console.WriteLine($"- {station.Nom} (Lignes: {string.Join(", ", station.Lignes)})");
            }
            
        }



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
        if (!g1.Noeuds.ContainsKey(startId) || !g1.Noeuds.ContainsKey(endId))
        {
            Console.WriteLine("Une ou plusieurs stations n'existent pas.");
            return;
        }
        DijkstraAlgorithm<string> dijkstra = new DijkstraAlgorithm<string>(g1);
        var resultat = dijkstra.TrouverChemin(startId, endId);
        Console.WriteLine($"Distance totale: {resultat.distanceTotale} minutes");
        Console.WriteLine("Itinéraire:");
        for (int i = 0; i < resultat.idsStations.Count; i++)
        {
            Console.WriteLine($"{resultat.idsStations[i]}");
        }
      


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

        AfficherMatriceAdjacence(g1.CreerMatriceAdjacenceMetro());
        Console.ReadKey();
        Console.WriteLine("\n\n");
            
        Console.ReadKey();

        g1.DessinerGraphe();
        