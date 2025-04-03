using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;

public class ContientDataAccess : AccessBDD
{
    public List<Contient> getAllContients()
    {
        List<Contient> contients = new List<Contient>();
        string query = "SELECT * FROM contient";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    contients.Add(new Contient
                    {
                        ID_Recette = Convert.ToInt32(reader["ID_Recette"]),
                        Nom = reader["Nom"].ToString(),
                    });
                }
            }
        }
        return contients;
    }
    
    public void addContient(Contient contient)
    {
        string query = "INSERT INTO contient (ID_Recette, Nom) VALUES (@IDRecette, @nomIngredient)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@IDRecette", contient.ID_Recette);
            command.Parameters.AddWithValue("@nomIngredient", contient.Nom);
            connection.Open();
            command.ExecuteNonQuery();
        }    
    }
}