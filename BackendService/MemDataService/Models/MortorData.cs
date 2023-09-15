using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemDataService.Models
{
    public class MortorData
    {
        [Key]
        public string? CarNo { get; set; }
        public Double? Device3TBA { get; set; } = 0;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public short? GiveMin { get; set; } = 0;
    }
}