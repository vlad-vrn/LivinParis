using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;

public class UtilisateurDataAccess : AccessBDD
{
    public List<Utilisateur> getAllUtilisateurs()
    {
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        string query = "SELECT * FROM Utilisateur";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    utilisateurs.Add(new Utilisateur
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nom = reader["Nom"].ToString(),
                        Prenom = reader["Prénom"].ToString(),
                        Telephone = Convert.ToInt32(reader["Téléphone"]),
                        Mail = reader["Mail"].ToString(),
                        Rue = reader["Rue"].ToString(),
                        NumeroRue = reader["Numero_Rue"].ToString(),
                        Ville = reader["Ville"].ToString(),
                        CodePostal = Convert.ToInt32(reader["Code_Postal"]),
                        StationProche = reader["Station_Plus_Proche"].ToString()
                    });
                }
            }
        }
        return utilisateurs;
    }
}