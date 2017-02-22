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
    }
}
