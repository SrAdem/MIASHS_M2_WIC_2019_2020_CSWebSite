using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarDeLaHess.Models
{
    public class Item
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public Boolean Available
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public string Picture
        {
            get;
            set;
        }

    }
}