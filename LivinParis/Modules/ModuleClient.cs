using DBConnectLibrary;
using Spectre.Console;

namespace LivinParis.Modules;

public class ModuleClient : GlobalDataAccess
{
    public void moduleClient()
    {
        List<int> cuisinierUserID = new List<int>();
        foreach (Cuisinier cuisinier in this.cuisinierDataAccess.getAllCuisiniers())
        {
            cuisinierUserID.Add(cuisinier.ID_Utilisateur);
        }
        List<int> clientUserID = new List<int>();
        foreach (Client client in this.clientDataAccess.getAllClients()) 
        {
            clientUserID.Add(client.ID_Utilisateur);
        }
        
        var rep = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Module client\nQue voulez vous consulter ?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Liste des utilisateurs (ordre alphabétique)", "Liste des utilisateurs (par rues)", "Liste des utilisateurs (par achats)", "Retour"
                }));
        switch (rep)
        {
            case "Liste des utilisateurs (ordre alphabétique)":
                foreach (var utilisateur in utilisateurDataAccess.getAllUtilisateursOrderByName())
                {
                    string str1 = utilisateur.Nom + " " + utilisateur.Prenom;
                    string str2(List<int> listClientID) => listClientID.Contains(utilisateur.Id) ? "Client" : "";
                    string str3(List<int> listCuisiID) => listCuisiID.Contains(utilisateur.Id) ? "Cuisinier" : ""; //trans en null et ??- (une autre nuit)
                    Console.WriteLine(str1 + " : " + str2(clientUserID) + " " +  str3(cuisinierUserID));
                }
                Console.ReadKey();
                break;
            case "Liste des utilisateurs (par rues)":
                foreach (var utilisateur in utilisateurDataAccess.getAllUtilisateursOrderByRues())
                {
                    string str1 = utilisateur.Prenom  + " " + utilisateur.Nom + " : " + utilisateur.NumeroRue + utilisateur.Rue;
                    string str2(List<int> listClientID) => listClientID.Contains(utilisateur.Id) ? "Client" : "";
                    string str3(List<int> listCuisiID) => listCuisiID.Contains(utilisateur.Id) ? "Cuisinier" : ""; //trans en null et ??- (une autre nuit)
                    Console.WriteLine(str1 + " : " + str2(clientUserID) + " " +  str3(cuisinierUserID));
                }
                Console.ReadKey();
                break;
            case "Liste des utilisateurs (par achats)":
                
                break;
            
            case "Retour":
                break;
        }
        
    }
}