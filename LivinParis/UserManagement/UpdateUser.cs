using DBConnectLibrary;

namespace LivinParis.Application;

public static class UpdateUser
{
    public static void becomeClient(Utilisateur user)
    {
        ClientDataAccess clientDataAccess = new ClientDataAccess();
        Client newClient = new Client();
        bool isEntreprise;
        Console.WriteLine("Vous souhaitez devenir client\nÊtes-vous une entreprise (y/n) : ");
        if (Console.ReadLine().ToUpper() == "Y")
        {
            isEntreprise = true;
        }
        else
        {
            isEntreprise = false;
        }

        newClient.ID_Utilisateur = user.Id;
        if (isEntreprise)
        {
            newClient.Entreprise = true;
        }
        else
        {
            newClient.Entreprise = false;
        }
        
        clientDataAccess.addClient(newClient);
        Console.WriteLine("Vous êtes maintenant client sur LivinParis.");
    }

    public static void becomeCuisinier(Utilisateur user)
    {
        CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
        Cuisinier newCuisinier = new Cuisinier();
        
        newCuisinier.ID_Utilisateur = user.Id;
        cuisinierDataAccess.addCuisinier(newCuisinier);
        Console.WriteLine("Vous êtes maintenant cuisinier sur LivinParis.");
    }
}