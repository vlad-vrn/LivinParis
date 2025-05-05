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
                        StationProche = Convert.ToInt32(reader["Station_Plus_Proche"])                    });
                }
            }
        }
        return utilisateurs;
    }
    
    public List<Utilisateur> getAllUtilisateursOrderByName()
    {
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        string query = "SELECT * FROM Utilisateur ORDER BY Nom";
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
                        StationProche = Convert.ToInt32(reader["Station_Plus_Proche"])                    });
                }
            }
        }
        return utilisateurs;
    }
    
    public List<Utilisateur> getAllUtilisateursOrderByRues()
    {
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        string query = "SELECT * FROM Utilisateur ORDER BY Rue";
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
                        StationProche = Convert.ToInt32(reader["Station_Plus_Proche"])                    });
                }
            }
        }
        return utilisateurs;
    }
    public Utilisateur getUtilisateur(int userId)
    {
        Utilisateur utilisateur = new Utilisateur();
        string query = "SELECT * FROM Utilisateur WHERE Id=@userId";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@userId", userId);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()){
                
                    utilisateur = new Utilisateur
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
                        StationProche = Convert.ToInt32(reader["Station_Plus_Proche"])
                    };
                }
            }
        }
        return utilisateur;
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

    public bool uniqueEmail(string email)
    {
        string query = "SELECT Count(*) FROM Utilisateur WHERE Mail = @email";
        using (var connection = Connection())
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
    
    public bool existingEmail(string email)
    {
        string query = "SELECT Count(*) FROM Utilisateur WHERE Mail = @email";
        using (var connection = Connection())
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
    
    public Utilisateur getUtilisateurFromMail(string mail)
    {
        Utilisateur user = new Utilisateur();
        string query = "SELECT * FROM Utilisateur WHERE Mail=@userMail";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@userMail", mail);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.Nom = reader["Nom"].ToString();
                    user.Prenom = reader["Prénom"].ToString();
                    user.Telephone = Convert.ToInt32(reader["Téléphone"]);
                    user.Mail = reader["Mail"].ToString();
                    user.Rue = reader["Rue"].ToString();
                    user.NumeroRue = reader["Numero_Rue"].ToString();
                    user.Ville = reader["Ville"].ToString();
                    user.CodePostal = Convert.ToInt32(reader["Code_Postal"]);
                    user.StationProche = Convert.ToInt32(reader["Station_Plus_Proche"]);
                }
            }
        }
        return user;
    }
    
    
}