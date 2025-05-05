using DBConnectLibrary;
using Org.BouncyCastle.Math.EC;
using Spectre.Console;

namespace LivinParis.LivraisonManagement;

public class CreateLivraison : GlobalDataAccess
{
    public void initLivraison(int userIDFromCuisi, int userIDFromClient, int idCommande)
    {
        Livraison newLivraison = new Livraison();
        Utilisateur thisCuisinier = utilisateurDataAccess.getUtilisateur(userIDFromCuisi);
        Utilisateur thisClient = utilisateurDataAccess.getUtilisateur(userIDFromClient);
        bool status = false;


        newLivraison.station_cuisinier = thisCuisinier.StationProche;
        newLivraison.station_client = thisClient.StationProche;
        newLivraison.est_livre = status;
        newLivraison.Date_Livraison = DateTime.Now;
        newLivraison.ID_Commande = idCommande;
        newLivraison.ID_Client = thisClient.Id;
        livraisonDataAccess.addLivraison(newLivraison);
    }

    public void voirLivraisons(int idUser)
    {
        Dictionary<string, Livraison> titreLivraisons = new Dictionary<string, Livraison>();
        foreach (Livraison livraison in livraisonDataAccess.getAllLivraisonsFromClient(clientDataAccess.getClientIDFromUserID(idUser)))
        {
            string s = "Livraison d'ID " + livraison.ID_Livraison;
            titreLivraisons.Add(s, livraison);
        }
        
        var choix  = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Sur quelle livraison souhaitez vous avoir des informations ? :\n")
                .PageSize(10)
                .AddChoices(titreLivraisons.Keys));
        
        Console.WriteLine("Livraison de la commande " + titreLivraisons[choix].ID_Commande);
        Console.WriteLine("Partant de " + titreLivraisons[choix].station_cuisinier);
        Console.WriteLine("jusqu'a " + titreLivraisons[choix].station_client);
        if (titreLivraisons[choix].est_livre == false)
        {
            Console.WriteLine("Cette commande n'est pas arrivée à notre connaissance.");
            var arrive  = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Est-elle arrivée ?\n")
                    .PageSize(10)
                    .AddChoices(new []
                    {
                        "Oui", "Non"
                    }));
            if (arrive == "Oui")
            {
                livraisonDataAccess.marquerLivraisonCommeLivree(titreLivraisons[choix].ID_Livraison);
            }
        }
        else
        {
            Console.WriteLine("Cette commande est arrivée.");
        }
        Console.ReadKey();
    }
}