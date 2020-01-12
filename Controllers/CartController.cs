﻿using BazarDeLaHess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult ToOrder()
        {
            return View("Order");
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