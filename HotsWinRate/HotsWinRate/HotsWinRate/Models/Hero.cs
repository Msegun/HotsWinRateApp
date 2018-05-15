using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotsWinRate.Models
{
    public class Hero
    {
        [Key]
        public int Id { get; set; }
        public string Namaewa { get; set; }
        public string Type { get; set; }
        public int Win { get; set; }
        public int Played { get; set; }
    }
}
