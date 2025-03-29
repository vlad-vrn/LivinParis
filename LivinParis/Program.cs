// See https://aka.ms/new-console-template for more information

using DBConnectLibrary;

UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
foreach (var Utilisateur in utilisateurDataAccess.getAllUtilisateurs())
{
    Console.WriteLine(Utilisateur.Id + " : " + Utilisateur.Nom);
}

foreach (var Utilisateur in utilisateurDataAccess.getUtilisateur(30))
{
    Console.WriteLine(Utilisateur.Prenom + " " + Utilisateur.Nom);
}

utilisateurDataAccess.delUtilisateur(31);
utilisateurDataAccess.delUtilisateur(32);

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