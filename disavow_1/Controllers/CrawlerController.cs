﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Xml.XPath;
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
            var valid = false;
            HttpClient httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var metaNodes = htmlDocument.DocumentNode
                                .SelectNodes("//meta");

            Console.WriteLine(metaNodes);
            foreach (var mnodes in metaNodes)
            {
                var meta = mnodes.GetAttributeValue("name", "");
                Console.WriteLine(meta);
                if(meta == "robots")
                {
                    var content = mnodes.GetAttributeValue("content", "");
                    content = content.ToLower();
                    Console.WriteLine(content);
                    if(IsValid(content, "nofollow"))
                    {
                        valid = true;
                    }
                }
            }

            // STEP:1 check if head meta tags has nofollow
            // if no follow flag is set then this whole page is valid ignore rest of code
            if (valid)
            {
                return;
            }

            Console.WriteLine($"\nGetThirdPartyAPI for site:{url}");

            // STEP:2 Check if the url rating is above threshold (currently:50) whole page is valid if so
            int threshold = 50;
            List<Pages> pages = GetThirdPartyAPI(url);
            if (pages.Any())
            {
                Console.WriteLine($"Check page rankings if below threshold [{threshold}] then flag");
                foreach (Pages page in pages)
                {
                    if (page.ahrefs_rank > threshold)
                    {
                        Console.WriteLine($"PASS:{page.url} - RANK {page.ahrefs_rank}");
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine($"FAIL:{page.url} - RANK {page.ahrefs_rank}");
                        Console.WriteLine($"BAD RATING possible bad site - run nofollowcheck");
                    }
                }
            }
            Console.WriteLine("\n");

            if (valid)
            {
                return;
            }

            // STEP:3 Check if HtmlDocument nofollow is on the a:rel nodes
            Boolean siteFlag = NoFollowCheck(htmlDocument, search_pattern);
            Console.WriteLine($"siteFlag:{siteFlag}");

        }

        private static Boolean NoFollowCheck(HtmlDocument htmlDocument, string search_pattern)
        {
            HtmlNodeCollection htmlNodes = htmlDocument.DocumentNode
                                .SelectNodes("//a");
            foreach (HtmlNode tnodes in htmlNodes)
            {
                string href = tnodes.GetAttributeValue("href", "");
                Console.WriteLine($"Checking: {href}");
                // check if the regex pattern matches what we want to look for
                if (IsValid(href, search_pattern))
                {
                    Console.WriteLine($"VALID Pattern - {href}[{search_pattern}] checking rel tag for nofollow");
                    // check if the href has a rel tag for no follow and move of if it exists.
                    string relTag = tnodes.GetAttributeValue("rel", "");
                    relTag = relTag.ToLower();
                    if (IsValid(relTag, "nofollow"))
                    {
                        Console.WriteLine($"SUCCESS:{href} - rel:nofollow EXISTS moving on\n");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"FAIL:{href} - rel:nofollow does not exist NEED TO FLAG\n");
                    }

                    //Console.WriteLine("Get list of pages from ahref.com for ");
                    //List<Pages> pages = GetThirdPartyAPI(href);

                    //if (pages != null)
                    //{
                    //    Console.WriteLine("Change page rankings if below threshold then flag");
                    //    foreach (Pages page in pages)
                    //    {
                    //        if (page.ahrefs_rank < 50)
                    //        {
                    //            Console.WriteLine($"\nsite:{page.url}");
                    //            Console.WriteLine($"\nBAD RATING of {page.ahrefs_rank} need to flag as bad site");
                    //        }
                    //    }
                    //}
                }
                else
                {
                    Console.WriteLine($"INVALID Pattern: {href} DOES NOT MATCH {search_pattern}.\n");
                }
            }
            return false;
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
