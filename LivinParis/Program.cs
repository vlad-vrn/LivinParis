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

    while (mainMenu.output == "Modélisation du métro")
    {
        mainMenu.output = "Retour";
        Console.ReadKey();
    }
    
    mainMenu.initialStartup();
}

