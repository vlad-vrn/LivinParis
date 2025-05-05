using DBConnectLibrary;
using LivinParis.LivraisonManagement;
using Org.BouncyCastle.Math.EC;
using Spectre.Console;

namespace LivinParis.CommandeManagement;

public class CreateCommande : GlobalDataAccess
{
    public void choisirPlat(int idUser)
    {
        CreateLivraison createLivraison = new CreateLivraison();
        Dictionary<string, Cuisinier> listCuisinier = new Dictionary<string, Cuisinier>();
        foreach (Cuisinier cuisto in cuisinierDataAccess.getAllCuisiniers())
        {
            listCuisinier.Add(utilisateurDataAccess.getUtilisateur(cuisto.ID_Utilisateur).Nom, cuisto);
        }
        Commande newCommande = new Commande();
        Dictionary<string, Plat> infosPlats = new Dictionary<string, Plat>();
        List<Plat> platsCommandes = new List<Plat>();
        
        var cuisinier  = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Chez quel cuisinier voulez vous commander ?\n")
                .PageSize(10)
                .AddChoices(listCuisinier.Keys));
        
        foreach (Plat registeredPlat in platDataAccess.getAllPlatFromCuisi(listCuisinier[cuisinier].ID_Cuisinier))
        {
            string s = registeredPlat.Nom + " : " + registeredPlat.Prix + "e";
            infosPlats.Add(s, registeredPlat);
        }

        var question = "Oui";
        while (question == "Oui")
        { 
            var choix  = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choix du plat :\n")
                    .PageSize(10)
                    .AddChoices(infosPlats.Keys));
            platsCommandes.Add(infosPlats[choix]);
        
            question  = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Un autre plat ?\n")
                    .PageSize(10)
                    .AddChoices(new []
                    {
                        "Oui", "Non"
                    }));
        }

        decimal prixTot = 0;
        Console.WriteLine("Récapitulons : \n");
        foreach (var plat in platsCommandes)
        {
            Console.WriteLine(plat.Nom + " : " + plat.Prix + "e");
            prixTot+= plat.Prix;
        }
        Console.WriteLine("Pour un total de " + prixTot + "e");
        var finaliser  = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Confirmez vous la commande ?\n")
                .PageSize(10)
                .AddChoices(new []
                {
                    "Oui", "Non"
                }));
        if (finaliser == "Oui")
        {
            newCommande.Prix_Commande = prixTot;
            newCommande.Nombre_Portion = platsCommandes.Count;
            newCommande.ID_Client = clientDataAccess.getClientIDFromUserID(idUser);
            newCommande.Date_Heure_Livraison = DateTime.Now;
            newCommande.ID_Cuisinier = listCuisinier[cuisinier].ID_Cuisinier; //C'est le cuisinierID du cuisinier
            Console.WriteLine("VOter cuisinier est il bien ");
            
            int newCommandeID = commandeDataAccess.addCommandeAndReturnID(newCommande);

            Console.WriteLine("hmm");
            Console.ReadKey();
            createLivraison.initLivraison(listCuisinier[cuisinier].ID_Cuisinier, idUser, newCommandeID);


        }
        //Dictionnaire 1 1 avec string en key et Plat en value pour pouvoir target le plat en particulier et le réduire
        //Console.WriteLine("Vous avez commandé le plat suivant : " + infosPlats[choix].Nom);
        //Console.ReadKey(); 10XXXXXXXXXXXXXXXXX
    }

    public void voirCommandes(int idUser)
    {
        Dictionary<string, Commande> titreCommandes = new Dictionary<string, Commande>();
        foreach (Commande commande in commandeDataAccess.getAllCommandeFromClient(clientDataAccess.getClientIDFromUserID(idUser)))
        {
            string s = "Commande d'ID " + commande.ID_Commande;
            titreCommandes.Add(s, commande);
        }
        
        Console.ReadKey();
        var choix  = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Sur quelle commande souhaitez vous avoir des informations ? :\n")
                .PageSize(10)
                .AddChoices(titreCommandes.Keys));
        Console.WriteLine("Nombre de portions : " + titreCommandes[choix].Nombre_Portion);
        Console.WriteLine("Prix de la commande : " + titreCommandes[choix].Prix_Commande);
        Console.WriteLine("Heure de commande : " + titreCommandes[choix].Date_Heure_Livraison);
        Console.ReadKey();
    }
}