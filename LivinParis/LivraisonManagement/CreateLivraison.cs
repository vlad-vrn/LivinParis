using DBConnectLibrary;
using LivinParis.StationManagement;
using Org.BouncyCastle.Math.EC;
using Spectre.Console;

namespace LivinParis.LivraisonManagement;

public class CreateLivraison : GlobalDataAccess
{
    public void initLivraison(int cuisiIDFromCuisi, int userIDFromClient, int idCommande)
    {
        Livraison newLivraison = new Livraison();
        Utilisateur thisCuisinier = utilisateurDataAccess.getUtilisateur(cuisinierDataAccess.getUserIDFromCuisiID(cuisiIDFromCuisi));
        Utilisateur thisClient = utilisateurDataAccess.getUtilisateur(userIDFromClient);
        bool status = false;


        newLivraison.station_cuisinier = thisCuisinier.StationProche;
        newLivraison.station_client = thisClient.StationProche;
        newLivraison.est_livre = status;
        newLivraison.Date_Livraison = DateTime.Now;
        newLivraison.ID_Commande = idCommande;
        newLivraison.ID_Client = clientDataAccess.getClientIDFromUserID(thisClient.Id);

        livraisonDataAccess.addLivraison(newLivraison);
        Console.ReadKey();
    }

    public void voirLivraisons(int idUser)
    {
        StationSelector selector = new StationSelector();
        StationManager stationManager = new StationManager();
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
        Console.WriteLine("Partant de " + selector.nomStationFromID(titreLivraisons[choix].station_cuisinier));
        Console.WriteLine("jusqu'a " + selector.nomStationFromID(titreLivraisons[choix].station_client));
        Console.WriteLine(stationManager.CheminMetro(titreLivraisons[choix].station_cuisinier, titreLivraisons[choix].station_client));
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
                Console.ReadKey();
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