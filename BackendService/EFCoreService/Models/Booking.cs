using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreService.Models
{
    public class Booking
    {
        [Key]
        public string OrederNumber { get; set; } = "";
        public DateTime OrederDate { get; set; } = DateTime.Now;
    }
}