using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAVO.Models
{
    public class updateList
    {
        public int id { get; set; }

        public string NumberLocalRoll { get; set; }

        public string NumberRoll { get; set; }

        public decimal LengthRoll { get; set; }

        public decimal WeightRoll { get; set; }

        public decimal WidthRoll { get; set; }

        public decimal ThicknessRoll { get; set; }

        public DateTime DateCreate { get; set; }

        public string Status { get; set; }
    }
}