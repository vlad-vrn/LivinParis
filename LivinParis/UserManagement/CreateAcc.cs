using System;
using System.Collections.Generic;
using DBConnectLibrary;
using LivinParis.StationManagement;

namespace LivinParis.Application;

public static class CreateAcc
{
    public static int Session { get; set; }

    public static void CreerCompteUser()
    {
        UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
        StationSelector stationSelector = new StationSelector();
        Utilisateur newUser = new Utilisateur();
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        utilisateurs = utilisateurDataAccess.getAllUtilisateurs();
        List<string> emailUtilisés = new List<string>();
        foreach (Utilisateur registeredUser in utilisateurs)
        {
            emailUtilisés.Add(registeredUser.Mail.ToUpper());
        }
        
        Console.WriteLine("\nEntrez votre nom de famille : ");
        string nom = Console.ReadLine();
        newUser.Nom = nom;

        
        Console.WriteLine("\nEntrez votre prénom : ");
        string prenom = Console.ReadLine();
        newUser.Prenom = prenom;
        
        Console.WriteLine("\nEntrez votre adresse mail : ");
        string mail = Console.ReadLine();
        
        while (emailUtilisés.Contains(mail.ToUpper()) == true)
        {
            Console.WriteLine("\nCette adresse email est déjà utilisée : veuillez entrer une nouvelle adresse mail : ");
            mail = Console.ReadLine();
        }
        newUser.Mail = mail;
        
        Console.WriteLine("\nEntrez votre numéro de téléphone : ");
        int tel = int.Parse(Console.ReadLine());
        newUser.Telephone = tel;
        
        Console.WriteLine("\nEntrez votre rue : ");
        string rue = Console.ReadLine();
        newUser.Rue = rue;
        Console.WriteLine("\nEntrez votre numéro de rue : ");
        string numRue = Console.ReadLine();
        newUser.NumeroRue = numRue;
        
        Console.WriteLine("\nEntrez votre ville");
        string ville = Console.ReadLine();
        newUser.Ville = ville;
        
        Console.WriteLine("\nEntrez votre code postal");
        try
        {
            int codePostal = int.Parse(Console.ReadLine());
            newUser.CodePostal = codePostal;

        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : veuillez remplir votre code Postal ultérieurement.");
        }
        
        Console.WriteLine("\nQuel est la station de métro la plus proche : ");
        int station = stationSelector.choisirStation();
        newUser.StationProche = station;
        
        utilisateurDataAccess.addUtilisateur(newUser);
    }
    
    
}

