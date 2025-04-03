using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Graph;

public class Station
{
    public int id;
    public string nom;
    public double[] lignes;
    

    public Station(int id, string nom, double[] lignes)
    {
        this.id = id;
        this.nom = nom;
        this.lignes = lignes;

    }

    public int Id
    {
        get { return id; }
    }

    public string Nom
    {
        get { return nom; }
    }

    public double[] Lignes
    {
        get { return lignes; }
    }
}


