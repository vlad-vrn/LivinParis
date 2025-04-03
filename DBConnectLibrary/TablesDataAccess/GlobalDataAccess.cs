namespace DBConnectLibrary;

public class GlobalDataAccess
{
    public readonly ClientDataAccess clientDataAccess = new ClientDataAccess();
    public readonly CommandeDataAccess commandeDataAccess = new CommandeDataAccess();
    public readonly ContientDataAccess contientDataAccess = new ContientDataAccess();
    public readonly CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
    public readonly IngredientDataAccess ingredientDataAccess = new IngredientDataAccess();
    public readonly LivraisonDataAccess livraisonDataAccess = new LivraisonDataAccess();
    public readonly PlatDataAccess platDataAccess = new PlatDataAccess();
    public readonly RecetteDataAccess recetteDataAccess = new RecetteDataAccess();
    public readonly UtilisateurDataAccess utilisateurDataAccess = new UtilisateurDataAccess();
}