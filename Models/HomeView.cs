﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarDeLaHess.Models
{
    public class HomeView
    {
        public List<Category> Category { get; set; }
        public List<Item> Item { get; set; }
    }
}