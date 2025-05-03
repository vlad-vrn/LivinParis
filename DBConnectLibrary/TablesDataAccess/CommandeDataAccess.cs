using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;


public class CommandeDataAccess : AccessBDD
{
    public List<Commande> GetAllCommandes()
    {
        List<Commande> commandes = new List<Commande>();
        string query = "SELECT * FROM Commande";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    commandes.Add(new Commande
                    {
                        ID_Commande = Convert.ToInt32(reader["ID_Commande"]),
                        Prix_Commande = Convert.ToDecimal(reader["Prix_Commande"]),
                        Nombre_Portion = Convert.ToInt32(reader["Nombre_Portions"]),
                        Date_Heure_Livraison = Convert.ToDateTime(reader["Date_Heure_Livraison"]),
                        ID_Client = Convert.ToInt32(reader["ID_Client"]),
                    });
                }
            }
        }
        return commandes;
    }

    public void addCommande(Commande commande)
    {
        string query =
            "INSERT INTO Commande (Prix_Commande, Nombre_Portions, Date_Heure_Livraison, ID_Client) VALUES (@commandePrix, @commandeNbPortions, @commandeDHLivraison, @commandeIDClient)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@commandePrix", commande.Prix_Commande);
            command.Parameters.AddWithValue("@commandeNbPortions", commande.Nombre_Portion);
            command.Parameters.AddWithValue("@commandeDHLivraison", commande.Date_Heure_Livraison);
            command.Parameters.AddWithValue("@commandeIDClient", commande.ID_Client);
            
            connection.Open();
            
            command.ExecuteNonQuery();
        }
    }
    
    //Ne peut pas marcher : une commande est réferencée comme une primary key de est_Commandé
    public void delCommande(int idCommande)
    {
        string query = "DELETE FROM Commande WHERE ID_Commande = @idCommande";
        using(var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idCommande", idCommande);
            connection.Open();
            command.ExecuteNonQuery();
        }    
    }
    
    public List<Commande> getAllCommandeFromClient(int ID_Client)
    {
        List<Commande> commandes = new List<Commande>();
        string query = "SELECT * FROM commande WHERE ID_client = @ID_Client";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID_Client", ID_Client);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    commandes.Add(new Commande
                    {
                        ID_Commande = Convert.ToInt32(reader["ID_Commande"]),
                        Prix_Commande = Convert.ToDecimal(reader["Prix_Commande"]),
                        Nombre_Portion = Convert.ToInt32(reader["Nombre_Portions"]),
                        Date_Heure_Livraison = Convert.ToDateTime(reader["Date_Heure_Livraison"]),
                        ID_Client = Convert.ToInt32(reader["ID_client"])
                    });
                }
            }
        }
        return commandes;
    }
    
}
