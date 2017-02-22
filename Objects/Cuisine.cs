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

        public override bool Equals(System.Object otherCuisine)
        {
            if (!(otherCuisine is Cuisine))
            {
                return false;
            }
            else
            {
                Cuisine newCuisine = (Cuisine) otherCuisine;
                bool typeEquality = this.GetType() == newCuisine.GetType();
                return (typeEquality);
            }
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO cuisine (type) OUTPUT INSERTED.id VALUES (@CuisineType);", conn);

            SqlParameter typeParameter = new SqlParameter();
            typeParameter.ParameterName = "@CuisineType";
            typeParameter.Value = this.GetType();
            cmd.Parameters.Add(typeParameter);

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

        public static Cuisine Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine WHERE id = @CuisineId;", conn);

            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@CuisineId";
            cuisineIdParameter.Value = id.ToString();
            cmd.Parameters.Add(cuisineIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundCuisineId = 0;
            string foundCuisineType = null;

            while (rdr.Read())
            {
                foundCuisineId = rdr.GetInt32(0);
                foundCuisineType = rdr.GetString(1);
            }
            Cuisine foundCuisine = new Cuisine(foundCuisineType, foundCuisineId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return foundCuisine;
        }

        public List<Restaurant> GetRestaurants()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE cuisine_id = @CuisineId;", conn);
            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@CuisineId";
            cuisineIdParameter.Value = this.GetCuisineId();
            cmd.Parameters.Add(cuisineIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Restaurant> restaurants = new List<Restaurant> {};
            while(rdr.Read())
            {
                int restaurantId = rdr.GetInt32(0);
                string restaurantName = rdr.GetString(1);
                int restaurantCuisineId = rdr.GetInt32(2);

                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantId);
                restaurants.Add(newRestaurant);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return restaurants;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM cuisine;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public int GetCuisineId()
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
