using DBConnectLibrary;
using Spectre.Console;

namespace LivinParis.RecetteManagement;

public class CreateRecette
{
    RecetteDataAccess recetteDataAccess = new RecetteDataAccess();
    List<string> recettesList = new List<string>();
    IngredientDataAccess ingredientDataAccess = new IngredientDataAccess();
    ContientDataAccess contientDataAccess = new ContientDataAccess();
    
    public void CreerRecette()
    {
        Recette newRecette = new Recette();
        foreach (Recette registeredRecette in recetteDataAccess.getAllRecettes())
        {
            recettesList.Add(registeredRecette.Nom_Recette.ToUpper());
        }
        
        List<string> ingredientList = new List<string>();
        foreach (Ingredient registeredIngredient in ingredientDataAccess.getAllIngredients())
        {
            ingredientList.Add(registeredIngredient.IngredientNom);
        }
        
        Console.WriteLine("Entrez le nom de la recette :\n");
        string nomRecette = Console.ReadLine();
        while(recettesList.Contains(nomRecette.ToUpper()))
        {
            AnsiConsole.Markup("Cette recette existe déjà, veuillez entrer un autre nom\n");
            nomRecette = Console.ReadLine();
        }
        newRecette.Nom_Recette = nomRecette;

        List<string> ingredientUtilise = new List<string>();
        string question = "Oui";
        while (question == "Oui")
        {
            var ingredient  = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Que voulez-vous faire ?")
                    .PageSize(10)
                    .AddChoices(ingredientList));
            ingredientUtilise.Add(ingredient);
            
            question  = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Un autre ingrédient ?\n")
                    .PageSize(10)
                    .AddChoices(new []
                    {
                        "Oui", "Non"
                    }));
        }
        
        AnsiConsole.Markup("Entrez le régime alimentaire de cette recette :\n");
        string regime = Console.ReadLine();
        newRecette.RegimeAlimentaire = regime;
        
        recetteDataAccess.addRecette(newRecette);
        
        foreach (var ingredient in ingredientUtilise)
        {
            Contient newContient = new Contient();
            newContient.ID_Recette = newRecette.ID_Recette;
            newContient.Nom_Ingredient = ingredient;
            Console.WriteLine("?????");
            contientDataAccess.addContient(newContient);
        }
        
        Console.WriteLine("Recette ajoutée !");
    }
}