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
    

    public Station(int id, string nom)
    {
        this.id = id;
        this.nom = nom;
       

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


