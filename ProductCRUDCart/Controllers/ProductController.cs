using ProductCRUDCart.Models;
using ProductCRUDCart.Models.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ProductCRUDCart.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            Database db = new Database();
            var products = db.Products.Get();
            return View(products);
        }
        public ActionResult Shop()
        {
            Database db = new Database();
            var products = db.Products.Get();
            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Product p = new Product();
            return View(p);
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                Database db = new Database();
                db.Products.Create(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Database db = new Database();
            var p = db.Products.Get(id);
            return View(p);
        }
        [HttpPost]
        public ActionResult Update(Product p)
        {
            if (ModelState.IsValid)
            {
                Database db = new Database();
                db.Products.Update(p);
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                Database db = new Database();
                db.Products.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            if (ModelState.IsValid)
            {
                Database db = new Database();
                var p = db.Products.Get(id);
                if (Session["Cart"] == null)
                {
                    List<Product> CartProduct = new List<Product>();
                    CartProduct.Add(p);
                    string json = new JavaScriptSerializer().Serialize(CartProduct);
                    Session["Cart"] = json;
                    return RedirectToAction("Shop");
                }
                else
                {
                    var Product = Session["Cart"].ToString();
                    var CartProduct = new JavaScriptSerializer().Deserialize<List<Product>>(Product);
                    CartProduct.Add(p);
                    string json = new JavaScriptSerializer().Serialize(CartProduct);
                    Session["Cart"] = json;
                    return RedirectToAction("Shop");
                }
            }
            return View();
        }

        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                return View();
            }
            var Product = Session["Cart"].ToString();
            var CartProducts = new JavaScriptSerializer().Deserialize<List<Product>>(Product);
            return View(CartProducts);
        }

        public ActionResult GenerateOrder()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Cart");
            }
            var Product = Session["Cart"].ToString();
            var CartProducts = new JavaScriptSerializer().Deserialize<List<Product>>(Product);

            Database db = new Database();
            foreach (var p in CartProducts)
            {
                db.Products.Order(p);
            }
            Session["Cart"] = null;
            return View();
        }
    }
}