using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarDeLaHess.Models
{
    //Liste des commandes de l'utilisateur. utiliser pour afficher les informations dans la page utilisateur (commandes en cours, livré)
    public class UserView
    {
        public List<Order> Order { get; set; }
    }
}