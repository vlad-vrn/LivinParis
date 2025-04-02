using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;

public class LivraisonDataAccess : AccessBDD
{
    public List<Livraison> getAllLivraisons()
    {
        List<Livraison> livraisons = new List<Livraison>();
        string query = "SELECT * FROM livraison";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    livraisons.Add(new Livraison
                    {
                        ID_Livraison = Convert.ToInt32(reader["ID_Livraison"]),
                        adresse_client = reader["adresse_client"].ToString(),
                        adresse_cuisinier = reader["adresse_cuisinier"].ToString(),
                        Date_Livraison = Convert.ToDateTime(reader["Date_Livraison"]),
                        ID_Commande = Convert.ToInt32(reader["ID_Commande"])
                    });
                }
            }
        }
        return livraisons;
    }

    public void addLivraison(Livraison livraison)
    {
        string query =
            "INSERT INTO livraison (ID_Livraison, adresse_client, adresse_cuisinier, Date_Livraison, ID_Commande) VALUES (@idClient, @adresseClient, @adresseCuisinier, @dateLivraison, @idCommande)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idClient", livraison.ID_Livraison);
            command.Parameters.AddWithValue("@adresseClient", livraison.adresse_client);
            command.Parameters.AddWithValue("@adresseCuisinier", livraison.adresse_cuisinier);
            command.Parameters.AddWithValue("@dateLivraison", livraison.Date_Livraison);
            command.Parameters.AddWithValue("@idCommande", livraison.ID_Commande);
            
            connection.Open();
            
            command.ExecuteNonQuery();
        }
    }

    public void delLivraison(int idLivraison)
    {
        string query = "DELETE FROM livraison WHERE ID_Livraison = @idLivraison";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idLivraison", idLivraison);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}