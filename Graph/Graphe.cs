using System.Runtime.CompilerServices;

namespace Graph;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Desktop;


public class Graphe<T>
{
    public required string Titre;
    private Dictionary<int, Noeud<T>> _dicoNoeuds;
    private Dictionary<int, Station> _stations;
    private bool _b;




    public Graphe(string titre, Dictionary<int, Noeud<T>>? noeuds = null, List<Lien<T>>? liens = null, bool _b = false)
    {
        this.Titre = titre;
        this._dicoNoeuds = noeuds ?? new Dictionary<int, Noeud<T>>();
        this._stations = new Dictionary<int, Station>();
    }


    public Dictionary<int, Noeud<T>> Noeuds
    {
        get => this._dicoNoeuds;
    }

    public object Station { get; set; }


    /// <summary>
    /// Initialise les noeuds du graphe d'après les valeurs du document texte.
    /// </summary>
    public void RemplirGraphe()
    {
        string lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(23).Take(1).First();
        string[] tokens = lines.Split(" ");
        int nbNoeuds = Int32.Parse(tokens[0]);
        for (int i = 0; i < nbNoeuds; i++)
        {
            Noeud<T> noeud = new Noeud<T>((i + 1).ToString())
                { Titre = (i + 1).ToString() }; //Répétition : pertinence du required ?
            int id = i + 1;
            this._dicoNoeuds.Add(id, noeud);
            //Console.WriteLine(noeud.Titre);
            //Console.WriteLine(id);
        }
    }

    /// <summary>
    /// Récupère la valeur de la taille du graphe d'après les valeurs du document texte.
    /// </summary>
    /// <returns></returns>
    public int TailleGraphe()
    {
        string lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(23).Take(1).First();
        string[] tokens = lines.Split(" ");
        int tailleGraphe = Int32.Parse(tokens[2]);
        return tailleGraphe;
    }

    /// <summary>
    /// Récupère la valeur de l'ordre du graphe dans le document texte.
    /// </summary>
    /// <returns></returns>

    public int OrdreGraphe()
    {
        string lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(23).Take(1).First();
        string[] tokens = lines.Split(" ");
        int ordreGraphe = Int32.Parse(tokens[0]);
        return ordreGraphe;
    }

    /// <summary>
    /// Initialise les liens du graphe.
    /// </summary>
    public void LiensGraphe()
    {
        string[] lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(24).ToArray();
        foreach (string line in lines)
        {
            string[] tokens = line.Split(" ");
            int numDepart = Int32.Parse(tokens[0]);
            int numArrive = Int32.Parse(tokens[1]);
            Noeud<T> noeudDepart = this._dicoNoeuds[numDepart];
            Noeud<T> noeudArrive = this._dicoNoeuds[numArrive];
            Lien<T> newLien = new Lien<T>(noeudDepart, noeudArrive);
            if (_b == false)
            {
                noeudDepart.Liens.Add(newLien);
                noeudArrive.Liens.Add(newLien);
            }
            else
            {
                noeudDepart.Liens.Add(newLien);
            }
        }
    }

    /// <summary>
    /// Affiche la liste d'adjacence du graphe, affichant les liens de chaque noeud.
    /// </summary>
    public void AfficherListeAdjacence()
    {
        string str = "";
        foreach (KeyValuePair<int, Noeud<T>> nodeKVP in this._dicoNoeuds)
        {
            str += nodeKVP.Value.AfficherLiens() + "\n";
        }

        Console.WriteLine(str);
    }

    /// <summary>
    /// Création d'une matrice d'adjacence, vérifiant si deux Noeuds sont liés.
    /// </summary>
    /// <returns></returns>
    public int[,] CreerMatriceAdjacence()
    {
        string lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(23).Take(1).First();
        string[] tokens = lines.Split(" ");
        int nbNoeuds = Int32.Parse(tokens[0]);
        int[,] mat = new int[nbNoeuds + 1, nbNoeuds + 1];
        for (int i = 0; i < nbNoeuds + 1; i++)
        {
            for (int j = 0; j < nbNoeuds + 1; j++)
            {
                if (i == 0)
                {
                    mat[i, j] = j;
                }
                else if (j == 0)
                {
                    mat[i, j] = i;
                }
                else
                {
                    Noeud<T> noeudI = this._dicoNoeuds[i];
                    Noeud<T> noeudJ = this._dicoNoeuds[j];
                    if ((noeudI.isLinked(noeudJ)) == true || noeudJ.isLinked(noeudI) == true)
                    {
                        mat[i, j] = 1;
                    }
                    else
                    {
                        mat[i, j] = 0;
                    }
                }
            }
        }

        return mat;
    }


