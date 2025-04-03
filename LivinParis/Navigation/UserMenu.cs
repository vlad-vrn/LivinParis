using System;
using System.Collections.Generic;
using System.Diagnostics;
using LivinParis.Application;
using LivinParis.PlatManagement;
using LivinParis.RecetteManagement;

namespace LivinParis.Navigation;
using DBConnectLibrary;
using Spectre.Console;

public class UserMenu
{
    public string placement { get; set; }
    MenuPrincipal menuPrincipal { get; set; }
    UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
    CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
    ClientDataAccess clientDataAccess = new ClientDataAccess();
    public void espaceUtilisateur(Utilisateur thisUser)
    {
        Console.Clear();
        UserMenu userMenu = new UserMenu();
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
            case "Accéder à votre espace cuisinier":
                userMenu.espaceCuisi(thisUser);
                break;
            case "Accéder à votre espace client":
                userMenu.espaceClient(thisUser);
                break;
            case "Retour":
                this.placement = "loginMenu"; //Inutile pr l'instant mais améliore la clarté de la navigation
                break;                      //Peut être dégager les switch case et plutôt en boucle while(placement != Retour), if(placement =....)) pr tout le temps revenir au choix sans reload
        }
    }
    public void espaceCuisi(Utilisateur thisUser)
    {
        CreatePlat createPlat = new CreatePlat();
        CreateRecette createRecette = new CreateRecette();
        string rep;
        Console.Clear();
        AnsiConsole.Markup("Bienvenue sur votre espace cuisinier, " + thisUser.Prenom + ".\n");
        rep = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Que voulez vous faire ?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Ajouter un plat", "Ajouter une recette", "Voir mes plats en ligne", "Historique des commandes", "Retour"
                }));
        switch (rep)
        {
            case "Ajouter un plat":
                createPlat.publierPlat();
                break;
            case "Ajouter une recette":
                createRecette.CreerRecette();
                break;
            case "Voir mes plats en ligne":
                AnsiConsole.Markup("plat en ligne...");
                break;
            case "Historique des commandes":
                AnsiConsole.Markup("Historique des commandes...");
                break;
            case "Retour":
                Console.WriteLine("Vous voulez partir");
                break;
        }
    }
    public void espaceClient(Utilisateur thisUser)
    {
        string rep;
        Console.Clear();
        AnsiConsole.Markup("Bienvenue sur votre espace client, " + thisUser.Prenom + ".\n");
        rep = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Que voulez vous faire ?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Commander un plat", "Voir mes commandes", "Historique des commandes", "Retour"
                    //Il sera possible de noter les cuisiniers dans "Historique des commandes"
                }));
        switch (rep)
        {
            case "Commander un plat":
                
                break;
            case "Voir mes commandes":
                break;
            case "Historique des commandes":
                break;
            case "Retour":
                Console.WriteLine("Vous voulez partir");
                break;
        }
    }
}