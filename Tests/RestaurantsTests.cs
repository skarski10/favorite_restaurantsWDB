using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantsApp
{
    public class RestaurantTest : IDisposable
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
        [Fact]
        public void Test_EqualOverrideTrueForSameName()
        {
            // Arrange
            Restaurant firstRestaurant = new Restaurant("Subway");
            Restaurant secondRestaurant = new Restaurant("Subway");

            // Act
            firstRestaurant.Save();
            secondRestaurant.Save();

            // Assert
            Assert.Equal(firstRestaurant, secondRestaurant);
        }

        [Fact]
        public void Test_Save_SaveToDB()
        {
            //Arrange
            Restaurant testRest = new Restaurant("Subway");
            testRest.Save();

            //Act
            List<Restaurant> testList = new List<Restaurant>{testRest};
            List<Restaurant> result = Restaurant.GetAll();

            //Assert
            Assert.Equal(testList, result);

        }

        public void Dispose()
        {
            Restaurant.DeleteAll();
        }
    }
}
