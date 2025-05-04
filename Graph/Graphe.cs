using System.Runtime.CompilerServices;

namespace Graph;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Globalization;

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
    public Dictionary<int, Station> Stations => this._stations;
    


    /// <summary>
    /// Initialise les noeuds du graphe d'après les valeurs du document texte.
    /// </summary>
    /*
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
*/
    /*
    
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
            Console.WriteLine($"Nombre de stations chargées : {_dicoNoeuds.Count}");
        }
    }
    */
    public void RemplirMetro()
    {
        // Charger d'abord les stations
        var stationsList = ChargerStations();
        foreach (var station in stationsList)
        {
            _stations[station.Id] = station;
            
            // Créer les nœuds correspondants
            Noeud<T> noeud = new Noeud<T>(station.Nom) 
            { 
                Titre = station.Nom,
                _contenu = (T)Convert.ChangeType(station.Nom, typeof(T)) // Stocker le nom de la station dans le contenu
            };
            _dicoNoeuds.Add(station.Id, noeud);
        }
        
        Console.WriteLine($"Nombre de stations chargées : {_dicoNoeuds.Count}");
        
        // Ensuite créer les liens
        LiensMetro();
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
    /*
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
    */
     
    
    public void LiensMetro()
{
    var stationsParNom = new Dictionary<string, List<(int id, string ligne)>>();

    // Lire toutes les lignes du fichier CSV (sauf l'en-tête)
    string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).ToArray();

    foreach (string line in lines)
    {
        string[] tokens = line.Split(',').Select(t => t.Trim('"')).ToArray();

        if (tokens.Length < 8) continue; // Validation minimum

        int idStation = int.Parse(tokens[0]);
        string nomStation = tokens[1];
        string ligne = tokens[7];

        // Enregistrer la station dans le dictionnaire pour correspondances
        if (!stationsParNom.ContainsKey(nomStation))
            stationsParNom[nomStation] = new List<(int, string)>();

        stationsParNom[nomStation].Add((idStation, ligne));

        // Création des liens vers la station précédente
        if (int.TryParse(tokens[2], out int idPrecedent) && idPrecedent > 0)
        {
            if (int.TryParse(tokens[4], out int temps))
                CreerLien(idStation, idPrecedent, temps);
        }

        // Création des liens vers la station suivante
        if (int.TryParse(tokens[3], out int idSuivant) && idSuivant > 0)
        {
            if (int.TryParse(tokens[4], out int temps))
                CreerLien(idStation, idSuivant, temps);
        }
    }

    // Deuxième passe : gérer les correspondances
    foreach (string line in lines)
    {
        string[] tokens = line.Split(',').Select(t => t.Trim('"')).ToArray();

        if (tokens.Length < 8) continue;

        int idStation = int.Parse(tokens[0]);
        string nomStation = tokens[1];
        string ligne = tokens[7];

        // Si un temps de changement est défini, créer les liens de correspondance
        if (int.TryParse(tokens[5], out int tempsChangement) && tempsChangement > 0)
        {
            var correspondances = stationsParNom[nomStation]
                .Where(x => x.ligne != ligne && x.id != idStation);

            foreach (var correspondance in correspondances)
            {
                CreerLien(idStation, correspondance.id, tempsChangement);

                // Si graphe non orienté, créer aussi l'inverse
                if (!_b)
                    CreerLien(correspondance.id, idStation, tempsChangement);
            }
        }
    }
}

