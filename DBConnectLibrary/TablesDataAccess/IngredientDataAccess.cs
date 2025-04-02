using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace DBConnectLibrary;

public class IngredientDataAccess : AccessBDD
{
  public List<Ingredient> getAllIngredients()
  {
    List<Ingredient> ingredients = new List<Ingredient>();
    string query = "SELECT * FROM ingrédient";
    using (var connection = Connection())
    using (var command = new MySqlCommand(query, connection))
    {
      connection.Open();
      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          ingredients.Add(new Ingredient
          {
            IngredientNom = reader["Nom"].ToString()
          });
        }
      }
    }
    return ingredients;
  }

  public void addIngredient(Ingredient ingredient)
  {
    string query = "INSERT INTO ingrédient (Nom) VALUES (@Nom)";
    using (var connection = Connection())
    using (var command = new MySqlCommand(query, connection))
    {
      command.Parameters.AddWithValue("@Nom", ingredient.IngredientNom);
      
      connection.Open();
      
      command.ExecuteNonQuery();
    }  
  }

  public void delIngredient(string ingredientNom)
  {
    string query = "DELETE FROM ingrédient WHERE Nom = @ingredientNom;";
    using (var connection = Connection())
    using (var command = new MySqlCommand(query, connection))
    {
      command.Parameters.AddWithValue("@ingredientNom", ingredientNom);
      connection.Open();
      command.ExecuteNonQuery();
    }  
  }
}