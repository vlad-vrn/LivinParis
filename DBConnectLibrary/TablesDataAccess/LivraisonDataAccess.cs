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
                        station_client = reader["station_client"].ToString(),
                        station_cuisinier = reader["station_cuisinier"].ToString(),
                        Date_Livraison = Convert.ToDateTime(reader["Date_Livraison"]),
                        est_livre = Convert.ToBoolean(reader["est_livre"]),
                        ID_Commande = Convert.ToInt32(reader["ID_Commande"]),
                        ID_Client = Convert.ToInt32(reader["ID_Client"]),
                    });
                }
            }
        }
        return livraisons;
    }

    public void addLivraison(Livraison livraison)
    {
        string query =
            "INSERT INTO livraison (ID_Livraison, station_client, station_cuisinier, Date_Livraison, est_livre, ID_Commande, ID_Client) VALUES (@idLivraison, @stationClient, @stationCuisinier, @dateLivraison, @est_livre, @idCommande, @idClient)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idLivraison", livraison.ID_Livraison);
            command.Parameters.AddWithValue("@stationClient", livraison.station_client);
            command.Parameters.AddWithValue("@stationCuisinier", livraison.station_cuisinier);
            command.Parameters.AddWithValue("@dateLivraison", livraison.Date_Livraison);
            command.Parameters.AddWithValue("@est_livre", livraison.est_livre);
            command.Parameters.AddWithValue("@idCommande", livraison.ID_Commande);
            command.Parameters.AddWithValue("@idClient", livraison.ID_Client);
            
            connection.Open();
            
            command.ExecuteNonQuery();
        }
    }

    public List<Livraison> getAllLivraisonsFromClient(int ID_Client)
    {
        List<Livraison> livraisons = new List<Livraison>();
        string query = "SELECT * FROM livraison WHERE ID_Client = @idClient";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idClient", ID_Client);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    livraisons.Add(new Livraison
                    {
                        ID_Livraison = Convert.ToInt32(reader["ID_Livraison"]),
                        station_client = reader["station_client"].ToString(),
                        station_cuisinier = reader["station_cuisinier"].ToString(),
                        Date_Livraison = Convert.ToDateTime(reader["Date_Livraison"]),
                        est_livre = Convert.ToBoolean(reader["est_livre"]),
                        ID_Commande = Convert.ToInt32(reader["ID_Commande"]),
                    });
                }
            }
        }

        return livraisons;
    }

    public void marquerLivraisonCommeLivree(int idLivraison)
    {
        string query = "UPDATE Livraison SET est_livre = TRUE WHERE ID_Livraison = @id";

        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", idLivraison);
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