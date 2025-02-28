namespace Graph;
using System.IO;
public class Graphe<T>
{
    public required string Titre;
    private List<Noeud<T>>? _noeuds;
    private List<Lien<T>>? _liens;

    public Graphe(string titre, List<Noeud<T>>? noeuds = null, List<Lien<T>>? liens = null)
    {
        this.Titre = titre;
        this._noeuds = noeuds;
        this._liens = liens;
    }

    
    public void RemplirGraphe()
    {
        string lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(23).Take(1).First();
        string[] tokens = lines.Split(" ");
        int nbNoeuds = Int32.Parse(tokens[0]);
        for (int i = 0; i < nbNoeuds; i++)
        {
            Noeud<T> noeud = new Noeud<T>((i+1).ToString()) { Titre = (i+1).ToString() }; //Répétition : pertinence du required ?
            _noeuds.Add(noeud);
        }
    }
    
    
//    public void LiensGraphe()
//    {
//        string[] lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(24).ToArray();
//        foreach (string line in lines)
//        {
//            string[] tokens = line.Split(" ");
//            this._liens.noeudDepart
//        }
//    }
}