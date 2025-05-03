using DBConnectLibrary;
using Spectre.Console;

namespace LivinParis.CommandeManagement;

public class CreateCommande : GlobalDataAccess
{
    public void choisirPlat(int idClient)
    {
        Commande newCommande = new Commande();
        Dictionary<string, Plat> infosPlats = new Dictionary<string, Plat>();
        List<Plat> platsCommandes = new List<Plat>();
        foreach (Plat registeredPlat in platDataAccess.getAllPlats())
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
            newCommande.ID_Client = clientDataAccess.getClientIDFromUserID(idClient);
            newCommande.Date_Heure_Livraison = DateTime.Now;
            
            commandeDataAccess.addCommande(newCommande);
        }
        Console.WriteLine("Le renoi est compétement fou");
        //Dictionnaire 1 1 avec string en key et Plat en value pour pouvoir target le plat en particulier et le réduire
        //Console.WriteLine("Vous avez commandé le plat suivant : " + infosPlats[choix].Nom);
        //Console.ReadKey(); 10XXXXXXXXXXXXXXXXX

        
    }
}