using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceProject.Data;
using ECommerceProject.Models;

namespace ECommerceProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _whe;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment whe)
        {
            _context = context;
            _whe = whe;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Brand).Include(p => p.Configaration).Include(p => p.ProductModel).Include(p => p.SubCategory).Include(p => p.Unit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Configaration)
                .Include(p => p.ProductModel)
                .Include(p => p.SubCategory)
                .Include(p => p.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "BrandName");
            ViewData["ConfigarationId"] = new SelectList(_context.Configaration, "Id", "ConfigarationName");
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "Id", "ModelName");
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "Id", "SubCategoryName");
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "UnitName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ShortDescription,LongDescription,ProductCode,SalePrice,Weight,WarnPoint,ProductWarranty,VAT,DiscountInPercent,DiscountAmount,IsActive,CreatedDate,UpdatedDate,SubCategoryId,BrandId,UnitId,ProductModelId,ConfigarationId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "BrandName", product.BrandId);
            ViewData["ConfigarationId"] = new SelectList(_context.Configaration, "Id", "ConfigarationName", product.ConfigarationId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "Id", "ModelName", product.ProductModelId);
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "Id", "SubCategoryName", product.SubCategoryId);
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "UnitName", product.UnitId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "BrandName", product.BrandId);
            ViewData["ConfigarationId"] = new SelectList(_context.Configaration, "Id", "ConfigarationName", product.ConfigarationId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "Id", "ModelName", product.ProductModelId);
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "Id", "SubCategoryName", product.SubCategoryId);
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "UnitName", product.UnitId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ShortDescription,LongDescription,ProductCode,SalePrice,Weight,WarnPoint,ProductWarranty,VAT,DiscountInPercent,DiscountAmount,IsActive,CreatedDate,UpdatedDate,SubCategoryId,BrandId,UnitId,ProductModelId,ConfigarationId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "BrandName", product.BrandId);
            ViewData["ConfigarationId"] = new SelectList(_context.Configaration, "Id", "ConfigarationName", product.ConfigarationId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "Id", "ModelName", product.ProductModelId);
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "Id", "SubCategoryName", product.SubCategoryId);
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "UnitName", product.UnitId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Configaration)
                .Include(p => p.ProductModel)
                .Include(p => p.SubCategory)
                .Include(p => p.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        #region .........:::: Custom Code Block ::::.........

        public async Task<IActionResult> UploadImage(int id)
        {
            if (id <= 0) { return NotFound(); }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(int id, IFormFile filetoupload)
        {
            if(id <= 0) { return NotFound(); }

            var product = await _context.Product.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            string msg = "";

            if (filetoupload != null)
            {
                if(filetoupload.Length > 0)
                {
                    string ext = Path.GetExtension(filetoupload.FileName);
                    if(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".webp")
                    {
                        string folder = "images/UploadedImages";
                        string webroot = _whe.WebRootPath;
                        string filename = Path.GetFileName(filetoupload.FileName);

                        string file_code = getImageName(id);
                        string fs = Path.Combine(webroot, folder, file_code+ext);

                        using (var stream = new FileStream(fs, FileMode.Create))
                        {
                            await filetoupload.CopyToAsync(stream);
                            msg = "File Has Been Uploaded Successfully";
                        }

                        ProductImage p = new ProductImage();
                        p.ProductId = id;
                        p.ImageName = filename;
                        p.ImageCode = file_code + ext;
                        p.ImageExtension = ext;

                        _context.ProductImage.Add(p);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        msg = "File Extension/ Format is not Allowed to Upload";
                    }
                }
                else
                {
                    msg = "File Size Could Not be Zero";
                }
            }
            else
            {
                msg = "Please Select a File to Upload";
            }
            ViewBag.msg = msg;
            return View(product);

        }


        // For different image name according to product id
        private string getImageName(int id)
        {
            int count_image = _context.ProductImage.Where(x => x.ProductId == id).Count();
            string imgname = "PIMG" + id.ToString().Trim().PadLeft(12, 'X') + (count_image + 1).ToString().Trim().PadLeft(2, '0');
            return imgname;
        }




        #endregion

    }
}
