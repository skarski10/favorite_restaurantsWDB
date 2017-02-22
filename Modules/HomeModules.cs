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

            Post["/submit"] = _ => {
                var newCuisine = new Cuisine(Request.Form["cuisine-type"]);
                newCuisine.Save();
                var cuisineList = Cuisine.GetAll();
            return View["cuisines.cshtml", cuisineList];
            };
        }
    }
}
