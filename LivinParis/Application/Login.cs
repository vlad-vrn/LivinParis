using DBConnectLibrary;

namespace LivinParis.Application;

public class Login
{
    public int Session { get; set; }
    
    public Utilisateur userLogin()
    {
        UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
        Utilisateur user = new Utilisateur();
        Console.WriteLine("Connection : ");
        Console.WriteLine("Veuillez entrer votre adresse mail : ");
        string mail = Console.ReadLine();
        if (utilisateurDataAccess.existingEmail(mail) == false)
        {
            Console.WriteLine("Cette adresse mail n'est pas renseignée : veuillez réessayer.");
            return null;
        }
        else
        {
            Console.WriteLine("Vous êtes connecté.");
            user = utilisateurDataAccess.getUtilisateurFromMail(mail);
        }
        return user;
    }
}