using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantsApp
{
    public class CuisineTest
    {
        [Fact]
        public void Test_DatabaseEmpty()
        {
            int result = Cuisine.GetAll().Count;

            Assert.Equal(0, result);
        }
    }
}
