// See https://aka.ms/new-console-template for more information

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
string output = mainMenu.initialStartup();
switch (output)
{
    case "Liv'In Paris":
        while (true)
        {
            mainMenu.menuLivinParis();
        }
        break;
}

/*
CreateAcc.CreerCompteUser();
//CreateAcc.CreerCompteUser();


foreach (var Utilisateur in utilisateurDataAccess.getAllUtilisateurs())
{
    Console.WriteLine(Utilisateur.Id + " " + Utilisateur.Nom);
}


Utilisateur thisUser = new Utilisateur();

try
{
    thisUser = login.userLogin();
}
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
