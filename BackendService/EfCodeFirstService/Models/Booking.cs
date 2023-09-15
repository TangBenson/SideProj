using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EfCodeFirstService.Models
{
    public class Booking
    {
        [Key]
        public string OrederNumber { get; set; } = "";
        public DateTime OrederDate { get; set; } = DateTime.Now;
    }
}