using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RestaurantsApp
{
    public class Restaurant
    {
        private int _id;
        private string _name;
        private int _cuisineId;

        public Restaurant(string name, int cuisineId, int id=0)
        {
            _id = id;
            _name = name;
            _cuisineId = cuisineId;
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
                bool cuisineIdEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
                return (restaurantEquality && cuisineIdEquality);
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
                int restaurantCuisineId = rdr.GetInt32(2);

                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantId);
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

            SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @CuisineId);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@RestaurantName";
            nameParameter.Value = this.GetName();

            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@CuisineId";
            cuisineIdParameter.Value = this.GetCuisineId();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(cuisineIdParameter);

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
          int foundCuisineId = 0;

          while(rdr.Read())
          {
            foundRestaurantId = rdr.GetInt32(0);
            foundRestaurantName = rdr.GetString(1);
            foundCuisineId = rdr.GetInt32(2);
          }

          Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundCuisineId, foundRestaurantId);

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
        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd= new SqlCommand("DELETE FROM restaurants WHERE id=@RestaurantId", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@RestaurantId";
            idParameter.Value = this.GetId();
            cmd.Parameters.Add(idParameter);
            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
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
        public int GetCuisineId()
       {
           return _cuisineId;
       }
       public void SetCuisineId(int newCuisineId)
       {
           _cuisineId = newCuisineId;
       }
    }
}