/// <summary>
/// Crée un lien entre deux noeuds s'ils existent dans le dictionnaire.
/// </summary>
private void CreerLien(int idDepart, int idArrivee, int poids)
{
    if (_dicoNoeuds.ContainsKey(idDepart) && _dicoNoeuds.ContainsKey(idArrivee))
    {
        Noeud<T> noeudDepart = _dicoNoeuds[idDepart];
        Noeud<T> noeudArrivee = _dicoNoeuds[idArrivee];

        // Éviter les doublons
        if (!noeudDepart.Liens.Any(l => l.NoeudArrive == noeudArrivee && l.Poids == poids))
        {
            Lien<T> lien = new Lien<T>(noeudDepart, noeudArrivee, poids);
            noeudDepart.Liens.Add(lien);

            // Si le graphe est non orienté
            if (!_b)
                noeudArrivee.Liens.Add(lien);
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
    /*
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
*/
    public int[,] CreerMatriceAdjacence()
    {
        string lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).Take(1).First();
        string[] tokens = lines.Split(',');

        for (int i = 0; i < tokens.Length; i++)
        {
            tokens[i] = tokens[i].Trim('"');
        }

        int nbNoeuds = 332;
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
    
/*
    public void DessinerGraphe(int width = 1000, int height = 1000)
    
{
    // Vérification de l'existence du dictionnaire des noeuds
    if (_dicoNoeuds == null || _dicoNoeuds.Count == 0)
    {
        Console.WriteLine("Erreur : Le dictionnaire des nœuds est vide ou non initialisé.");
        return;
    }

   
    if (_stations == null || _stations.Count == 0)
    {
        Console.WriteLine("Erreur : aucune station à dessiner.");
        return;
    }
    // Créer un dictionnaire pour stocker les positions des stations (en pixels)
    Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();

    // Définir les bornes pour normaliser les coordonnées (latitude, longitude)
    double minLongitude = _stations.Values.Min(station => station.Longitude);
    double maxLongitude = _stations.Values.Max(station => station.Longitude);
    double minLatitude = _stations.Values.Min(station => station.Latitude);
    double maxLatitude = _stations.Values.Max(station => station.Latitude);

    // Créer un canvas pour dessiner le graphe
    using (var bitmap = new SKBitmap(width, height))
    using (var canvas = new SKCanvas(bitmap))
    {
        canvas.Clear(SKColors.White);

        // Itérer sur chaque station et calculer sa position en pixels
        foreach (var station in _stations.Values)
        {
            // Normaliser les coordonnées (latitude, longitude) en fonction de l'espace du graphe
            float x = (float)((station.Longitude - minLongitude) / (maxLongitude - minLongitude) * width);
            float y = (float)((station.Latitude - minLatitude) / (maxLatitude - minLatitude) * height);

            positions[station.Id] = new SKPoint(x, y);
        }

        // Dessiner les liens (arêtes entre stations)
        using (var paint = new SKPaint { Color = SKColors.Gray, StrokeWidth = 2, IsAntialias = true })
        using (var textPaint = new SKPaint { Color = SKColors.Black, TextSize = 20, IsAntialias = true })
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
                        // Dessiner l'arête entre les deux stations
                        canvas.DrawLine(positions[id1], positions[id2], paint);

                        // Afficher la pondération (temps entre stations) au milieu du segment
                        SKPoint midPoint = new SKPoint((positions[id1].X + positions[id2].X) / 2,
                                                       (positions[id1].Y + positions[id2].Y) / 2);
                        canvas.DrawText(lien.Poids.ToString(), midPoint.X, midPoint.Y, textPaint);
                    }
                }
            }
        }

        // Dessiner les stations (nœuds)
        using (var nodePaint = new SKPaint { Color = SKColors.Blue, IsAntialias = true })
        using (var textPaint = new SKPaint { Color = SKColors.Black, TextSize = 18, IsAntialias = true })
        {
            foreach (var kvp in positions)
            {
                int nodeId = kvp.Key;
                SKPoint pos = kvp.Value;

                // Récupérer le nom de la station
                var station = _stations[nodeId];
                string stationName = station.Nom;

                // Dessiner le nœud (cercle)
                canvas.DrawCircle(pos, 10, nodePaint);

                // Afficher le nom de la station
                canvas.DrawText(stationName, pos.X + 15, pos.Y + 5, textPaint);
            }
        }

        // Sauvegarder l'image
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "graphe_metro2" +
                                                    ".png");

        using (var image = SKImage.FromBitmap(bitmap))
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        using (var stream = File.OpenWrite(filePath))
        {
            data.SaveTo(stream);
        }
        Console.WriteLine($"Graphe dessiné et enregistré sous {filePath}");
    }

    
}
*/

public void DessinerGraphe(int width = 1200, int height = 1000)
{
    if (_stations == null || _stations.Count == 0)
    {
        Console.WriteLine("Erreur : aucune station à dessiner. Avez-vous appelé RemplirMetro() ?");
        return;
    }

    // Welsh-Powell : Calcul de la coloration des sommets
    Dictionary<int, int> couleurParStation = WelshPowellColoration();

    // Palette de couleurs (à élargir si plus de 10 groupes)
    List<SKColor> palette = new List<SKColor>
    {
        SKColors.Red, SKColors.Blue, SKColors.Green, SKColors.Orange, SKColors.Purple,
        SKColors.Brown, SKColors.Magenta, SKColors.Teal, SKColors.Yellow, SKColors.Gray
    };

    // Créer un dictionnaire pour stocker les positions des stations
    Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();

    // Calculer les bornes géographiques
    double minLong = _stations.Values.Min(s => s.Longitude);
    double maxLong = _stations.Values.Max(s => s.Longitude);
    double minLat = _stations.Values.Min(s => s.Latitude);
    double maxLat = _stations.Values.Max(s => s.Latitude);

    float margin = 50;
    float plotWidth = width - 2 * margin;
    float plotHeight = height - 2 * margin;

    using (var bitmap = new SKBitmap(width, height))
    using (var canvas = new SKCanvas(bitmap))
    {
        canvas.Clear(SKColors.White);

        foreach (var station in _stations.Values)
        {
            float x = margin + (float)((station.Longitude - minLong) / (maxLong - minLong) * plotWidth);
            float y = height - margin - (float)((station.Latitude - minLat) / (maxLat - minLat) * plotHeight);
            positions[station.Id] = new SKPoint(x, y);
        }

        // Couleurs des lignes de métro
        var lineColors = new Dictionary<string, SKColor>
        {
            {"1", new SKColor(255, 20, 147)}, {"2", new SKColor(0, 155, 58)},
            {"3", new SKColor(153, 102, 51)}, {"4", new SKColor(187, 0, 187)},
            {"5", new SKColor(255, 127, 0)}, {"6", new SKColor(0, 190, 255)},
            {"7", new SKColor(255, 255, 0)}, {"8", new SKColor(200, 200, 200)},
            {"9", new SKColor(200, 100, 0)}, {"10", new SKColor(220, 180, 0)},
            {"11", new SKColor(100, 200, 100)}, {"12", new SKColor(0, 100, 255)},
            {"13", new SKColor(100, 100, 255)}, {"14", new SKColor(100, 0, 100)}
        };

        var lignesDessinees = new HashSet<string>();
        foreach (var kvp in _dicoNoeuds)
        {
            var noeud = kvp.Value;
            foreach (Lien<T> lien in noeud.Liens)
            {
                var autreNoeud = lien.NoeudDepart == noeud ? lien.NoeudArrive : lien.NoeudDepart;
                int id1 = kvp.Key;
                int id2 = _dicoNoeuds.First(x => x.Value == autreNoeud).Key;

                if (positions.ContainsKey(id1) && positions.ContainsKey(id2))
                {
                    var station1 = _stations[id1];
                    var station2 = _stations[id2];
                    var lignesCommune = station1.Lignes.Intersect(station2.Lignes).FirstOrDefault();

                    if (lignesCommune != null && !lignesDessinees.Contains($"{id1}-{id2}"))
                    {
                        SKColor lineColor = lineColors.ContainsKey(lignesCommune) ? lineColors[lignesCommune] : SKColors.Gray;

                        using (var linePaint = new SKPaint
                        {
                            Color = lineColor,
                            StrokeWidth = 6,
                            IsAntialias = true,
                            Style = SKPaintStyle.Stroke
                        })
                        {
                            canvas.DrawLine(positions[id1], positions[id2], linePaint);
                            lignesDessinees.Add($"{id1}-{id2}");
                            lignesDessinees.Add($"{id2}-{id1}");
                        }
                    }
                }
            }
        }

        // Dessiner les stations colorées selon leur groupe
        using (var borderPaint = new SKPaint { Color = SKColors.Black, StrokeWidth = 2, IsAntialias = true, Style = SKPaintStyle.Stroke })
        {
            foreach (var kvp in positions)
            {
                int nodeId = kvp.Key;
                SKPoint pos = kvp.Value;

                int couleurId = couleurParStation.ContainsKey(nodeId) ? couleurParStation[nodeId] : 0;
                var fillColor = couleurId < palette.Count ? palette[couleurId] : SKColors.DarkSlateGray;

                using (var nodePaint = new SKPaint { Color = fillColor, IsAntialias = true })
                {
                    canvas.DrawCircle(pos, 8, nodePaint);
                    canvas.DrawCircle(pos, 8, borderPaint);
                }
            }
        }

        // Dessiner les noms des stations importantes
        using (var textPaint = new SKPaint
        {
            Color = SKColors.Black,
            TextSize = 14,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial")
        })
        {
            var stationsImportantes = new List<string>
            {
                "Châtelet", "Gare du Nord", "Montparnasse", "Saint-Lazare",
                "La Défense", "Charles de Gaulle - Étoile", "Bastille", "République"
            };

            foreach (var kvp in positions)
            {
                int nodeId = kvp.Key;
                SKPoint pos = kvp.Value;
                var station = _stations[nodeId];

                if (stationsImportantes.Contains(station.Nom))
                {
                    canvas.DrawText(station.Nom, pos.X + 12, pos.Y - 12, textPaint);
                }
            }
        }

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "metro_paris_colorie.png");

        using (var image = SKImage.FromBitmap(bitmap))
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        using (var stream = File.OpenWrite(filePath))
        {
            data.SaveTo(stream);
        }
    }

    Console.WriteLine($"Carte colorée du métro enregistrée sous: metro_paris_colorie.png");
}
private Dictionary<int, int> WelshPowellColoration()
{
    // Trie des sommets par ordre décroissant de degré
    var sommetsTries = _dicoNoeuds
        .OrderByDescending(kvp => kvp.Value.Liens.Count)
        .Select(kvp => kvp.Key)
        .ToList();

    var couleurParSommet = new Dictionary<int, int>();

    foreach (var sommet in sommetsTries)
    {
        var couleursUtiliséesParVoisins = new HashSet<int>();

        foreach (var lien in _dicoNoeuds[sommet].Liens)
        {
            var voisin = lien.NoeudArrive == _dicoNoeuds[sommet] ? lien.NoeudDepart : lien.NoeudArrive;
            int idVoisin = _dicoNoeuds.First(x => x.Value == voisin).Key;

            if (couleurParSommet.ContainsKey(idVoisin))
            {
                couleursUtiliséesParVoisins.Add(couleurParSommet[idVoisin]);
            }
        }

        // Cherche la plus petite couleur disponible
        int couleur = 0;
        while (couleursUtiliséesParVoisins.Contains(couleur))
        {
            couleur++;
        }

        couleurParSommet[sommet] = couleur;
    }

    Console.WriteLine($"Nombre de couleurs utilisées : {couleurParSommet.Values.Max() + 1}");
    return couleurParSommet;
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
            /// <summary>
            /// On retire les guillemets pour chaque token
            /// <summary>
            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }
            


            string idString = tokens[0].Trim();
            if (!int.TryParse(idString, out int id))
            {
                Console.WriteLine($"Erreur : ID invalide ({idString})");
                continue;
            }

           

            string nom = tokens[1].Trim();
            //double longitude = Convert.ToDouble(tokens[9]);
            //double latitude = Convert.ToDouble(tokens[10]);
            if (!double.TryParse(tokens[9], NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude) ||
                !double.TryParse(tokens[10], NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude))
            {
                Console.WriteLine($"Erreur : coordonnées invalides pour la station {nom}");
                continue;
            }

            List<string> lignesMetro = tokens[7]
                .Split('/') // Séparer les lignes par "/"
                .Select(ligne => ligne.Trim()) // Supprimer les espaces
                .ToList();
            /// <summary>
            /// Ajout de la station à la liste
            /// <summary>
            stations.Add(new Station(id, nom, longitude, latitude, lignesMetro));

        }

        Console.WriteLine($"Chargement terminé : {stations.Count} stations ajoutées.");
        return stations;


    }
}

 





