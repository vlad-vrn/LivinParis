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
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public List<string> lignes { get; set; }
  
    

    public Station(int id, string nom, double longitude, double latitude, List<string> lignes = null)
    {
        this.id = id;
        this.nom = nom;
        this.lignes = lignes;
        this.Longitude = longitude;
        this.Latitude = latitude;
this.lignes = lignes ?? new List<string>();
    }
    

    public int Id
    {
        get { return id; }
    }

    public string Nom
    {
        get { return nom; }
    }

    public List<string>  Lignes
    {
        get { return lignes; }
    }
}


