namespace Graph;

using System;
using System.Collections.Generic;
using System.Linq;

public class Coloration<T>
{
    private Graphe<T> _graphe;
    private Dictionary<int, int> _colorationNoeuds; // ID noeud -> couleur
    private int _nombreCouleurs;
    private Dictionary<int, List<int>> _groupesParCouleur; // Couleur -> liste d'IDs de noeuds

    public Coloration(Graphe<T> graphe)
    {
        _graphe = graphe;
        _colorationNoeuds = new Dictionary<int, int>();
        _groupesParCouleur = new Dictionary<int, List<int>>();
        _nombreCouleurs = 0;
    }

    /// <summary>
    /// Applique l'algorithme de Welsh-Powell pour colorier le graphe
    /// </summary>
    public void AppliquerWelshPowell()
    {
        // 1. Trier les noeuds par degré décroissant
        var noeudsTries = _graphe.Noeuds
            .OrderByDescending(n => n.Value.Liens.Count)
            .Select(n => n.Key)
            .ToList();

        // 2. Initialisation
        _colorationNoeuds.Clear();
        _groupesParCouleur.Clear();
        _nombreCouleurs = 0;

        // 3. Coloration
        foreach (var idNoeud in noeudsTries)
        {
            if (_colorationNoeuds.ContainsKey(idNoeud)) continue;

            // Trouver la plus petite couleur disponible
            int couleurDisponible = TrouverCouleurDisponible(idNoeud);

            // Assigner la couleur
            _colorationNoeuds[idNoeud] = couleurDisponible;
            
            // Mettre à jour les groupes
            if (!_groupesParCouleur.ContainsKey(couleurDisponible))
                _groupesParCouleur[couleurDisponible] = new List<int>();
            
            _groupesParCouleur[couleurDisponible].Add(idNoeud);

            // Mettre à jour le nombre de couleurs si nécessaire
            if (couleurDisponible + 1 > _nombreCouleurs)
                _nombreCouleurs = couleurDisponible + 1;
        }
    }

    private int TrouverCouleurDisponible(int idNoeud)
    {
        var noeud = _graphe.Noeuds[idNoeud];
        var couleursVoisins = new HashSet<int>();

        // Récupérer les couleurs des voisins
        foreach (var lien in noeud.Liens)
        {
            var idVoisin = _graphe.Noeuds.First(n => n.Value == lien.NoeudArrive).Key;
            if (_colorationNoeuds.TryGetValue(idVoisin, out int couleur))
                couleursVoisins.Add(couleur);
        }

        // Trouver la plus petite couleur non utilisée par les voisins
        for (int couleur = 0; couleur <= _nombreCouleurs; couleur++)
        {
            if (!couleursVoisins.Contains(couleur))
                return couleur;
        }

        return _nombreCouleurs; // Nouvelle couleur
    }

    /// <summary>
    /// Retourne le nombre minimal de couleurs utilisées
    /// </summary>
    public int NombreMinimalCouleurs => _nombreCouleurs;

    /// <summary>
    /// Détermine si le graphe est biparti
    /// </summary>
    public bool EstBiparti()
    {
        return _nombreCouleurs <= 2;
    }

    /// <summary>
    /// Vérifie si le graphe pourrait être planaire selon le théorème des 4 couleurs
    /// (Note: condition nécessaire mais non suffisante)
    /// </summary>
    public bool EstPlanaire()
    {
        return _nombreCouleurs <= 4;
    }

    /// <summary>
    /// Retourne les groupes indépendants (noeuds de même couleur)
    /// </summary>
    public Dictionary<int, List<int>> GetGroupesIndependants()
    {
        return _groupesParCouleur;
    }

    /// <summary>
    /// Affiche les résultats de la coloration
    /// </summary>
    public void AfficherResultats()
    {
        Console.WriteLine("=== Résultats de la Coloration ===");
        Console.WriteLine($"Nombre minimal de couleurs nécessaires: {_nombreCouleurs}");


        
    }

    /// <summary>
    /// Retourne la coloration d'un noeud spécifique
    /// </summary>
    public int GetCouleurNoeud(int idNoeud)
    {
        return _colorationNoeuds.TryGetValue(idNoeud, out int couleur) ? couleur : -1;
    }

    /// <summary>
    /// Retourne la coloration complète du graphe
    /// </summary>
    public Dictionary<int, int> GetColorationComplete()
    {
        return new Dictionary<int, int>(_colorationNoeuds);
    }
}