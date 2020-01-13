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

        [HttpPost]
        public ActionResult Pay(String number, String date, String code)
        {
            return RedirectToAction("MyAccount", "Users");
        }

        public ActionResult Buy(int id)
        {
            OrderItems productModel = new OrderItems();
            var itemToBuy = (from i in _db.Item
                             where i.id_item == id
                             select i).First();
            if (Session["cart"] == null)
            {
                List<OrderItems> cart = new List<OrderItems>();
                cart.Add(new OrderItems { id_item = id, quantity = 1, Item = itemToBuy });
                Session["cart"] = cart;
            }
            else
            {
                List<OrderItems> cart = (List<OrderItems>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].quantity++;
                }
                else
                {
                    cart.Add(new OrderItems { id_item = id, quantity = 1, Item = itemToBuy });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Cart");
        }

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