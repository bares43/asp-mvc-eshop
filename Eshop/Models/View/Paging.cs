using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Eshop.Class;

namespace Eshop.Models.View
{
    public class Paging
    {
        public Paginator Paginator { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public RouteValueDictionary UrlParams(int page)
        {
            var values =  new RouteValueDictionary(new {page = page});

            foreach (var param in Paginator.UrlParams)
            {
                values.Add(param.Key, param.Value);
            }

            return values;
        }
    }
}