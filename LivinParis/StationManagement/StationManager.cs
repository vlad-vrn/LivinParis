using DBConnectLibrary;
using Graph;
using LivinParis.LivraisonManagement;
using Org.BouncyCastle.Math.EC;
using Spectre.Console;

namespace LivinParis.StationManagement;

public class StationManager : GlobalDataAccess
{
    private List<Station> stations = Graphe<Station>.ChargerStations();


    public void choisirStation()
    {
        
    }
    public string CheminMetro(int startId, int endId)
    {
        return "aez";
    }
}