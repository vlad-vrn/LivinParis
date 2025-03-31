using DBConnectLibrary;
using LivinParis.Application;
using Spectre.Console;

namespace LivinParis.Navigation;

public class MenuPrincipal
{
    UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
    Login login = new Login();
    public string initialStartup()
    {
        AnsiConsole.Markup(("Menu principal\n"));
        var rep = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Selection")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Rendu 1", "Modélisation du métro", "Liv'In Paris"
                }));
        return rep;
    }

    public void menuLivinParis()
    {
        Utilisateur thisUser = new Utilisateur();
        AnsiConsole.Markup(("Bienvenue sur Liv'In Paris !\n"));
        var rep = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Que voulez-vous faire ?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Connexion", "Inscription", "Liste des utilisateurs"
                }));
        switch (rep)
        {
            case "Connexion":
                thisUser = login.userLogin();
                break;
            case "Inscription":
                CreateAcc.CreerCompteUser();
                break;
            case "Liste des utilisateurs":
                foreach (var Utilisateur in utilisateurDataAccess.getAllUtilisateurs())
                {
                    AnsiConsole.WriteLine(Utilisateur.Prenom + " " + Utilisateur.Nom);
                }
                break;
        }
    }
}