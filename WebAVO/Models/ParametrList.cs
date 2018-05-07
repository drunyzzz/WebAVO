using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAVO.Models
{
    public class ParametrList
    {
        public DateTime UtcDateTime { get; set; }
        public decimal Length { get; set; }
        public decimal Speed { get; set; }
        public decimal Extract { get; set; }
        public decimal P1750 { get; set; }
        public decimal B800 { get; set; }
        public decimal ILH { get; set; }
        public decimal ILL { get; set; }
        public decimal IRH { get; set; }
        public decimal IRL { get; set; }
        public decimal Texture { get; set; }
        public decimal TLH { get; set; }
        public decimal TLL { get; set; }
        public decimal TRH { get; set; }
        public decimal TRL { get; set; }
        public decimal TehP_right { get; set; }
        public decimal TehP_left { get; set; }
    }
}