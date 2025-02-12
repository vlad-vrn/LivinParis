namespace Graph;

public class Lien<T>
{
    private Noeud<T> _noeudDepart;
    private Noeud<T> _noeudArrive;

    public Lien(Noeud<T> noeudDepart, Noeud<T> noeudArrive)
    {
        this._noeudDepart = noeudDepart;
        this._noeudArrive = noeudArrive;
    }
}