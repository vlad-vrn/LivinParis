using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;

public class ClientDataAccess : AccessBDD
{
    public List<Client> getAllClients()
    {
        List<Client> clients = new List<Client>();
        string query = "SELECT * FROM client_";
        using (var connection = Connection())
        using(var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    clients.Add(new Client
                    {
                        ID_Client = Convert.ToInt32(reader["ID_Client"]),
                        Entreprise = Convert.ToBoolean(reader["Entreprise"]),
                        ID_Utilisateur = Convert.ToInt32(reader["Id"])
                    });
                }
            }
        }
        return clients;
    }

    public void addClient(Client client)
    {
        string query = "INSERT INTO client_ (ID_client, Entreprise, ID) VALUES (@ID_client, @Entreprise, @ID_Utilisateur)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID_client", client.ID_Client);
            command.Parameters.AddWithValue("@Entreprise", client.Entreprise);
            command.Parameters.AddWithValue("@ID_Utilisateur", client.ID_Utilisateur);
            
            connection.Open();
            
            command.ExecuteNonQuery();
        }
    }

    public void delClient(int idClient)
    {
        string query = "DELETE FROM client_ WHERE ID_Client = @ID_Client";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID_Client", idClient);
            connection.Open();
            command.ExecuteNonQuery();
        }    
    }
}