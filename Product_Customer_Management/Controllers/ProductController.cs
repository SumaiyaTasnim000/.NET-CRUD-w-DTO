using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Product_Customer_Management.DTOs;
using Product_Customer_Management.EF;


namespace Product_Customer_Management.Controllers
{
    public class ProductController : Controller
    {
        
        ProductEntities1 db = new ProductEntities1();

     
        public static prod_table Convert(ProductDTO p)
        {
            return new prod_table
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = (int)p.Price,
                StockQuantity = p.StockQuantity,
                Category = p.Category
            };
        }

        public static ProductDTO Convert(prod_table p)
        {
            return new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Category = p.Category
            };
        }

        public static List<ProductDTO> Convert(List<prod_table> data)
        {
            var list = new List<ProductDTO>();
            foreach (var p in data)
            {
                list.Add(Convert(p));
            }
            return list;
        }

        // GET: Product/List
        public ActionResult List()
        {
            var data = db.prod_table.ToList();
            return View(Convert(data));
        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new prod_table());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductDTO p)
        {
            if (ModelState.IsValid)
            {
                db.prod_table.Add(Convert(p));
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(p);
        }

        // GET: Product/Details/{id}
        public ActionResult Details(int id)
        {
            var exobj = db.prod_table.Find(id);
            return View(Convert(exobj));
        }

        // GET: Product/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var exobj = db.prod_table.Find(id);
            return View(Convert(exobj));
        }

        // POST: Product/Edit
        [HttpPost]
        
        public ActionResult Edit(ProductDTO p)
        {
            var exobj = db.prod_table.Find(p.ProductId);

            if (exobj != null)
            {
                exobj.Name = p.Name;
                exobj.Description = p.Description;
                exobj.Price = (int)p.Price; // Convert decimal to int
                exobj.StockQuantity = p.StockQuantity;
                exobj.Category = p.Category;

                db.SaveChanges();
            }

            return RedirectToAction("List");
        }


        // GET: Product/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var exobj = db.prod_table.Find(id);
            return View(Convert(exobj));
        }

        // POST: Product/Delete
        [HttpPost]
        public ActionResult Delete(int productId, string dcsn)
        {
            if (dcsn.Equals("Yes"))
            {
                var exobj = db.prod_table.Find(productId);
                db.prod_table.Remove(exobj);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
