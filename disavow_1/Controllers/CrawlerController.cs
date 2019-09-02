using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using disavow_1.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace disavow_1.Controllers
{
    [Route("disavow/[controller]")]
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
            Console.WriteLine($"Looking into site:{item.Url}");

            // TODO: need to add pattern to enter input into frontend and store that in DB

            // TODO: get input from FRONTEND to fil in Pattern in db to replace www.magneticsketch
            GetHtmlAsync(item.Url, "www.magneticsketch");

            // TODO: Return not the item but a better reponse for post call
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
                if(IsValid(href, search_pattern))
                {
                    Console.WriteLine("Get list of pages from ahref.com");
                    List<Pages> pages = GetThirdPartyAPI(href);

                    if (pages != null)
                    {
                        Console.WriteLine("Change page rankings if below threshold then flag");
                        foreach (Pages page in pages)
                        {
                            if(page.ahrefs_rank < 50)
                            {
                                Console.WriteLine($"\nsite:{page.url}");
                                Console.WriteLine($"\nBAD RATING of {page.ahrefs_rank} need to flag as bad site");
                            }
                        }
                    }
                }

            }
        }

        private static List<Pages> GetThirdPartyAPI(string url)
        {
            var thirdPartyApi = "https://apiv2.ahrefs.com/?token=5e939df83301fe6711378e35648802428c0c03a5&limit=100&output=json&from=ahrefs_rank&mode=exact";
            thirdPartyApi += "&target=" + url;

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.GetAsync(thirdPartyApi).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;

            TestApi data = JsonConvert.DeserializeObject<TestApi>(stringData);
            List<Pages> urls = data.pages;

            return urls;
        }

        // Checks for bad urls
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
