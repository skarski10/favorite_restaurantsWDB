using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantsApp
{
    public class CuisineTest : IDisposable
    {
        [Fact]
        public void Test_DatabaseEmpty()
        {
            int result = Cuisine.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_ReturnTrueIfEqual()
        {
            //Arrange, Act
            Cuisine firstCuisine = new Cuisine("French");
            Cuisine secondCuisine = new Cuisine("French");

            //Assert
            Assert.Equal(firstCuisine, secondCuisine);

        }

        [Fact]
        public void Test_Save_SavesCuisine()
        {
            // Arrange
            Cuisine testCuisine = new Cuisine("German");

            // Act
            testCuisine.Save();
            List<Cuisine> result = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine>{testCuisine};

            // Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
            // Arrange
            Cuisine testCuisine = new Cuisine("Mexican");
            testCuisine.Save();

            // Act
            Cuisine savedCuisine = Cuisine.GetAll()[0];

            int result = savedCuisine.GetCuisineId();
            int testId = testCuisine.GetCuisineId();

            // Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_FindCuisineId()
        {
            //Arrange
            Cuisine testCuisine = new Cuisine("Mex");
            testCuisine.Save();

            //Act
            Cuisine foundCuisineId = Cuisine.Find(testCuisine.GetCuisineId());

            //Assert
            Assert.Equal(testCuisine, foundCuisineId);
        }

        [Fact]
        public void Test_GetRestaurants_RetrievesAllRestaurantsWithCuisine()
        {
            Cuisine testCuisine = new Cuisine("Mexican");
            testCuisine.Save();

            Restaurant firstRestaurant = new Restaurant("Chipotle", testCuisine.GetCuisineId());
            firstRestaurant.Save();
            Restaurant secondRestaurant = new Restaurant("Taco Del Mar", testCuisine.GetCuisineId());
            secondRestaurant.Save();


            List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
            List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

            Assert.Equal(testRestaurantList, resultRestaurantList);
        }


        public void Dispose()
        {
            Cuisine.DeleteAll();
            Restaurant.DeleteAll();
        }

    }
}
