// See https://aka.ms/new-console-template for more information

using DBConnectLibrary;

UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
foreach (var Utilisateur in utilisateurDataAccess.getAllUtilisateurs())
{
    Console.WriteLine(Utilisateur.Id + " : " + Utilisateur.Nom);
}