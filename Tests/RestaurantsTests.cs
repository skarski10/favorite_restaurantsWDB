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
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurant;Integrated Security=SSPI;";
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
            Restaurant firstRestaurant = new Restaurant("Subway", 1);
            Restaurant secondRestaurant = new Restaurant("Subway", 1);

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
            Restaurant testRest = new Restaurant("Subway", 1);
            testRest.Save();

            //Act
            List<Restaurant> testList = new List<Restaurant>{testRest};
            List<Restaurant> result = Restaurant.GetAll();

            //Assert
            Assert.Equal(testList, result);

        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
          Restaurant testRest = new Restaurant("subway", 1);
          testRest.Save();

          Restaurant savedRest = Restaurant.GetAll()[0];

          int testId = testRest.GetId();
          int result = savedRest.GetId();

          Assert.Equal(testId, result);
        }
        [Fact]
        public void Test_FindFindsTaskInDatabase()
        {
          // Arrange
          Restaurant  testRestaurant = new Restaurant("Red Lobster", 1);
          testRestaurant.Save();

          // Act
          Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

          // Assert
          Assert.Equal(testRestaurant, foundRestaurant);
        }

        public void Dispose()
        {
            Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
