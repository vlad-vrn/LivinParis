using DBConnectLibrary;
using LivinParis.Application;
using Spectre.Console;

namespace LivinParis.Navigation;

public class MenuPrincipal
{
    public string output { get; set; }
    UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
    CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
    Login login = new Login();
    public void initialStartup()
    {
        Console.Clear();
        AnsiConsole.Markup(("Menu principal\n"));
        var rep = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Selection")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Rendu 1", "Modélisation du métro", "Liv'In Paris", "Quitter l'application"
                }));
        this.output = rep;
    }

    public void menuLivinParis()
    {
        Console.Clear();
        Utilisateur thisUser = new Utilisateur();
        List<int> cuisinierUserID = new List<int>();
        foreach (Cuisinier cuisinier in this.cuisinierDataAccess.getAllCuisiniers())
        {
            cuisinierUserID.Add(cuisinier.ID_Utilisateur);
        }
        AnsiConsole.Markup(("Bienvenue sur Liv'In Paris !\n"));
        var rep = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Que voulez-vous faire ?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Connexion", "Inscription", "Liste des utilisateurs", "Retour"
                }));
        switch (rep)
        {
            case "Connexion":
                thisUser = login.userLogin();
                if (cuisinierUserID.Contains(thisUser.Id))
                {
                    Console.WriteLine("Bienvnue sur votre espace cuisinier.");
                }
                Console.ReadKey();
                break;
            case "Inscription":
                CreateAcc.CreerCompteUser();
                Console.ReadKey();
                break;
            case "Liste des utilisateurs":
                foreach (var Utilisateur in utilisateurDataAccess.getAllUtilisateurs())
                {
                    AnsiConsole.WriteLine(Utilisateur.Prenom + " " + Utilisateur.Nom);
                }
                Console.ReadKey();
                break;
            case "Retour":
                this.output = "Retour";
                break;
        }
    }

    public void espaceCuisinier()
    {
        Console.Clear();
    }
}