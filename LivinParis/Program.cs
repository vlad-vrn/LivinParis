// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics.Tracing;
using DBConnectLibrary;
using Graph;
using LivinParis.Application;
using LivinParis.Navigation;
using Spectre.Console;

UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
ClientDataAccess clientDataAccess = new ClientDataAccess();
CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
Login login = new Login();
MenuPrincipal mainMenu = new MenuPrincipal();
mainMenu.initialStartup();
while (mainMenu.output != "Quitter l'application")
{
    while (mainMenu.output == "Liv'In Paris")
    {
        mainMenu.menuLivinParis();
    }

    while (mainMenu.output == "Rendu 1")
    {
        Console.WriteLine("Pas dispo pour l'instant");
        mainMenu.output = "Retour";
        Console.ReadKey();
    }

    while (mainMenu.output == "Modélisation du métro")
    {
        Console.WriteLine("Pas dispo pour l'instant");
        mainMenu.output = "Retour";
        Console.ReadKey();
    }
    
    mainMenu.initialStartup();
}

/*
CreateAcc.CreerCompteUser();
//CreateAcc.CreerCompteUser();

//+ (clientUserID) => clientUserID.Contains(Utilisateur.ID) ? "Client" : "" 
+ (cuisinierUserID) => cuisinierUserID.Contains(Utilisateur.ID) ? "Cuisinier" : ""

foreach (var Utilisateur in utilisateurDataAccess.getAllUtilisateurs())
{
    Console.WriteLine(Utilisateur.Id + " " + Utilisateur.Nom);
}


Utilisateur thisUser = new Utilisateur();

try
{
    thisUser = login.userLogin();
}
//Catch ne marche pas
catch (Exception ex)
{
    Console.WriteLine("Cette adresse mail n'est pas renseignée : veuillez réessayer TRYCATCH.");
}


Console.WriteLine(thisUser.Id + " " + thisUser.Nom);

UpdateUser.becomeClient(thisUser);
UpdateUser.becomeCuisinier(thisUser);

foreach (var Cuisinier in cuisinierDataAccess.getAllCuisiniers())
{
    Console.WriteLine(Cuisinier.ID_Cuisinier + " " + Cuisinier.ID_Utilisateur);
}

foreach (var Client in clientDataAccess.getAllClients())
{
    Console.WriteLine(Client.ID_Client + " " + Client.ID_Utilisateur);
}

*/
