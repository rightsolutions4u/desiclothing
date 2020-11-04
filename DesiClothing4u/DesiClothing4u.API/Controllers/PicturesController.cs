using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesiClothing4u.Common.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Hosting.Internal;

namespace DesiClothing4u.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : Controller
    {
        private readonly desiclothingContext _context;
        //private readonly IWebHostEnvironment _webHostEnvironment;

        public PicturesController(desiclothingContext context)
        {
            _context = context;
        }
   
        //public PicturesController(IWebHostEnvironment webHostEnvironment)
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //}
        // GET: PicturesController
        public ActionResult Index()
        {
            return View();
        }
        // GET: api/Pictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            return picture;
        }
        
        //GET: api/PostPicture 
        [HttpPost("PostPicture")]
        public async Task<ActionResult<IEnumerable<Picture>>> PostPicture(List<IFormFile> file, IFormCollection Id )
        {
            //string webRootPath = _webHostEnvironment.WebRootPath;
            //string webRootPath = WebHostEnvironment.WebRootPath;

            //string contentRootPath = _webHostEnvironment.ContentRootPath;
            
            string path = "c:/ProductImages/";
            var id = Id["Id"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Getting File Details
            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in file)
            {
                var Extension = Path.GetExtension(postedFile.FileName);
                var fileName = Path.GetFileName(postedFile.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;

                //Saving file to Folder
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
                //Saving data to database
                //VendorProduct vendorproduct = new VendorProduct();
                Picture picture = new Picture
                {
                    MimeType = "Jpg",
                    SeoFilename = fileName,
                    AltAttribute = "",
                    TitleAttribute = "",
                    IsNew = true,
                    VirtualPath = path
                };
                _context.Pictures.Add(picture);
                await _context.SaveChangesAsync();
                //code to insert picture and product mapping
                var pid = Convert.ToInt32(id.ToString());
                ProductPictureMapping map = new ProductPictureMapping
                {
                    ProductId = pid
                };
                map.PictureId = picture.Id;
                map.DisplayOrder = 0;
                _context.ProductPictureMappings.Add(map);
                await _context.SaveChangesAsync();
            }
            return await PostPictureJson(); 
        }
        public async Task<ActionResult<IEnumerable<Picture>>> PostPictureJson()
        {

            return await _context.Pictures.ToListAsync();
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        //private string GetPathAndFilename(string filename)
        //{
        //    return this.hostingEnvironment.WebRootPath + "\\uploads\\" + filename;
        //}

        // POST: PicturesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
      
        // POST: PicturesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: PicturesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
