using System;

namespace DBConnectLibrary;

public class Commande
{
    public int ID_Commande { get; set; }
    public decimal Prix_Commande { get; set; }
    public int Nombre_Portion { get; set; }
    public DateTime Date_Heure_Livraison { get; set; }
    public int ID_Client { get; set; }
    public int ID_Cuisinier { get; set; }
}