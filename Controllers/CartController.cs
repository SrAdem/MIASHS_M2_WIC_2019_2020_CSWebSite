using BazarDeLaHess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarDeLaHess.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buy(int id)
        {
            ProductModel productModel = new ProductModel();
            if (Session["cart"] == null)
            {
                List<OrderItems> cart = new List<OrderItems>();
                cart.Add(new OrderItems { id_item = id, quantity = 1 });
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
                    cart.Add(new OrderItems { id_item = id, quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            List<OrderItems> cart = (List<OrderItems>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

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