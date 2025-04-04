using DBConnectLibrary;
using Spectre.Console;

namespace LivinParis.Modules;

public class ModuleCuisinier : GlobalDataAccess
{
    public void moduleCuisinier()
    {
        List<Utilisateur> userCuisi = new List<Utilisateur>();
        foreach (Cuisinier cuisinier in cuisinierDataAccess.getAllCuisiniers())
        {
            userCuisi.Add(utilisateurDataAccess.getUtilisateur(cuisinier.ID_Utilisateur));
        }
        Dictionary<string, Utilisateur> userNames = new Dictionary<string, Utilisateur>();
        foreach (Utilisateur utilisateur in userCuisi)
        {
            userNames.Add(utilisateur.Nom, utilisateur);
        }
        var outputing = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Sur quel cuisinier voulez vous avoir des infos ?")
                .PageSize(10)
                .AddChoices(userNames.Keys));
        
        Utilisateur thisUser = userNames[outputing];
        Cuisinier thisCuisi = cuisinierDataAccess.getCuisiFromUserID(thisUser.Id);
        
        var info = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Que voulez vous savoir ?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Plat du jour du cuisinier", "Les plats du cuisinier", "Retour"
                }));

        switch (info)
        {
            case "Plat du jour du cuisinier":
                AnsiConsole.Markup("Voici le plat du jour proposé par " + thisUser.Nom + ":\n");
                foreach (Plat platDuJour in platDataAccess.getPlatDuJour(thisCuisi))
                {
                    Console.WriteLine(platDuJour.Nom);
                }
                Console.ReadKey();
                break;
            case "Les plats du cuisinier":
                foreach (Plat plats in platDataAccess.getAllPlatFromCuisi(thisCuisi.ID_Cuisinier))
                {
                    Console.WriteLine(plats.Nom);
                }
                Console.ReadKey();
                break;
            case "Retour":
                break;
        }
        
    }
}