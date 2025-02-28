namespace Graph;

public class Noeud<T>
{
    public required string Titre;
    private T _contenu;
    private List<Lien<T>> _liens;

    public Noeud(string titre, T contenu = default!, List<Lien<T>>? liens = null)
    {
        this.Titre = titre;
        this._contenu = contenu;
        this._liens = liens;
    }
}
