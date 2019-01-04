using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Product_Assignment.Models;

namespace MVC_Product_Assignment.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult NewProduct()
        {
            ProductDAL dal = new ProductDAL();
            ViewBag.category = dal.GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult NewProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                model.ProductImageAddress = "/Images/" + Guid.NewGuid() + ".jpg";
                model.ProductImageFile.SaveAs(Server.MapPath(model.ProductImageAddress));
                ProductDAL dal = new ProductDAL();
                int id = dal.AddProduct(model);
                ViewBag.msg = "Product Added : " + id;
                ModelState.Clear();
                ViewBag.category = dal.GetCategory();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ProductDAL dal = new ProductDAL();
                ViewBag.category = dal.GetCategory();
                return View();
            }
        }
        public ActionResult Index()
        {
            ViewBag.msg = "Product Details";
            return View();
        }
        public ActionResult Search()
        {
            List<ProductProjectionModel> list = new List<ProductProjectionModel>();
            return View(list);
        }
        [HttpPost]
        public ActionResult Search(string key)
        {
            ProductDAL dal = new ProductDAL();
            List<ProductProjectionModel> list = dal.Search(key);
            return View(list);
        }
        public ActionResult Find(int id)
        {
            ProductDAL dal = new ProductDAL();
            ProductModel model = dal.Find(id);
            return View(model);
        }
        public ActionResult Update(int id)
        {
            ProductDAL dal = new ProductDAL();
            ProductModel model = dal.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(ProductModel model)
        {
            ProductDAL dal = new ProductDAL();
            dal.Update(model.ProductID, model.ProductPrice,model.ProductCategory);
            return View("View_Updated");
        }
    }
}