using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    
    public List<Utilisateur> getUtilisateur(int userId)
    {
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        string query = "SELECT * FROM Utilisateur WHERE Id=@userId";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@userId", userId);
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
    
    public void addUtilisateur(Utilisateur utilisateur)
    {
        string query = "INSERT INTO Utilisateur (Nom, Prénom, Téléphone, Mail, Rue, Numero_Rue, Ville, Code_Postal, Station_Plus_Proche) VALUES (@userNom, @userPrenom, @userTelephone, @userMail, @userRue, @userNumRue, @userVille, @userCodePostal, @userStationProche)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@userNom", utilisateur.Nom);
            command.Parameters.AddWithValue("@userPrenom", utilisateur.Prenom);
            command.Parameters.AddWithValue("@userTelephone", utilisateur.Telephone);
            command.Parameters.AddWithValue("@userMail", utilisateur.Mail);
            command.Parameters.AddWithValue("@userRue", utilisateur.Rue);
            command.Parameters.AddWithValue("@userNumRue", utilisateur.NumeroRue);
            command.Parameters.AddWithValue("@userVille", utilisateur.Ville);
            command.Parameters.AddWithValue("@userCodePostal", utilisateur.CodePostal);
            command.Parameters.AddWithValue("@userStationProche", utilisateur.StationProche);
            
            connection.Open();
            
            command.ExecuteNonQuery();
        }
    }
    
    public void delUtilisateur(int userId)
    {
        string query = "DELETE FROM Utilisateur WHERE id = @userId;";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@userId", userId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}