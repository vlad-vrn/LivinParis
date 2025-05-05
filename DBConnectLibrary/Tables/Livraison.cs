using System;

namespace DBConnectLibrary;

public class Livraison
{
    public int ID_Livraison { get; set; }
    public int station_client { get; set; }
    public int station_cuisinier { get; set; }
    public DateTime Date_Livraison { get; set; }
    public bool est_livre { get; set; }
    public int ID_Commande { get; set; }
    public int ID_Client { get; set; }
}