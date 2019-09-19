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
using OfficeOpenXml;

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
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            // TODO: some reason no files coming through
            
            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            Console.WriteLine(System.IO.File.Exists(filePath));
            if (System.IO.File.Exists(filePath))
            {
                Console.WriteLine(filePath + " :DELETE");

                System.IO.File.Delete(filePath);
            }

            var list = new List<UserInfo>();

            foreach (IFormFile file in files)
            {

                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++)
                            {
                                //worksheet.Cells[row, 1].Value.ToString().Trim()
                                var data_one = "";
                                var data_two = "";

                                if (worksheet.Cells[row, 1].Value != null)
                                    data_one = worksheet.Cells[row, 1].Value.ToString().Trim();

                                if (worksheet.Cells[row, 2].Value != null)
                                    data_two = worksheet.Cells[row, 2].Value.ToString().Trim();

                                if(data_two != "" && data_two != "")
                                {

                                    list.Add(new UserInfo
                                        {
                                            UserName = data_one,
                                            Age = data_two,
                                        }
                                    );
                                }
                            }
                        }
                    }
                }
            }


            return Ok(new { count = files.Count, size, filePath, data = list });
        }

    }
}