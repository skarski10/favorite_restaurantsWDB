using Nancy;
using RestaurantsApp;
using System.Collections.Generic;

namespace RestaurantsApp
{
    public class HomeModule: NancyModule
    {
        public HomeModule()
        {
            // Take you to the homepage
            Get["/"] = _ => {
                return View["index.cshtml"];
            };
            // take you to the cuisine page
            Get["/cuisines"] = _ => {
                // var newCuisine = new Cuisine(Request.Form["cuisine-type"]);
                // newCuisine.Save();
                var cuisineList = Cuisine.GetAll();
                return View["cuisines.cshtml", cuisineList];
            };
            // Post the enter cuisine name onto the cuisines page
            Post["/cuisines"] = _ => {
                var newCuisine = new Cuisine(Request.Form["cuisine-type"]);
                newCuisine.Save();
                var cuisineList = Cuisine.GetAll();
                return View["cuisines.cshtml", cuisineList];
            };
            // Delete a cuisine
            Delete["/cuisines/{cuisineId}"] = parameters => {
                Cuisine specificCuisine = Cuisine.Find(parameters.cuisineId);
                specificCuisine.Delete();
                List<Cuisine> cuisineList = Cuisine.GetAll();
                return View["cuisines.cshtml", cuisineList];
            };
            // Edit a cuisine
            Patch["/cuisines/{cuisineId}/updated"] = parameters => {
                Cuisine selectedCuisine = Cuisine.Find(parameters.cuisineId);
                selectedCuisine.Update(Request.Form["cuisine-type"]);
                return View["cuisineUpdated.cshtml"];
            };
            // Take you to the page to edit a cuisine
            Get["/cuisines/{cuisineId}/edit"] = parameters => {
                Cuisine selectedCuisine = Cuisine.Find(parameters.cuisineId);
                return View["cuisineEdit.cshtml", selectedCuisine];
            };

            // Get an id for each cuisine and take you to the clicked on cuisines restaurant page
            Get["/cuisines/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Cuisine selectedCuisine = Cuisine.Find(parameters.id);
                List<Restaurant> addedRestaurants = selectedCuisine.GetRestaurants();
                model.Add("restaurant", addedRestaurants);
                model.Add("cuisine", selectedCuisine);
                return View["restaurants.cshtml", model];
            };
            // Take the restaurant entered and post in on the restaurants page
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
            //
            Get["/cuisine/{id}/restaurant/{restaurantId}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Cuisine selectedCuisine = Cuisine.Find(parameters.id);
                List<Restaurant> allRestaurants = selectedCuisine.GetRestaurants();
                model.Add("cuisine", selectedCuisine);
                model.Add("restaurant", allRestaurants);
                return View["restaurants.cshtml", model];
            };
            // Delete a restaurant from a restaurant
            Delete["/cuisines/{id}/restaurants/{restaurantId}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Cuisine selectedCuisine = Cuisine.Find(parameters.id);
                Restaurant specificRestaurant = Restaurant.Find(parameters.restaurantId);
                specificRestaurant.Delete();
                List<Restaurant> restaurantList = selectedCuisine.GetRestaurants();
                model.Add("cuisine", selectedCuisine);
                model.Add("restaurant", restaurantList);
                return View["restaurants.cshtml", model];
            };
            // Take you to the page to edit a restaurant
            Get["/restaurants/{restaurantId}/edit"] = parameters => {
                Restaurant selectedRestaurant = Restaurant.Find(parameters.restaurantId);
                return View["edit_restaurant_form.cshtml", selectedRestaurant];
            };

            // Edit a restaurant
            Patch["/restaurants/{restaurantId}/updated"] = parameters => {
                Restaurant selectedRestaurant = Restaurant.Find(parameters.restaurantId);
                selectedRestaurant.Update(Request.Form["restaurant"]);
                return View["restaurant_updated.cshtml"];
            };
        }
    }
}
