using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Poser.Core.Models;
using Poser.Core.Models.Orders;
using Poser.EF;

namespace Poser.Controllers
{
    [Route("Pos")]
    public class PosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PosController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var compositeModel = new CompositeModel
            {
                Products = _context.Products.ToList(),
                Categories = _context.Categories.ToList()
            };

            return View("../Dashboard/Pos/index", compositeModel);
        }

        [HttpGet]
        [Route("GetProductsData")]
        public JsonResult GetProductsData(int? categoryId)
        {
            try
            {
                var products = _context.Products.Where(p => !categoryId.HasValue || p.CategoryId == categoryId.Value).ToList();

                return Json(products);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpGet]
        [Route("GetProductStockData")]
        public JsonResult GetProductStockData(int? productId)
        {
            try
            {
                var productStocks = _context.ProductStocks
                    .Where(ps => ps.ProductId == productId).ToList();

                return Json(productStocks);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpPost]
        [Route("MakePayment")]
        public ActionResult MakePayment([FromBody]JObject data)
        {
            List<dynamic>? products = data["products"].ToObject<List<dynamic>>();

            if (products?.Count <= 0)
            {
                return Json(new { success = false, message = "There's No Products For This Payment" });
            }

            //double totalPrice = products.Sum(product => Convert.ToDouble(product["price"]));
            double totalPrice = 0;

            foreach (var product in products)
            {
                totalPrice += (double)product["price"];
            }

            //Begin Of Transaction 
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // Save Order Details
                var order = new Order()
                {
                    PaymentMethodId = 1,
                    Total = totalPrice,
                    IsPaid = true,
                    CustomerId = 4
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Save OrderProducts Details
                foreach (var product in products)
                {
                    var orderProduct = new OrderProduct()
                    {
                        OrderId = order.Id,
                        ProductStockId = 1,
                        //ProductStockId = (int)product["id"],
                        Quantity = (int)product["qty"],
                        Price = (double)product["price"] / (int)product["qty"],
                        Total = (double)product["price"]
                    };
                    _context.OrderProducts.Add(orderProduct);
                }


                //End Of Transaction 
                transaction.Commit();
            }
            catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");

                return Json(new { success = false, message = ex.Message });

            }
            _context.SaveChanges();

            return Json(new { success = true, message = "Payment Done Successfully" });
        }

    }
}
