using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;


public class PlatDataAccess : AccessBDD
{
    
    public List<Plat> getAllPlats()
    {
        List<Plat> plats = new List<Plat>();
        string query = "SELECT * FROM Plat";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    plats.Add(new Plat
                    {
                        ID_Plat = Convert.ToInt32(reader["ID_Plat"]),
                        Nom = reader["nom"].ToString(),
                        Quantite = Convert.ToInt32(reader["quantité"]),
                        Prix = Convert.ToDecimal(reader["prix"]),
                        Date_Fabrication = Convert.ToDateTime(reader["date_fabrication"]),
                        Date_Peremption = Convert.ToDateTime(reader["date_péremption"]),
                        NombrePortion = Convert.ToInt32(reader["nombre_portions_total"]),
                        PlatDuJour = Convert.ToBoolean(reader["plat_du_jour"]),
                        ID_Recette = Convert.ToInt32(reader["id_recette"]),
                        ID_Cuisinier = Convert.ToInt32(reader["id_cuisinier"])
                    });
                }
            }
        }
        return plats;
    }
    
    public void addPlat(Plat plat)
    {
        string query = "INSERT INTO Plat (Nom, Quantité, Prix, Date_Fabrication, Date_Péremption, Nombre_Portions_Total, Plat_Du_Jour, ID_Recette, ID_cuisinier) VALUES (@platNom, @platQuantite, @platPrix, @platDateFab, @platDatePer, @platNbPortion, @platDuJour, @platIDRecette, @platIDCuisinier)";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@platNom", plat.Nom);
            command.Parameters.AddWithValue("@platQuantite", plat.Quantite);
            command.Parameters.AddWithValue("@platPrix", plat.Prix);
            command.Parameters.AddWithValue("@platDateFab", plat.Date_Fabrication);
            command.Parameters.AddWithValue("@platDatePer", plat.Date_Peremption);
            command.Parameters.AddWithValue("@platNbPortion", plat.NombrePortion);
            command.Parameters.AddWithValue("@platDuJour", plat.PlatDuJour);
            command.Parameters.AddWithValue("@platIDRecette", plat.ID_Recette);
            command.Parameters.AddWithValue("@platIDCuisinier", plat.ID_Cuisinier);
            
            connection.Open();
            
            command.ExecuteNonQuery();
        }
    }

    public void delPlat(int idPlat)
    {
        string query = "DELETE FROM Plat WHERE ID_Plat = @idPlat";
        using(var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idPlat", idPlat);
            connection.Open();
            command.ExecuteNonQuery();
        }    
    }
    
    public List<Plat> getAllPlatFromCuisi(int ID_Cuisinier)
    {
        List<Plat> plats = new List<Plat>();
        string query = "SELECT * FROM plat WHERE ID_cuisinier=@ID_Cuisinier";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID_Cuisinier", ID_Cuisinier);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    plats.Add(new Plat
                    {
                        ID_Plat = Convert.ToInt32(reader["ID_Plat"]),
                        Nom = reader["nom"].ToString(),
                        Quantite = Convert.ToInt32(reader["quantité"]),
                        Prix = Convert.ToDecimal(reader["prix"]),
                        Date_Fabrication = Convert.ToDateTime(reader["date_fabrication"]),
                        Date_Peremption = Convert.ToDateTime(reader["date_péremption"]),
                        NombrePortion = Convert.ToInt32(reader["nombre_portions_total"]),
                        PlatDuJour = Convert.ToBoolean(reader["plat_du_jour"]),
                        ID_Recette = Convert.ToInt32(reader["id_recette"]),
                        ID_Cuisinier = Convert.ToInt32(reader["id_cuisinier"])
                    });
                }
            }
        }
        return plats;
    }
    
    public List<Plat> getPlatDuJour(Cuisinier thisCuisinier)
    {
        List<Plat> plats = new List<Plat>();
        string query = "SELECT * FROM plat WHERE Plat_Du_Jour = TRUE";
        using (var connection = Connection())
        using (var command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    plats.Add(new Plat
                    {
                        ID_Plat = Convert.ToInt32(reader["ID_Plat"]),
                        Nom = reader["Nom"].ToString(),
                        Quantite = Convert.ToInt32(reader["quantité"]),
                        Prix = Convert.ToDecimal(reader["prix"]),
                        Date_Fabrication = Convert.ToDateTime(reader["date_fabrication"]),
                        Date_Peremption = Convert.ToDateTime(reader["date_péremption"]),
                        NombrePortion = Convert.ToInt32(reader["nombre_portions_total"]),
                        PlatDuJour = Convert.ToBoolean(reader["plat_du_jour"]),
                        ID_Recette = Convert.ToInt32(reader["id_recette"]),
                        ID_Cuisinier = Convert.ToInt32(reader["id_cuisinier"])
                    });
                }
            }
        }
        return plats;
    }
}