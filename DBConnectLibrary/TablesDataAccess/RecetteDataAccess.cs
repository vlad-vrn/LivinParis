using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;

public class RecetteDataAccess : AccessBDD
{
    public List<Recette> getAllRecettes()
    {
        List<Recette> recettes = new List<Recette>();
        string query = "SELECT * FROM recette";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    recettes.Add(new Recette
                    {
                        ID_Recette = Convert.ToInt32(reader["idRecette"]),
                        Nom_Recette = reader["Nom"].ToString()
                    });
                }
            }
        }
        return recettes;
    }

    public void addRecette(Recette recette)
    {
        string query = "INSERT INTO recette (Nom) VALUES (@NomRecette)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@NomRecette", recette.Nom_Recette);
            connection.Open();
            command.ExecuteNonQuery();
        }    
    }

    public void delRecette(int idRecette)
    {
        string query = "DELETE FROM recette WHERE ID_Recette = @idRecette";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idRecette", idRecette);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}