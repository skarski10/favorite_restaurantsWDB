using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RestaurantsApp
{
    public class Restaurant
    {
        private int _id;
        private string _name;

        public Restaurant(string name, int id=0)
        {
            _id = id;
            _name = name;
        }

        public override bool Equals(System.Object otherRestaurant)
        {
            if (!(otherRestaurant is Restaurant))
            {
                return false;
            }
            else
            {
                Restaurant newRestaurant = (Restaurant) otherRestaurant;
                bool restaurantEquality = this.GetName() == newRestaurant.GetName();
                return (restaurantEquality);
            }
        }

        public static List<Restaurant> GetAll()
        {
            List<Restaurant> AllRestaurants = new List<Restaurant>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int restaurantId = rdr.GetInt32(0);
                string restaurantName = rdr.GetString(1);

                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantId);
                AllRestaurants.Add(newRestaurant);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return AllRestaurants;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name) OUTPUT INSERTED.id VALUES (@RestaurantName);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@RestaurantName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static Restaurant Find(int id)
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
          SqlParameter restaurantIdParameter = new SqlParameter();
          restaurantIdParameter.ParameterName = "@RestaurantId";
          restaurantIdParameter.Value = id.ToString();
          cmd.Parameters.Add(restaurantIdParameter);
          SqlDataReader rdr = cmd.ExecuteReader();

          int foundRestaurantId = 0;
          string foundRestaurantName = null;

          while(rdr.Read())
          {
            foundRestaurantId = rdr.GetInt32(0);
            foundRestaurantName = rdr.GetString(1);
          }

          Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantId);

          if (rdr != null)
          {
            rdr.Close();
          }
          if (conn != null)
          {
            conn.Close();
          }
          return foundRestaurant;

        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }
    }
}
