using DBConnectLibrary;
using LivinParis.Application;
using Spectre.Console;

namespace LivinParis.Navigation;

public class MenuPrincipal
{
    public string output { get; set; }
    UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
    CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
    ClientDataAccess clientDataAccess = new ClientDataAccess();
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
        List<int> cuisinierUserID = new List<int>();
        foreach (Cuisinier cuisinier in this.cuisinierDataAccess.getAllCuisiniers())
        {
            cuisinierUserID.Add(cuisinier.ID_Utilisateur);
        }
        List<int> clientUserID = new List<int>();
        foreach (Client client in this.clientDataAccess.getAllClients()) //Recharge de la liste à chaque itération, opti possible.
        {
            clientUserID.Add(client.ID_Utilisateur);
        }
        
        Console.Clear();
        Utilisateur thisUser = new Utilisateur();
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
                espaceUtilisateur(thisUser);
                Console.ReadKey();
                break;
            case "Inscription":
                CreateAcc.CreerCompteUser();
                Console.ReadKey();
                break;
            case "Liste des utilisateurs":
                foreach (var thisUtilisateur in utilisateurDataAccess.getAllUtilisateurs())
                {
                    string str1 = thisUtilisateur.Prenom + " " + thisUtilisateur.Nom;
                    string str2(List<int> listClientID) => listClientID.Contains(thisUtilisateur.Id) ? "Client" : "";
                    string str3(List<int> listCuisiID) => listCuisiID.Contains(thisUtilisateur.Id) ? "Cuisinier" : ""; //trans en null et ??- (une autre nuit)
                    
                    Console.WriteLine(str1 + " : " + str2(clientUserID) + " " +  str3(cuisinierUserID));
                }
                Console.ReadKey();
                break;
            case "Retour":
                this.output = "Retour";
                break;
        }
    }

    public void espaceUtilisateur(Utilisateur thisUser)
    {
        Console.Clear();
        List<int> cuisinierUserID = new List<int>();
        foreach (Cuisinier cuisinier in this.cuisinierDataAccess.getAllCuisiniers())
        {
            cuisinierUserID.Add(cuisinier.ID_Utilisateur);
        }
        List<int> clientUserID = new List<int>();
        foreach (Client client in this.clientDataAccess.getAllClients()) //Recharge de la liste à chaque itération, opti possible.
        {
            clientUserID.Add(client.ID_Utilisateur);
        }

        string rep;

        if (cuisinierUserID.Contains(thisUser.Id) == false && clientUserID.Contains(thisUser.Id) == false)
        {
            AnsiConsole.Markup("Bienvenue sur votre espace utilisateur, " + thisUser.Prenom + " !\n");
            rep = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Que voulez vous faire ?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Devenir client", "Devenir cuisinier", "Retour"
                    }));
        }
        else if (cuisinierUserID.Contains(thisUser.Id) == false && clientUserID.Contains(thisUser.Id) == true)
        {
            AnsiConsole.Markup("Bienvenue sur votre espace utilisateur, " + thisUser.Prenom + " !\n");
            rep = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Que voulez vous faire ?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Accéder à votre espace client", "Devenir cuisinier", "Retour"
                    }));
        }
        else if (cuisinierUserID.Contains(thisUser.Id) == true && clientUserID.Contains(thisUser.Id) == false)
        {
            AnsiConsole.Markup("Bienvenue sur votre espace utilisateur, " + thisUser.Prenom + " !\n");
            rep = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Que voulez vous faire ?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Devenir client", "Accéder à votre espace cuisinier", "Retour"
                    }));
        }
        else
        {
            AnsiConsole.Markup("Bienvenue sur votre espace utilisateur, " + thisUser.Prenom + " !\n");
            rep = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Que voulez vous faire ?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Accéder à votre espace client", "Accéder à votre espace cuisinier", "Retour"
                    }));
        }

        switch (rep)
        {
            case "Devenir client":
                UpdateUser.becomeClient(thisUser);
                Console.ReadKey();
                break;
            case "Devenir cuisinier":
                UpdateUser.becomeCuisinier(thisUser);
                Console.ReadKey();
                break;
            case "Retour":
                this.output = "Retour";
                break;
        }
        
    }

    public void espaceCuisi()
    {
        Console.Clear();
        
    }
}