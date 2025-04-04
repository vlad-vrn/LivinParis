using DBConnectLibrary;
using Spectre.Console;

namespace LivinParis.PlatManagement;

public class CreatePlat : GlobalDataAccess
{
    public void publierPlat(int idCuisinier)
    {
        Plat newPlat = new Plat();
        List<string> nomRecettes = new List<string>();
        foreach (Recette registeredRecette in recetteDataAccess.getAllRecettes())
        {
            nomRecettes.Add(registeredRecette.Nom_Recette);
        }
        var choix  = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choix du plat :")
                .PageSize(10)
                .AddChoices(nomRecettes));
     
        newPlat.Nom = choix;
        
        AnsiConsole.Markup("Combien de plats voulez vous mettre en ligne ?\n");
        newPlat.Quantite = Convert.ToInt32(Console.ReadLine());

        AnsiConsole.Markup("Entrez le nombre de portions\n");
        newPlat.NombrePortion = Convert.ToInt32(Console.ReadLine());

        AnsiConsole.Markup("Quel est le prix d'une portion ?\n");
        newPlat.Prix = Convert.ToInt32(Console.ReadLine());
        
        newPlat.ID_Cuisinier = idCuisinier;
        
        newPlat.ID_Recette = recetteDataAccess.getIDFromName(choix);
        var jour   = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Est-ce le plat du jour ?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Oui", "Non"
                }));
        if (jour == "Oui")
        {
            newPlat.PlatDuJour = true;
        }
        else
        {
            newPlat.PlatDuJour = false;
        }
        newPlat.Date_Fabrication = DateTime.Today;
        newPlat.Date_Peremption = DateTime.Today;
        
        platDataAccess.addPlat(newPlat);
        
    }
}