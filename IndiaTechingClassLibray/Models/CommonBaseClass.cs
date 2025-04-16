using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class CommonBaseClass
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages
        {
            get 
            {
                if (TotalItems != 0 && PageSize != 0)
                {
                    return (int)Math.Ceiling((decimal)TotalItems / PageSize);
                }
                else
                    return 0;
            }
        }
    }
}