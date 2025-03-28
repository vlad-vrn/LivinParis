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

    public Noeud<T> NoeudDepart
    {
        get => _noeudDepart;
        set => _noeudDepart = value;
    }

    public Noeud<T> NoeudArrive
    {
        get => _noeudArrive;
        set => _noeudArrive = value;
    }
}