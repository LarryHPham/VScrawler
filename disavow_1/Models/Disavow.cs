using System;
using System.ComponentModel.DataAnnotations;

namespace disavow_1.Models
{
    public class Disavow
    {
        public int Id { get; set; }
        public string Url { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckedDate { get; set; }
        public bool NoFollowNoLink { get; set; }
        public bool Parsed { get; set; }
    }
}