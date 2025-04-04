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
    /// <summary>
    /// Initialise les noeuds du graphe d'après les valeurs du document texte.
    /// </summary>
    public void RemplirMetro()
    {
        string lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).Take(1).First();
        string[] tokens = lines.Split(",");
        int nbNoeuds = 333;
        for (int i = 0; i < nbNoeuds; i++)
        {
            Noeud<T> noeud = new Noeud<T>((i + 1).ToString())
                { Titre = (i + 1).ToString() }; 
            int id = i + 1;
            this._dicoNoeuds.Add(id, noeud);
            
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
   public void LiensMetro()
{
    Dictionary<string, List<(int id, string ligne)>> stationsParNom = new Dictionary<string, List<(int, string)>>();

    string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).ToArray();
    foreach (string line in lines)
    {
        string[] tokens = line.Split(',');
        for (int i = 0; i < tokens.Length; i++)
        {
            tokens[i] = tokens[i].Trim('"');
        }

        int idStation = int.Parse(tokens[0]);
        string nomStation = tokens[1];
        string ligne = tokens[7];
        if (!stationsParNom.ContainsKey(nomStation))
        {
            stationsParNom[nomStation] = new List<(int, string)>();
        }
        stationsParNom[nomStation].Add((idStation, ligne));

        if (int.TryParse(tokens[2], out int idPrecedent) && idPrecedent > 0)
        {
            int temps = int.Parse(tokens[4]);
            
            CreerLien(idStation, idPrecedent, temps);
        }

        if (int.TryParse(tokens[3], out int idSuivant) && idSuivant > 0)
        {
            int temps = int.Parse(tokens[4]);
            CreerLien(idStation, idSuivant, temps);
        }
    }

    foreach (string line in lines)
    {
        string[] tokens = line.Split(',');
        for (int i = 0; i < tokens.Length; i++)
        {
            tokens[i] = tokens[i].Trim('"');
        }

        int idStation = int.Parse(tokens[0]);
        string nomStation = tokens[1];
        string ligne = tokens[7];

        if (int.TryParse(tokens[5], out int tempsChangement) && tempsChangement > 0)
        {
            var stationsCorrespondantes = stationsParNom[nomStation]
                .Where(x => x.ligne != ligne)
                .ToList();

            foreach (var correspondance in stationsCorrespondantes)
            {
                CreerLien(idStation, correspondance.id, tempsChangement);
                if (!_b) 
                {
                    CreerLien(correspondance.id, idStation, tempsChangement);
                }
            }
        }
    }
}

private void CreerLien(int idDepart, int idArrivee, int poids)
{
    Noeud<T> noeudDepart = this._dicoNoeuds[idDepart];
    Noeud<T> noeudArrivee = this._dicoNoeuds[idArrivee];
    
    Lien<T> newLien = new Lien<T>(noeudDepart, noeudArrivee, poids);
    noeudDepart.Liens.Add(newLien);
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
            string lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).Take(1).First();
            string[] tokens = lines.Split(',');
            
            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }
            int nbNoeuds =332 ;
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

            HashSet<int> visited = new HashSet<int>(); 
            Queue<Noeud<T>> queue = new Queue<Noeud<T>>();

            Noeud<T> startNode = _dicoNoeuds[startId];
            queue.Enqueue(startNode);
            visited.Add(startId);

            Console.WriteLine("Parcours BFS à partir du noeud " + startNode.Titre + ":");

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



        public static List<Station> ChargerStations()
        {
            var stations = new List<Station>();

            string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).ToArray();

            if (lines.Length == 0)
            {
                Console.WriteLine("Le fichier CSV est vide.");
            }

            foreach (string ligne in lines)
            {
                

                string[] tokens = ligne.Split(',');
                for (int i = 0; i < tokens.Length; i++)
                {
                    tokens[i] = tokens[i].Trim('"');
                }
                
                int id = Convert.ToInt32(tokens[0]);

                string nom = tokens[1].Trim();

                double[] lignesMetro = Array.ConvertAll(tokens[7].Split('/'), double.Parse);

               stations.Add(new Station(id, nom, lignesMetro));
            }

            Console.WriteLine($"Chargement terminé : {stations.Count} stations ajoutées.");
            return stations;


        }


        public void DessinerGraphe(int width = 1000, int height = 1000)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "graphe_metro.png");

            if (_dicoNoeuds == null || _dicoNoeuds.Count == 0)
            {
                Console.WriteLine("Erreur : Le dictionnaire des nœuds est vide ou non initialisé.");
                return;
            }

            using (var bitmap = new SKBitmap(width, height))
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.White);

                int rayon = Math.Min(width, height) / 2 - 100; // Rayon ajusté
                int centerX = width / 2, centerY = height / 2;
                double angleStep = (2 * Math.PI) / _dicoNoeuds.Count;
                Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();

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

                using (var paint = new SKPaint { Color = SKColors.Gray, StrokeWidth = 2, IsAntialias = true })
                using (var textPaint = new SKPaint { Color = SKColors.Black, TextSize = 20, IsAntialias = true })
                {
                    foreach (var kvp in _dicoNoeuds)
                    {
                        if (kvp.Value == null)
                        {
                            Console.WriteLine($"Erreur : Noeud {kvp.Key} est null.");
                            continue;
                        }

                        Noeud<T> noeud = kvp.Value;

                        foreach (Lien<T> lien in noeud.Liens)
                        {
                            Noeud<T> autreNoeud = lien.NoeudDepart == noeud ? lien.NoeudArrive : lien.NoeudDepart;

                            if (!_dicoNoeuds.ContainsValue(autreNoeud))
                            {
                                Console.WriteLine($"Erreur : Le lien vers le nœud {autreNoeud.id} est invalide.");
                                continue;
                            }

                            int id1 = _dicoNoeuds.First(x => x.Value == noeud).Key;
                            int id2 = _dicoNoeuds.First(x => x.Value == autreNoeud).Key;

                            if (positions.ContainsKey(id1) && positions.ContainsKey(id2))
                            {
                                canvas.DrawLine(positions[id1], positions[id2], paint);

                                SKPoint midPoint = new SKPoint((positions[id1].X + positions[id2].X) / 2,
                                    (positions[id1].Y + positions[id2].Y) / 2);
                                canvas.DrawText(lien.Poids.ToString(), midPoint.X, midPoint.Y, textPaint);
                            }
                        }
                    }
                }

                using (var nodePaint = new SKPaint { Color = SKColors.Blue, IsAntialias = true })
                using (var textPaint = new SKPaint { Color = SKColors.Black, TextSize = 18, IsAntialias = true })
                {
                    foreach (var kvp in positions)
                    {
                        int nodeId = kvp.Key;
                        SKPoint pos = kvp.Value;

                        if (!_dicoNoeuds.ContainsKey(nodeId) || _dicoNoeuds[nodeId] == null)
                        {
                            Console.WriteLine($"Erreur : Noeud {nodeId} est null ou introuvable.");
                            continue;
                        }
                        var contenu = _dicoNoeuds[nodeId]._contenu;
                        string stationName = contenu != null ? contenu.ToString() : "Inconnu";

                        canvas.DrawCircle(pos, 10, nodePaint);

                        canvas.DrawText(stationName, pos.X + 15, pos.Y + 5, textPaint);
                    }
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite(filePath))
                {
                    data.SaveTo(stream);
                }
            }

            Console.WriteLine($"Graphe dessiné et enregistré sous {filePath}");
        }
}

 





