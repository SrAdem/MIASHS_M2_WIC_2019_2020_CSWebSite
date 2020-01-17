using BazarDeLaHess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace BazarDeLaHess.Controllers
{
    public class CartController : Controller
    {
        private readonly BazarDeLaHessEntities _db = new BazarDeLaHessEntities();
        public ActionResult Cart()
        {
            if (Session["userid"] == null) //Si l'utilisateur n'est pas connecté, on le renvoie à la page d'accueil
                return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult Order()
        {
            if (Session["userid"] == null) //Si l'utilisateur n'est pas connecté, on le renvoie à la page d'accueil
                return RedirectToAction("Index","Home");

            //On récupère l'adresse de l'utilisateur
            int id = (int)Session["userid"];
            Address userAddress = (from i in _db.Users
                                   where i.id_user == id
                                   select i.Address).First();

            //Si l'utilisateur à déjà une adresse enregistrer alors on l'affiche sur le formulaire
            if (userAddress != null)
                return View(userAddress);
            //Sinon on affiche un formulaire vide
            return View(new Address());
        }

        [HttpPost]
        public ActionResult Order(Address address, string save, string send)
        {
            ModelState.Remove("id_address"); // This will remove the key 

            if (ModelState.IsValid) //S'il y a toutes les information requise
            {
                if(save != null) //Si l'utilisateur souhaite sauvgarder les informations d'adresse
                {
                    List<Address> addressExist = (from i in _db.Address
                                            where i.number == address.number
                                            where i.title == address.title
                                            where i.city == address.city
                                            where i.postcode == address.postcode
                                            where i.country == address.country
                                            select i).ToList();

                    int id = (int)Session["userid"];
                    Users currentUser = (from i in _db.Users
                                         where i.id_user == id
                                         select i).First();

                    if (addressExist.Count == 0)//Si l'adresse n'éxiste pas, on la crée dans la base de donnée en l'ajoutant à l'utilisateur
                    {
                        //Ajoute l'adresse chez l'utilisateur dans la base de donnée !! et dans la table adresse par la jointure !!
                        currentUser.Address = address;
                        _db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                    }
                    else
                    { //Sinon on ajoute l'adresse déjà existante de la base de donnée et on met à jour l'utilisateur
                        currentUser.Address = addressExist.First();
                        _db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                else if(send != null) //Si l'utilisateur souhaite envoyer les informations pour le paiment
                {
                    return RedirectToAction("Pay");
                }
            }
            //Dans le cas où les champs ne sont pas complété
            return View(address);
        }

        public ActionResult Pay ()
        {
            if (Session["userid"] == null) //Si l'utilisateur n'est pas connecté, on le renvoie à la page d'accueil
                return RedirectToAction("Index", "Home");
            return View();
        }

        //Le paiement à était effectué, il faut donc enregistrer la commande
        //Les paramêtres sont inutiles mais permettent de différencier le Get (fonction précédente) du Post.
        [HttpPost]
        public ActionResult Pay(String number, String date, String code)
        {
            List<OrderItems> cart = (List<OrderItems>)Session["cart"]; //On récupère la liste des articles
            //On récupère les informations sur l'utilisateur
            int userid = (int)Session["userid"];

            //On créer une nouvelle commande, on la complète et l'enregistre dans la base de donnée
            Order order = new Order();
            ModelState.Remove("id_order");
            order.id_user = userid;
            order.delivered = false;
            order.date = DateTime.Now;
            _db.Order.Add(order);
            _db.SaveChanges();

            int lastOrder = (from i in _db.Order //On récupère l'id de la commande
                             select i.id_order).OrderByDescending(i => i).First();

            //Pour chaque article, on enlève Item pour qu'il n'y ai pas d'erreur d'insertion et on ajoute l'id de la commande
            foreach (OrderItems orderItem in cart)
            {
                orderItem.Item = null;
                orderItem.id_order = lastOrder;
                _db.OrderItems.Add(orderItem); //On ajoute à la bdd
            }
            //On sauvgarde
            _db.SaveChanges();

            Session["cart"] = new List<OrderItems>(); //On vide le panier
            return RedirectToAction("MyAccount", "Users"); //On affiche la page utilisateur
        }

        //Ajouter un produit (id) au panier
        public ActionResult Buy(int id)
        {
            OrderItems productModel = new OrderItems();
            var itemToBuy = (from i in _db.Item
                             where i.id_item == id
                             select i).First();
           
            List<OrderItems> cart = (List<OrderItems>)Session["cart"]; //On recupère le contenu du panier
            int index = isExist(id); //Vérifie si le produit est dans le panier
            if (index != -1) //Si le produit est déjà dans le panier
            {
                cart[index].quantity++; //On augmente sa quantité
            }
            else
            {
                cart.Add(new OrderItems { id_item = id, quantity = 1, Item = itemToBuy }); //Sinon on l'ajoute au panier
            }
            Session["cart"] = cart;
            return RedirectToAction("Cart"); //Renvoie à la page du panier
        }

        // modifie la quantité d'un objet
        public ActionResult Edit(int id, int qte)
        {
            if(qte == 0)
            {
                return View("Cart");
            }
            List<OrderItems> cart = (List<OrderItems>)Session["cart"];
            int index = isExist(id);
            cart[index].quantity = qte;
            return RedirectToAction("Cart");
        }

        //Supprime un objet du panier
        public ActionResult Remove(int id)
        {
            List<OrderItems> cart = (List<OrderItems>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Cart");
        }

        /*Retourn l'index de l'item dans la liste des items du panier*/
        private int isExist(int id)
        {
            List<OrderItems> cart = (List<OrderItems>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].id_item.Equals(id))
                    return i;
            return -1;
        }

    }
}