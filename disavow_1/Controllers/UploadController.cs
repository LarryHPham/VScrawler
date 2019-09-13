using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using disavow_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace disavow_1.Controllers
{
    public class UploadController : Controller
    {
        private readonly disavow_1Context _context;

        public UploadController(disavow_1Context context)
        {
            _context = context;
        }

        // GET: Disavow
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disavow.ToListAsync());
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<Models.IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            // TODO: some reason no files coming through
            Console.WriteLine(files.Count());

            foreach (Models.IFormFile file in files)
            {
                if (file.Length > 0 && file.ContentType == "xlsx")
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);

                        XmlDocument document = new XmlDocument();
                        document.Load(filePath);
                        Console.WriteLine("===============");
                        Console.WriteLine(document);
                        Console.WriteLine("===============");
                    }
                }
            }


            return Ok(new { count = files.Count, size });
        }

    }
}