using DBConnectLibrary;
using Spectre.Console;

namespace LivinParis.CommandeManagement;

public class CreateCommande : GlobalDataAccess
{
    public void choisirPlat(int idClient)
    {
        List<string> infosPlats = new List<string>();
        foreach (Plat registeredPlat in platDataAccess.getAllPlats())
        {
            string s = registeredPlat.Nom + " : " + registeredPlat.Prix + "\u20ac";
            infosPlats.Add(s);
        }
        //Dictionnaire 1 1 avec string en key et Plat en value pour pouvoir target le plat en particulier et le réduire 
        var choix  = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choix du plat :")
                .PageSize(10)
                .AddChoices(infosPlats));
        
    }
}