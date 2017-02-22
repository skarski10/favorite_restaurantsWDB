using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantsApp
{
    public class RestaurantTest
    {
        public RestaurantTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmpty()
        {
            // Arrange, Act
            int result = Restaurant.GetAll().Count;

            // Assert
            Assert.Equal(0, result);
        }
    }
}
