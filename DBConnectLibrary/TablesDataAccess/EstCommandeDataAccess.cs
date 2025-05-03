using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;

public class EstCommandeDataAccess : AccessBDD
{
    public List<EstCommande> getAllEstCommande()
    {
        List<EstCommande> estCommande = new List<EstCommande>();
        string query = "SELECT * FROM est_commandé";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    estCommande.Add(new EstCommande
                    {
                        ID_Plat = Convert.ToInt32(reader["ID_Plat"]),
                        ID_Commande = Convert.ToInt32(reader["ID_Commande"]),
                    });
                }
            }
        }
        return estCommande;
    }

    public void addEstCommande(EstCommande estCommande)
    {
        string query = "INSERT INTO est_commandé (ID_Plat, ID_Commande) VALUES (@ID_Plat, @ID_Commande)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID_Plat", estCommande.ID_Plat);
            command.Parameters.AddWithValue("@ID_Commande", estCommande.ID_Commande);
            command.ExecuteNonQuery();
        }    
    }
}