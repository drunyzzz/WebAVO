using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAVO.Models
{
    public class StendList
    {
        public string NInStend { get; set; }
        public string NStend { get; set; }
        public string NRoll { get; set; }
        public decimal Length { get; set; }
        public decimal Weight { get; set; }
        public decimal Thickness { get; set; }
        public decimal Width { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateProduction { get; set; }
        public string DateUserAply { get; set; }
        public int Id { get; set; }
    }
}