using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RestaurantsApp
{
    public class Cuisine
    {
        private int _id;
        private string _type;

        public Cuisine(string type, int id = 0)
        {
            _id = id;
            _type = type;
        }


        public static List<Cuisine> GetAll()
        {
            List<Cuisine> cuisineList = new List<Cuisine> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int cuisineId = rdr.GetInt32(0);
                string cuisineType = rdr.GetString(1);

                Cuisine newCuisine = new Cuisine(cuisineType, cuisineId);
                cuisineList.Add(newCuisine);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return cuisineList;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetType()
        {
            return _type;
        }

        public void SetType(string newType)
        {
            _type = newType;
        }

    }
}
