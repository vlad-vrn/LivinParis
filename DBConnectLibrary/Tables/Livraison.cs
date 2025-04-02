namespace DBConnectLibrary;

public class Livraison
{
    public int ID_Livraison { get; set; }
    public string adresse_client { get; set; }
    public string adresse_cuisinier { get; set; }
    public DateTime Date_Livraison { get; set; }
    public int ID_Commande { get; set; }
}