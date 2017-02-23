using Nancy;
using RestaurantsApp;
using System.Collections.Generic;

namespace RestaurantsApp
{
    public class HomeModule: NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml"];
            };

            Get["/cuisines"] = _ => {
                // var newCuisine = new Cuisine(Request.Form["cuisine-type"]);
                // newCuisine.Save();
                var cuisineList = Cuisine.GetAll();
                return View["cuisines.cshtml", cuisineList];
            };

            Post["/cuisines"] = _ => {
                var newCuisine = new Cuisine(Request.Form["cuisine-type"]);
                newCuisine.Save();
                var cuisineList = Cuisine.GetAll();
                return View["cuisines.cshtml", cuisineList];
            };

            Get["/cuisines/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Cuisine selectedCuisine = Cuisine.Find(parameters.id);
                List<Restaurant> addedRestaurants = selectedCuisine.GetRestaurants();
                model.Add("restaurant", addedRestaurants);
                model.Add("cuisine", selectedCuisine);
                return View["restaurants.cshtml", model];
            };
            Post["/cuisine/{id}/restaurants"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Cuisine selectedCuisine = Cuisine.Find(Request.Form["cuisine-id"]);
                List<Restaurant> cuisineRestaurant = selectedCuisine.GetRestaurants();
                string restaurantEntered = Request.Form["restaurant"];
                Restaurant newRestaurant = new Restaurant(restaurantEntered, selectedCuisine.GetCuisineId());
                newRestaurant.Save();
                cuisineRestaurant.Add(newRestaurant);
                model.Add("restaurant", cuisineRestaurant);
                model.Add("cuisine", selectedCuisine);
                return View["restaurants.cshtml", model];
            };
            Post["/cuisines/delete"] = _ => {
                Cuisine.DeleteAll();
                Restaurant.DeleteAll();
                return View["cuisines.cshtml"];
            };

            // Get["/cuisines/{id}/restaurants"] = parameters => {
            //     Dictionary<string, object> model = new Dictionary<string, object>();
            //     Cuisine selectedCuisine = Cuisine.Find(parameters.id);
            //     List<Restaurant> allRestaurants = selectedCuisine.GetRestaurants();
            //     model.Add("cuisine", selectedCuisine);
            //     model.Add("restaurant", allRestaurants);
            //     return View["restaurant.cshtml", model];
            // };
        }
    }
}
