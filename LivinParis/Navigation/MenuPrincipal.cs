using System;
using System.Collections.Generic;
using DBConnectLibrary;
using LivinParis.Application;
using Spectre.Console;

namespace LivinParis.Navigation;

public class MenuPrincipal
{
    public string output { get; set; }
    public string placement { get;set; }
    UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
    CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
    ClientDataAccess clientDataAccess = new ClientDataAccess();
    Login login = new Login();
    UserMenu userMenu = new UserMenu();
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
                thisUser = login.userLogin(); //faut check si c'est bien un user
                userMenu.placement = "userMenu";
                while (userMenu.placement != "loginMenu")
                {
                    userMenu.espaceUtilisateur(thisUser); //tout se passe ici en ft...
                }
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
}