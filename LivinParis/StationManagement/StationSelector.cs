using Org.BouncyCastle.Asn1.IsisMtt.X509;
using Spectre.Console;

namespace LivinParis.StationManagement;

public class StationSelector
{
    public Dictionary<int, StationLP> Metro = StationManager.remplirMetroLP();
    public Dictionary<int, StationLP> MetroRed = StationManager.remplirMetroLPRed();
    public string nomStationFromID(int stationID)
    {
        return MetroRed[stationID].libelle;
    }
    public int choisirStation()
    {
        List<string> libelles = new List<string>();

        var ligne = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choisissez votre ligne\n")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14"
                }));
        
        foreach (StationLP station in Metro.Values)
        {
            if (station.lignes.Contains(Convert.ToInt32(ligne)))
            {
                libelles.Add(station.libelle);
            }
        }
        
        var choix  = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choix de la station\n")
                .PageSize(10)
                .AddChoices(libelles));
        int idStation = 0;
        
        foreach (var kvp in Metro)
        {
            if (kvp.Value.libelle == choix)
            {
                idStation = kvp.Key;
                break;
            }
        }
        
        return idStation;
    }
}