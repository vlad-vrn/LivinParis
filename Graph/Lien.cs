namespace Graph;

public class Lien<T>
{
    private Noeud<T> _noeudDepart;
    private Noeud<T> _noeudArrive;
    public int _poids;

    public Lien(Noeud<T> noeudDepart, Noeud<T> noeudArrive,int poids)
    {
        this._noeudDepart = noeudDepart;
        this._noeudArrive = noeudArrive;
        this._poids = poids;
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
    public int Poids
    {
        get => _poids;
        set => _poids = value;
    }
}