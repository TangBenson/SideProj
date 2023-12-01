using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GQLService.Models
{
    public class Car
    {
        public string? CarNo { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public Int16? Price { get; set; }
        public string? CID { get; set; }
        /// <summary>
        /// 車型，如 yaris
        /// </summary>
        /// <value></value>
        public string? Type { get; set; }
        public string? Brand { get; set; }
        /// <summary>
        /// 車種，如 跑車、牛車....
        /// </summary>
        /// <value></value>
        public string? Type2 { get; set; }
        
    }
}