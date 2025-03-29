namespace DBConnectLibrary;

public class Plat
{
    public int ID_Plat {get;set;}
    public string Nom {get;set;}
    public int Quantite {get;set;}
    public decimal Prix {get;set;}
    public string RegimeAlimentaire {get;set;}
    public DateTime Date_Fabrication {get;set;}
    public DateTime Date_Peremption {get;set;}
    public int NombrePortion {get;set;}
    public bool PlatDuJour {get;set;}
    public int ID_Recette {get;set;}
    public int ID_Cuisinier {get;set;}
}