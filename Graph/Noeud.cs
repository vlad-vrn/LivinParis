using System.Collections.Generic;

namespace Graph;

public class Noeud<T>
{
    public required string Titre;
    public int id {get;set;}
    public T _contenu; 
    public List<Lien<T>> Liens { get; set; } = new List<Lien<T>>();
    public object Data { get; set; }

    public Noeud(string titre, T contenu = default!, List<Lien<T>>? liens = null)
    {
        this.Titre = titre;
        this._contenu = contenu;
        this.Liens = liens ?? new List<Lien<T>>();
    }

    public bool isLinked(Noeud<T> noeud1)
    {
        foreach (Lien<T> lien in this.Liens)
        {
            if (noeud1 != lien.NoeudDepart)
            {
                if (lien.NoeudDepart == noeud1 || lien.NoeudArrive == noeud1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public string AfficherLiens()
    {
        string str = "Les liens du noeud " + this.Titre + " sont : ";
        foreach (Lien<T> lien in Liens)
        {
            Noeud<T> nD = lien.NoeudDepart;
            Noeud<T> nA = lien.NoeudArrive;
            if (nD.Titre == this.Titre)
            {
                str += nA.Titre;
            }
            else
            {
                str += nD.Titre;
            }
            str += ", ";
        }

        return str.Remove(str.Length - 2);
    }
}
