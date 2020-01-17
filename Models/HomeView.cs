using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarDeLaHess.Models
{
    //Classe pour pouvoir avoir la liste des MasterCategory et des objets à afficher sur la page Index, page d'affichage des articles.
    public class HomeView
    {
        public List<MasterCategory> Category { get; set; }
        public List<Item> Item { get; set; }
    }
}