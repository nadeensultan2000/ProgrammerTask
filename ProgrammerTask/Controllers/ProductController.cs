using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammerTask.Data;
using ProgrammerTask.Models;
using ProgrammerTask.Reports;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgrammerTask.Controllers
{
   
    public class ProductController : Controller
    {
        public ApplicationDbContext context { get; }

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }


        // GET: ProductController
        public ActionResult Index()
        {
            IEnumerable<Product> products = context.Products.ToList();
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: ProductController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                bool ProductExist = context.Products.Any(p => p.Name == product.Name);
                if (ProductExist)
                {
                    ModelState.AddModelError("Name", "Product is Exist");
                }
                if (ModelState.IsValid)
                {

                    context.Products.Add(product);
                    context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {

            Product product = context.Products.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }

        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                bool ProductExist = context.Products.Any(p => p.Name == product.Name);
                if (ProductExist)
                {
                    ModelState.AddModelError("Name", "Product is Exist");
                }
                if (ModelState.IsValid)
                {
                    context.Products.Update(product);
                    context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5


        // POST: ProductController/Delete/5

        public ActionResult Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound(); // Return NotFound if the product is not found
            }
            else
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


        }
   
        public IActionResult PrintAll()
        {
            ProductsListReport report = new ProductsListReport();
            report.DataSource=context.Products.ToList();

       
            return View(report);
        }
        public IActionResult Print(int id)
        {
            
            //the datasource of report is always list not single even if it's single record
            var list = new List<Product>();
            var product = context.Products.Find(id);
            
            if (product == null) return NotFound();
            list.Add(product);
            var report = new ProductDetailsReport();
            report.DataSource=list;
            return View(report);
        }
    }
}

