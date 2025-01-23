using ECommerceProject.Data;
using ECommerceProject.Models;
using ECommerceProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddToCart(int pid, double qty)
        {
            var extcart = HttpContext.Session.GetObject<Cart>("mycart");
            if (extcart == null)
            {
                var prod = _context.Product.Include(m=>m.Unit).Include(m=> m.Configaration).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == pid);
                if (prod != null)
                {
                    ProductVM p = new ProductVM();
                    p.ProductId = pid;
                    p.ProductName = prod.ProductName;
                    p.UnitId = prod.UnitId;
                    p.UnitName = prod.Unit.UnitName;
                    p.Image = prod.ProductImages[0].ImageCode;
                    p.ConfigarationId = prod.ConfigarationId;
                    p.ConfigarationName = prod.Configaration.ConfigarationName;
                    p.SaleQuantity = (decimal)qty;
                    p.UnitPrice = prod.SalePrice;
                    p.SubTotal = prod.SalePrice * (decimal)qty;

                    Cart c = new Cart();
                    c.Products.Add(p);
                    HttpContext.Session.SetObject<Cart>("mycart", c);
                    return Json(new { flag = "1", msg = "Product is added in empty cart" });
                }
                else
                {
                    return Json(new { flag = "0", msg = "Product is invalid" });
                }
            }
            else
            {
                var prod1 = _context.Product.Include(m => m.Unit).Include(m => m.Configaration).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == pid);
                if (prod1 != null)
                {
                    var prod2 = extcart.Products.FirstOrDefault(x => x.ProductId == pid);
                    if(prod2==null)
                    {
                        ProductVM p = new ProductVM();
                        p.ProductId = pid;
                        p.ProductName = prod1.ProductName;
                        p.UnitId = prod1.UnitId;
                        p.UnitName = prod1.Unit.UnitName;
                        p.Image = prod1.ProductImages[0].ImageCode;
                        p.ConfigarationId = prod1.ConfigarationId;
                        p.ConfigarationName = prod1.Configaration.ConfigarationName;
                        p.SaleQuantity = (decimal)qty;
                        p.UnitPrice = prod1.SalePrice;
                        p.SubTotal = prod1.SalePrice * (decimal)qty;

                        //Cart c = new Cart();
                        //c.Products.Add(p);
                        extcart.Products.Add(p);
                        HttpContext.Session.SetObject<Cart>("mycart", extcart);
                        return Json(new { flag = "1", msg = "Product is added in existing cart" });
                    }
                    else
                    {
                        prod2.SaleQuantity += 1;
                        HttpContext.Session.SetObject<Cart>("mycart", extcart);
                        return Json(new { flag = "1", msg = "Product is updated in existing cart" });
                    }
                }
                else
                {
                    return Json(new { flag = "0", msg = "Product is invalid" });
                }
            }
        }

        [HttpPost]
        public JsonResult RemoveItem(int pid)
        {
            var extcart = HttpContext.Session.GetObject<Cart>("mycart");
            if (extcart != null)
            {
                var prod = extcart.Products.FirstOrDefault(x => x.ProductId == pid);
                if (prod != null)
                {
                    extcart.Products.Remove(prod);
                    HttpContext.Session.SetObject<Cart>("mycart", extcart);
                    return Json(new { flag = "1", msg = "Product is removed successfully." });
                }
                else
                {
                    return Json(new { flag = "0", msg = "Product is invalid" });
                }
            }
            else
            {
                return Json(new { flag = "0", msg = "Product is invalid" });
            }
        }

        [HttpPost]
        public JsonResult IncreaseItem(int pid)
        {
            var extcart = HttpContext.Session.GetObject<Cart>("mycart");
            if(extcart != null)
            {
                var prod = extcart.Products.FirstOrDefault(x => x.ProductId == pid);
                if (prod != null)
                {
                    prod.SaleQuantity += 1;
                    HttpContext.Session.SetObject<Cart>("mycart", extcart);
                    return Json(new { flag = "1", msg = "Product Increase Successfully" });
                }
                else
                {
                    return Json(new { flag = "0", msg = "Product is invalid" });
                }
            }
            else
            {
                return Json(new { flag = "0", msg = "Product is invalid" });
            }
        }

        [HttpPost]
        public JsonResult DecreaseItem(int pid)
        {
            var extcart = HttpContext.Session.GetObject<Cart>("mycart");
            if (extcart != null)
            {
                var prod = extcart.Products.FirstOrDefault(x => x.ProductId == pid);
                if (prod != null)
                {
                    decimal qt = prod.SaleQuantity;
                    if(qt >= 2)
                    {
                        prod.SaleQuantity -= 1;
                        HttpContext.Session.SetObject<Cart>("mycart", extcart);
                        return Json(new { flag = "1", msg = "Product decreased Successfully" });
                    }
                    else
                    {
                        return Json(new { flag = "0", msg = "Please remove item" });
                    }
                }
                else
                {
                    return Json(new { flag = "0", msg = "Product is invalid" });
                }
            }
            else
            {
                return Json(new { flag = "0", msg = "Product is invalid" });
            }
        }

        //For Showing Cart
        public IActionResult ShoppingCart()
        {
            var extcart = HttpContext.Session.GetObject<Cart>("mycart");
            if(extcart != null)
            {
                return View(extcart.Products);
            }
            else
                return View();
        }

        public IActionResult Checkout()
        {
            var extcart = HttpContext.Session.GetObject<Cart>("mycart");
            if( extcart != null)
            {
                return View(extcart.Products);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string custname, string custmobile, string custemail, string custaddress)
        {
            string msg = "";
            if (string.IsNullOrEmpty(custname))
            {
                msg = "Customer Name is required";
            }
            if (string.IsNullOrEmpty(custmobile))
            {
                msg += "Mobile Number is required";
            }
            if (string.IsNullOrEmpty(custemail))
            {
                msg += "Email is required";
            }
            if (string.IsNullOrEmpty(custaddress))
            {
                msg += "Address is required";
            }

            if (msg.Length > 5)
            {
                TempData["msg"] = msg;
                return RedirectToAction("checkout");
            }

            var extcart = HttpContext.Session.GetObject<Cart>("mycart");
            if (extcart != null)
            {
                Customer c = new Customer();
                c.CustomerName = custname;
                c.CustomerMobile = custmobile;
                c.CustomerEmail = custemail;
                c.CustomerAddress = custaddress;

                _context.Customer.Add(c);
                await _context.SaveChangesAsync();

                SalesOrder so = new SalesOrder();
                so.CustomerId = c.Id;
                so.InvoiceNumber = "";
                so.InvoiceDate = DateTime.Today;
                so.ShippingCharge = 0;
                so.Discount = 0;

                _context.SalesOrder.Add(so);
                await _context.SaveChangesAsync();

                decimal total = 0;
                decimal tax = 0;
                decimal tax_factor = (decimal)0.0125;
                decimal netpayable = 0;

                foreach (var item in extcart.Products)
                {
                    SaleDetails d = new SaleDetails();
                    d.ProductId = item.ProductId;
                    d.SaleQuantity = item.SaleQuantity;
                    d.UnitPrice = item.UnitPrice;
                    d.UnitId = item.UnitId;
                    d.SalesOrderId = so.Id;
                    d.SubTotal = item.SaleQuantity * item.UnitPrice;

                    total += d.SubTotal;

                    _context.SaleDetails.Add(d);
                }
                await _context.SaveChangesAsync();

                tax = total * tax_factor;
                netpayable = total + tax;

                so.Discount = 0;
                so.VAT = tax;
                so.TotalBillAmount = total;
                so.NetPayableBill = netpayable;

                _context.SalesOrder.Update(so);
                await _context.SaveChangesAsync();

                TempData["msg"] = "Congratulations ! Your Order has been submitted successfully.";
                return RedirectToAction("confirmorder");
            }
            else
                return RedirectToAction("checkout");
        }

        public IActionResult ConfirmOrder()
        {
            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("checkout");
            }   
        }

        public IActionResult WishList()
        {
            return View();
        }
    }
}
