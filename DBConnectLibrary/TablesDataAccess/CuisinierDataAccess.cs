using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;

public class CuisinierDataAccess : AccessBDD
{
    public List<Cuisinier> getAllCuisiniers()
    {
        List<Cuisinier> cuisiniers = new List<Cuisinier>();
        string query = "SELECT * FROM cuisinier";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cuisiniers.Add(new Cuisinier
                    {
                        ID_Cuisinier = Convert.ToInt32(reader["ID_Cuisinier"]),
                        ID_Utilisateur = Convert.ToInt32(reader["ID"])
                    });
                }
            }
        }
        return cuisiniers;
    }

    public void addCuisinier(Cuisinier cuisinier)
    {
        string query = "INSERT INTO cuisinier (ID_cuisinier, ID) VALUES (@ID_cuisinier, @ID)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID_cuisinier", cuisinier.ID_Cuisinier);
            command.Parameters.AddWithValue("@ID", cuisinier.ID_Utilisateur);
            
            connection.Open();
            command.ExecuteNonQuery();
        }    
    }

    public void delCuisinier(int cuisinierID)
    {
        string query = "DELETE FROM cuisinier WHERE ID_cuisinier = @ID_cuisinier";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID_cuisinier", cuisinierID);
            connection.Open();
            command.ExecuteNonQuery();
        }    
    }
    
    public int getCuisiIDFromUserID(int userID)
    {
        Cuisinier cuisinier = new Cuisinier();
        string query = "SELECT * FROM cuisinier WHERE ID=@userID";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@userID", userID);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cuisinier.ID_Cuisinier = Convert.ToInt32(reader["ID_cuisinier"]);
                }
            }
        }
        return cuisinier.ID_Cuisinier;
    }
    
    public int getUserIDFromCuisiID(int cuisiID)
    {
        Cuisinier cuisinier = new Cuisinier();
        string query = "SELECT * FROM cuisinier WHERE ID_cuisinier=@cuisiID";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@cuisiID", cuisiID);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cuisinier.ID_Utilisateur = Convert.ToInt32(reader["ID"]);
                }
            }
        }
        return cuisinier.ID_Utilisateur;
    }
    
    public Cuisinier getCuisiFromUserID(int userID)
    {
        Cuisinier cuisinier = new Cuisinier();
        string query = "SELECT * FROM cuisinier WHERE ID=@userID";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@userID", userID);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cuisinier = new Cuisinier
                    {
                        ID_Cuisinier = Convert.ToInt32(reader["ID_cuisinier"]),
                        ID_Utilisateur = Convert.ToInt32(reader["ID"]),
                    };
                }
            }
        }
        return cuisinier;
    }
    
}