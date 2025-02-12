namespace Graph;

public class Graphe<T>
{
    public required string Titre;
    private List<Noeud<T>>? _noeuds;
    private List<Lien<T>>? _liens;

    public Graphe(string titre, List<Noeud<T>>? noeuds, List<Lien<T>>? liens)
    {
        this.Titre = titre;
        this._noeuds = noeuds;
        this._liens = liens;
    }
}