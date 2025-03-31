using DBConnectLibrary;

namespace LivinParis.RecetteManagement;

public class CreateRecette
{
    RecetteDataAccess recetteDataAccess = new RecetteDataAccess();
    List<string> recettesList = new List<string>();
    
    
    public void CreerRecette()
    {
        Recette newRecette = new Recette();
        foreach (Recette registeredRecette in recetteDataAccess.getAllRecettes())
        {
            recettesList.Add(registeredRecette.Nom_Recette.ToUpper());
        }
        
        Console.WriteLine("Entrez le nom de la recette :");
        string nomRecette = Console.ReadLine();
        newRecette.Nom_Recette = nomRecette;
        
        
        Console.WriteLine("Recette ajoutée !");
        
        
        
    }
}