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

    public void RemplirMetro()
    {
        
        var stationsList = ChargerStations();
        foreach (var station in stationsList)
        {
            _stations[station.Id] = station;
            

            Noeud<T> noeud = new Noeud<T>(station.Nom) 
            { 
                Titre = station.Nom,
                _contenu = (T)Convert.ChangeType(station.Nom, typeof(T)) 
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
     
    
    public void LiensMetro()
{
    var stationsParNom = new Dictionary<string, List<(int id, string ligne)>>();

    string[] lines = File.ReadAllLines("..\\..\\..\\MetroParis.csv").Skip(1).ToArray();

    foreach (string line in lines)
    {
        string[] tokens = line.Split(',').Select(t => t.Trim('"')).ToArray();

        if (tokens.Length < 8) continue; 

        int idStation = int.Parse(tokens[0]);
        string nomStation = tokens[1];
        string ligne = tokens[7];
        if (!stationsParNom.ContainsKey(nomStation))
            stationsParNom[nomStation] = new List<(int, string)>();

        stationsParNom[nomStation].Add((idStation, ligne));
        
        if (int.TryParse(tokens[2], out int idPrecedent) && idPrecedent > 0)
        {
            if (int.TryParse(tokens[4], out int temps))
                CreerLien(idStation, idPrecedent, temps);
        }
        if (int.TryParse(tokens[3], out int idSuivant) && idSuivant > 0)
        {
            if (int.TryParse(tokens[4], out int temps))
                CreerLien(idStation, idSuivant, temps);
        }
    }
    foreach (string line in lines)
    {
        string[] tokens = line.Split(',').Select(t => t.Trim('"')).ToArray();

        if (tokens.Length < 8) continue;

        int idStation = int.Parse(tokens[0]);
        string nomStation = tokens[1];
        string ligne = tokens[7];

        if (int.TryParse(tokens[5], out int tempsChangement) && tempsChangement > 0)
        {
            var correspondances = stationsParNom[nomStation]
                .Where(x => x.ligne != ligne && x.id != idStation);

            foreach (var correspondance in correspondances)
            {
                CreerLien(idStation, correspondance.id, tempsChangement);

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
        if (!noeudDepart.Liens.Any(l => l.NoeudArrive == noeudArrivee && l.Poids == poids))
        {
            Lien<T> lien = new Lien<T>(noeudDepart, noeudArrivee, poids);
            noeudDepart.Liens.Add(lien);
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
    public int[,] CreerMatriceAdjacenceMetro()
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
    

public void DessinerGraphe(int width = 1500, int height = 1200)
{
    if (_stations == null || _stations.Count == 0)
    {
        Console.WriteLine("Erreur : Aucune station chargée. Appelez d'abord RemplirMetro().");
        return;
    }
    
    Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();
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
    double minLong = _stations.Values.Min(s => s.Longitude);
    double maxLong = _stations.Values.Max(s => s.Longitude);
    double minLat = _stations.Values.Min(s => s.Latitude);
    double maxLat = _stations.Values.Max(s => s.Latitude);

    foreach (var station in _stations.Values)
    {
        float x = 50 + (float)((station.Longitude - minLong) / (maxLong - minLong) * (width - 100));
        float y = height - 50 - (float)((station.Latitude - minLat) / (maxLat - minLat) * (height - 100));
        positions[station.Id] = new SKPoint(x, y);
    }
    using (var bitmap = new SKBitmap(width, height))
    using (var canvas = new SKCanvas(bitmap))
    {
        canvas.Clear(SKColors.White);
        var labeledLines = new HashSet<string>();
    using (var linePaint = new SKPaint { IsAntialias = true, StrokeWidth = 8 })
    using (var textPaint = new SKPaint 
    { 
        Color = SKColors.White, 
        TextSize = 24,
        IsAntialias = true,
        Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright),
        TextAlign = SKTextAlign.Center 
    })
    {
        
        var lineSegments = new Dictionary<string, List<SKPoint>>();
        foreach (var kvp in _dicoNoeuds)
        {
            foreach (Lien<T> lien in kvp.Value.Liens)
            {
                int idFrom = kvp.Key;
                int idTo = _dicoNoeuds.First(x => x.Value == lien.NoeudArrive).Key;

                if (!positions.ContainsKey(idFrom) || !positions.ContainsKey(idTo)) 
                    continue;

                var stationFrom = _stations[idFrom];
                var stationTo = _stations[idTo];
                var commonLine = stationFrom.Lignes.Intersect(stationTo.Lignes).FirstOrDefault();

                if (commonLine != null)
                {
                    if (!lineSegments.ContainsKey(commonLine))
                        lineSegments[commonLine] = new List<SKPoint>();

                    lineSegments[commonLine].Add(positions[idFrom]);
                    lineSegments[commonLine].Add(positions[idTo]);
                }
            }
        }
        foreach (var ligne in lineSegments)
        {
            string lineNumber = ligne.Key;
            var segments = ligne.Value;
            linePaint.Color = lineColors.GetValueOrDefault(lineNumber, SKColors.Gray);
            for (int i = 0; i < segments.Count; i += 2)
            {
                if (i + 1 < segments.Count)
                    canvas.DrawLine(segments[i], segments[i+1], linePaint);
            }
            if (segments.Count >= 2)
            {
                SKPoint debut = segments[0];
                SKPoint fin = segments[1];
                SKPoint milieu = new SKPoint((debut.X + fin.X) / 2, (debut.Y + fin.Y) / 2);
                using (var circlePaint = new SKPaint 
                { 
                    Color = linePaint.Color, 
                    Style = SKPaintStyle.Fill,
                    IsAntialias = true 
                })
                {
                    canvas.DrawCircle(milieu, 20, circlePaint);
                }
                using (var borderPaint = new SKPaint 
                { 
                    Color = SKColors.Black, 
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 2,
                    IsAntialias = true 
                })
                {
                    canvas.DrawCircle(milieu, 20, borderPaint);
                }
                canvas.DrawText(lineNumber, milieu.X, milieu.Y + 8, textPaint);
            }
        }
    }
        using (var stationPaint = new SKPaint { Color = SKColors.White, IsAntialias = true })
        using (var borderPaint = new SKPaint { Color = SKColors.Black, StrokeWidth = 3, IsAntialias = true, Style = SKPaintStyle.Stroke })
        {
            foreach (var pos in positions)
            {
                canvas.DrawCircle(pos.Value, 10, stationPaint);
                canvas.DrawCircle(pos.Value, 10, borderPaint);
            }
        }
        var mainStations = new List<string> { "Châtelet", "Gare du Nord", "Montparnasse", "Saint-Lazare", 
                                            "La Défense", "Bastille", "République", "Charles de Gaulle - Étoile" };
        using (var textPaint = new SKPaint 
        { 
            Color = SKColors.Black, 
            TextSize = 14, 
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
        })
        {
            foreach (var station in _stations.Values.Where(s => mainStations.Contains(s.Nom)))
            {
                if (positions.TryGetValue(station.Id, out var pos))
                {
                    canvas.DrawText(station.Nom, pos.X + 15, pos.Y, textPaint);
                }
            }
        }
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "plan_metro_parisien.png");

        using (var image = SKImage.FromBitmap(bitmap))
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        using (var stream = File.OpenWrite(filePath))
        {
            data.SaveTo(stream);
            Console.WriteLine($"Plan généré : {filePath}");
        }
    }
}
    public void AfficherStations()
    {
        foreach (var station in _stations.Values)
        {
            Console.WriteLine(
                $"Station {station.Nom} (ID: {station.Id}) | Lignes: {string.Join(", ", station.Lignes)}");
        }
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
            


            string idString = tokens[0].Trim();
            if (!int.TryParse(idString, out int id))
            {
                Console.WriteLine($"Erreur : ID invalide ({idString})");
                continue;
            }

           

            string nom = tokens[1].Trim(); ;
            if (!double.TryParse(tokens[9], NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude) ||
                !double.TryParse(tokens[10], NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude))
            {
                continue;
            }

            List<string> lignesMetro = tokens[7]
                .Split('/') 
                .Select(ligne => ligne.Trim()) 
                .ToList();
            stations.Add(new Station(id, nom, longitude, latitude, lignesMetro));

        }
        
        return stations;


    }
}