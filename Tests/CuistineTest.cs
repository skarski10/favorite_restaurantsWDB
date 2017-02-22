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



        public void Dispose()
        {
            Cuisine.DeleteAll();
        }

    }
}
