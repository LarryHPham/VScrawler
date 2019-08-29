using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using disavow_1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace disavow_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlerController : ControllerBase
    {
        private readonly disavow_1Context _context;

        public CrawlerController(disavow_1Context context)
        {
            _context = context;
        }

        // POST api/<controller>/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Disavow>> Post(int id)
        {
            var item = await _context.Disavow.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            GetHtmlAsync("https://www.victoriavanle.com/", "magneticsketch");

            return item;
        }

        private static async void GetHtmlAsync(string url, string search_pattern)
        {
            var httpClient = new HttpClient();

            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var htmlNodes = htmlDocument.DocumentNode
                                .SelectNodes("//a");
            foreach (var tnodes in htmlNodes)
            {
                var href = tnodes.GetAttributeValue("href", "");
                Console.WriteLine("href:" + href + "=>" + IsValid(href, search_pattern));

            }
        }

        static bool IsValid(string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Disavow>> Get(int id)
        {
            Console.WriteLine("ID:" + id);
            var item = await _context.Disavow.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return NotFound();
        }

    }
}
