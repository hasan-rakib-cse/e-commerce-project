using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IWebHostEnvironment _whe;

        public FileUploadController(IWebHostEnvironment whe)
        {
            _whe = whe;
        }

        public IActionResult Index()
        {
            string msg = "";
            if (TempData["msg"] != null)
            {
                msg = TempData["msg"].ToString();
            }
            ViewBag.msg = msg;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile filetoupload)
        {
            string msg = "";
            if (filetoupload != null)
            {
                if (filetoupload.Length > 0)
                {
                    string ext = Path.GetExtension(filetoupload.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg" || ext == ".webp")
                    {
                        string folder = "images/products";
                        string webroot = _whe.WebRootPath;
                        string filename = Path.GetFileName(filetoupload.FileName);

                        string fs = Path.Combine(webroot, folder, filename);

                        if (System.IO.File.Exists(fs))
                        {
                            string uf = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
                            uf += Path.GetExtension(filetoupload.FileName);
                            fs = Path.Combine(webroot, folder, uf);
                        }

                        using (var stream = new FileStream(fs, FileMode.Create))
                        {
                            await filetoupload.CopyToAsync(stream);
                            msg = "File Has Been Uploaded Successfully";
                        }
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
            TempData["msg"] = msg;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ShowFile()
        {
            string folder = "images/products";
            string webroot = _whe.WebRootPath;
            string mf = Path.Combine(webroot, folder);
            String[] files = Directory.GetFiles(mf);

            return View(files);
        }

    }
}
