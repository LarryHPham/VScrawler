using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace disavow_1.Models
{
    public class TestApi
    {
        public List<Pages> pages { get; set; }
    }

    public class Pages
    {
        public string url { get; set; }
        public int ahrefs_rank { get; set; }
    }
}