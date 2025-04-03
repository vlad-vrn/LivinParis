using DBConnectLibrary;
using Spectre.Console;

namespace LivinParis.PlatManagement;

public class CreatePlat : GlobalDataAccess
{
    public void publierPlat()
    {
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
        
    }
}