    public void BFS(int startId)
    {
        if (!_dicoNoeuds.ContainsKey(startId))
        {
            Console.WriteLine("Le noeud spécifié n'existe pas.");
            return;
        }

        HashSet<int> visited = new HashSet<int>(); // Pour suivre les nœuds visités
        Queue<Noeud<T>> queue = new Queue<Noeud<T>>();

        Noeud<T> startNode = _dicoNoeuds[startId];
        queue.Enqueue(startNode);
        visited.Add(startId);

        Console.WriteLine("Parcours BFS à partir du nœud " + startNode.Titre + ":");

        while (queue.Count > 0)
        {
            Noeud<T> currentNode = queue.Dequeue();
            Console.Write(currentNode.Titre + " ");

            foreach (Lien<T> lien in currentNode.Liens)
            {
                Noeud<T> neighbor = lien.NoeudDepart == currentNode ? lien.NoeudArrive : lien.NoeudDepart;
                int neighborId = _dicoNoeuds.FirstOrDefault(x => x.Value == neighbor).Key;

                if (!visited.Contains(neighborId))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighborId);
                }
            }
        }

        Console.WriteLine();
    }

    public void DessinerGraphe(string filePath = "graphe.png", int width = 600, int height = 600)
    {
        using (var bitmap = new SKBitmap(width, height))
        using (var canvas = new SKCanvas(bitmap))
        {
            canvas.Clear(SKColors.White);

            int rayon = Math.Min(width, height) / 3; // Rayon du cercle des nœuds
            int centerX = width / 2, centerY = height / 2;
            int totalNodes = _dicoNoeuds.Count;
            double angleStep = (2 * Math.PI) / totalNodes;
            Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();

            // Générer les positions des nœuds
            int index = 0;
            foreach (var kvp in _dicoNoeuds)
            {
                int nodeId = kvp.Key;
                double angle = index * angleStep;
                float x = centerX + (float)(rayon * Math.Cos(angle));
                float y = centerY + (float)(rayon * Math.Sin(angle));
                positions[nodeId] = new SKPoint(x, y);
                index++;
            }

            // Dessiner les liens (arêtes)
            using (var paint = new SKPaint { Color = SKColors.Gray, StrokeWidth = 2 })
            {
                foreach (var kvp in _dicoNoeuds)
                {
                    Noeud<T> noeud = kvp.Value;
                    foreach (Lien<T> lien in noeud.Liens)
                    {
                        Noeud<T> autreNoeud = lien.NoeudDepart == noeud ? lien.NoeudArrive : lien.NoeudDepart;
                        int id1 = _dicoNoeuds.First(x => x.Value == noeud).Key;
                        int id2 = _dicoNoeuds.First(x => x.Value == autreNoeud).Key;

                        if (positions.ContainsKey(id1) && positions.ContainsKey(id2))
                        {
                            canvas.DrawLine(positions[id1], positions[id2], paint);
                        }
                    }
                }
            }

            // Dessiner les nœuds
            using (var nodePaint = new SKPaint { Color = SKColors.Blue, IsAntialias = true })
            using (var textPaint = new SKPaint { Color = SKColors.White, TextSize = 20, IsAntialias = true })
            {
                foreach (var kvp in positions)
                {
                    SKPoint pos = kvp.Value;
                    canvas.DrawCircle(pos, 20, nodePaint); // Dessiner le nœud

                    // Dessiner le texte au centre du cercle
                    var textBounds = new SKRect();
                    textPaint.MeasureText(kvp.Key.ToString(), ref textBounds);
                    float textX = pos.X - textBounds.MidX;
                    float textY = pos.Y - textBounds.MidY;
                    canvas.DrawText(kvp.Key.ToString(), textX, textY, textPaint);
                }
            }

            // Sauvegarde en image
            using (var image = SKImage.FromBitmap(bitmap))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            using (var stream = System.IO.File.OpenWrite(filePath))
            {
                data.SaveTo(stream);
            }
        }

        Console.WriteLine($"Graphe dessiné et enregistré sous {filePath}");
    }
/*
    public void ChargerStations()
    {
        string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis (1).xlsx").Skip(1).ToArray();

        foreach (string ligne in lines)
        {
            string[] tokens = ligne.Split('\t'); // Séparateur : tabulation

            if (tokens.Length < 7)
            {
                Console.WriteLine("Erreur : Ligne invalide dans le fichier.");
                continue;
            }

            int id;
            if (!int.TryParse(tokens[0], out id))
            {
                Console.WriteLine($"Erreur : ID invalide ({tokens[0]}).");
                continue;
            }

            string nom = tokens[1].Trim();

            int[] lignesMetro;
            try
            {
                lignesMetro = tokens[6].Split(',')
                    .Where(x => !string.IsNullOrWhiteSpace(x)) // Filtre les vides
                    .Select(int.Parse)
                    .ToArray();
            }
            catch
            {
                Console.WriteLine($"Erreur : Lignes invalides ({tokens[6]}) pour la station {nom}.");
                lignesMetro = Array.Empty<int>();
            }

            // Création et ajout de la station
            Station station = new Station(id, nom, lignesMetro);
            _stations[id] = station;

            Console.WriteLine(
                $"Station chargée : {station.Nom} (ID: {station.Id}) | Lignes: {string.Join(", ", station.Lignes)}");
        }

        Console.WriteLine($"Chargement terminé : {_stations.Count} stations ajoutées.");
    }
    */
    public void AfficherStations()
    {
        foreach (var station in _stations.Values)
        {
            Console.WriteLine($"Station {station.Nom} (ID: {station.Id}) | Lignes: {string.Join(", ", station.Lignes)}");
        }
    }
}


public static List<Station> ChargerStations()
{
    var stations = new List<Station>();
    /* string cheminFichier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MetroParis .csv");

     if (!File.Exists(cheminFichier))
     {
         Console.WriteLine("Erreur : Le fichier stations.csv est introuvable.");
         return stations;
     }
*/

    string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis (1).xlsx").Skip(1).ToArray();
    foreach (string ligne in lines)

    {

        string[] tokens = ligne.Split(',');


        int id = Convert.ToInt32(tokens[0]);

        string nom = tokens[1].Trim();

        Console.WriteLine("mon cul");
        int[] lignesMetro = Array.ConvertAll(tokens[6].Trim().Split('/'), int.Parse);
        Console.WriteLine("mon cul");



        // Ajout de la station à la liste
        
        stations.Add(new Station(id, nom, lignesMetro));
    }

    Console.WriteLine($"Chargement terminé : {stations.Count} stations ajoutées.");
    return stations;

}





