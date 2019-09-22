using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SocialNetwork
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //Note the use of a constraint with a regex that matches integers of any length
            //Need the constraint to ensure this route doesn't match /photo/create for example
            routes.MapRoute(
                name: "PhotoRoute",
                url: "photo/{id}",
                defaults: new { controller = "Photo", action = "Details" },
                constraints: new { id = "[0-9]+" }
            );

            routes.MapRoute(
                name: "CommentRoute",
                url: "comment/{id}",
                defaults: new { controller = "Comment", action = "Details" },
                constraints: new { id = "[0-9]+" }
            );
        }
    }
}
