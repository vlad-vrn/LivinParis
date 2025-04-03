// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics.Tracing;
using DBConnectLibrary;
using Spectre.Console;


AnsiConsole.Write(new FigletText(FigletFont.Default, "Livin Paris").Color(Color.Blue));
Console.ReadKey();
/*var fruit = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("What's your [green]favorite fruit[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
        .AddChoices(new[] {
            "Apple", "Apricot", "Avocado", 
            "Banana", "Blackcurrant", "Blueberry",
            "Cherry", "Cloudberry", "Cocunut",
        }));

// Echo the fruit back to the terminal
AnsiConsole.WriteLine($"I agree. {fruit} is tasty!");
*/
/*
UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
PlatDataAccess platDataAccess = new PlatDataAccess();
CommandeDataAccess commandeDataAccess = new CommandeDataAccess();
ClientDataAccess clientDataAccess = new ClientDataAccess();

///Test UtilisateurDataAccess
Utilisateur user1 = new Utilisateur();
user1.Nom = "Varenne-Welnovski";
user1.Prenom = "Vladimir";
user1.Telephone = 0768799734;
user1.Mail = "varenne.vw@gmail.com";
user1.Rue = "Poulet";
user1.NumeroRue = "33";
user1.Ville = "Paris";
user1.CodePostal = 75018;
user1.StationProche = "Château Rouge";

utilisateurDataAccess.addUtilisateur(user1);

foreach (var Utilisateur in utilisateurDataAccess.getAllUtilisateurs())
{
    Console.WriteLine(Utilisateur.Id + " : " + Utilisateur.Nom);
}

Console.WriteLine("*******************************");

///Test PlatDataAccess
foreach (var Plat in platDataAccess.getAllPlats())
{
    Console.WriteLine(Plat.ID_Plat + " : " + Plat.Nom + " " + Plat.RegimeAlimentaire);
}

Console.WriteLine("*******************************");

///Test CommandeDataAccess
foreach (var Commande in commandeDataAccess.GetAllCommandes())
{
    Console.WriteLine(Commande.ID_Commande + " " + Commande.ID_Client + " " + Commande.Prix_Commande);
}

Console.WriteLine("*******************************");

///Test ClientDataAccess
foreach (var Client in clientDataAccess.getAllClients())
{
    Console.WriteLine(Client.ID_Client + " " + Client.ID_Utilisateur);
}

Console.WriteLine("*******************************");*/