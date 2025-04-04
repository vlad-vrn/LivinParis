using System;
using System.Collections.Generic;
using DBConnectLibrary;

namespace LivinParis.Application;

public class Login
{
    public int Session { get; set; }
    
    public Utilisateur userLogin()
    {
        UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
        Utilisateur user = new Utilisateur();
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        List<string> emailUtilisés = new List<string>();
        foreach (Utilisateur registeredUsers in utilisateurDataAccess.getAllUtilisateurs())
        {
            emailUtilisés.Add(registeredUsers.Mail);
        }
        
        Console.WriteLine("Connexion : ");
        Console.WriteLine("Veuillez entrer votre adresse mail : ");
        string mail = Console.ReadLine();
        if (emailUtilisés.Contains(mail) == false)
        {
            Console.WriteLine("Cette adresse mail n'est pas renseignée : veuillez réessayer.");
            Console.ReadKey();
            return null; ///Return QUOI en vrai...
        }
        else
        {
            user = utilisateurDataAccess.getUtilisateurFromMail(mail);
            Console.WriteLine("Bienvenue " + user.Prenom + ", vous êtes connecté.");
        }
        return user;
    }
}