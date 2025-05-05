using System;
using System.Collections.Generic;
using DBConnectLibrary;
using LivinParis.Application;
using LivinParis.Modules;
using Spectre.Console;
using Graph;

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
                    "Modélisation du métro", "Liv'In Paris", "Quitter l'application"
                }));
        this.output = rep;
    }

    public void menuMetro()
    {
        Fonctions fonction = new Fonctions();
        Console.WriteLine("Chargement des stations...");

        List<Station> stations = Graphe<string>.ChargerStations();

        Graphe<string> g1 = new Graphe<string>("g1") { Titre = "MetroParis" };

        g1.RemplirMetro();
        g1.LiensMetro();
        while (this.output != "Retour")
        {
            var rep = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Que voulez-vous faire ?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Voir les stations", "Voir un itinéraire", "Voir la coloration", "Afficher liste d'adjacence", "Afficher matrice d'adjacence", "Dessiner graphe", "Retour"
                    }));
            switch (rep)
            {
                case "Voir les stations":
                    fonction.voirStations(g1, stations);
                    Console.ReadKey();
                    break;
                case "Voir un itinéraire":
                    fonction.voirItineraire(g1, stations);
                    Console.ReadKey();
                    break;
                case "Voir la coloration":
                    fonction.voirColoration(g1, stations);
                    Console.ReadKey();
                    break;
                case "Afficher liste d'adjacence":
                    fonction.afficherListeAdjacence(g1, stations);
                    Console.ReadKey();
                    break;
                case "Afficher matrice d'adjacence":
                    fonction.voirMatriceAdjacence(g1, stations);
                    Console.ReadKey();
                    break;
                case "Dessiner graphe":
                    fonction.dessinerGraphe(g1, stations);
                    Console.ReadKey();
                    break;
                case "Retour":
                    this.output = "Retour";
                    break;
            }

            Console.Clear();
        }
    }
    public void menuLivinParis()
    {
        List<int> cuisinierUserID = new List<int>();
        foreach (Cuisinier cuisinier in this.cuisinierDataAccess.getAllCuisiniers())
        {
            cuisinierUserID.Add(cuisinier.ID_Utilisateur);
        }
        List<int> clientUserID = new List<int>();
        foreach (Client client in this.clientDataAccess.getAllClients()) 
        {
            clientUserID.Add(client.ID_Utilisateur);
        }
        ModuleClient moduleClient = new ModuleClient();
        ModuleCuisinier moduleCuisinier = new ModuleCuisinier();
        Console.Clear();
        Utilisateur thisUser = new Utilisateur();
        AnsiConsole.Markup(("Bienvenue sur Liv'In Paris !\n"));
        var rep = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Que voulez-vous faire ?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Connexion", "Inscription", "Liste des utilisateurs", "Accès aux modules", "Retour"
                }));
        switch (rep)
        {
            case "Connexion":
                thisUser = login.userLogin(); 
                if (thisUser != null)
                {
                    userMenu.placement = "userMenu";
                    while (userMenu.placement != "loginMenu")
                    {
                        userMenu.espaceUtilisateur(thisUser);
                    }
                } 
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
                    string str3(List<int> listCuisiID) => listCuisiID.Contains(thisUtilisateur.Id) ? "Cuisinier" : ""; 
                    
                    Console.WriteLine(str1 + " : " + str2(clientUserID) + " " +  str3(cuisinierUserID));
                }
                Console.ReadKey();
                break;
            
            case "Accès aux modules":
                var outputing = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Que voulez-vous faire ?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Module Client", "Module Cuisinier", "Module Commande", "Module Statistiques", "Retour"
                        }));
                switch (outputing)
                {
                    case "Module Client":
                        moduleClient.moduleClient();
                        break;
                    case "Module Cuisinier":
                        moduleCuisinier.moduleCuisinier();
                        Console.ReadKey();
                        break;
                    case "Module Commande":
                        break;
                    case "Module Statistiques":
                        break;
                    case "Retour":
                        break;
                }
                break;
            
            case "Retour":
                this.output = "Retour";
                break;
        }
    }
}