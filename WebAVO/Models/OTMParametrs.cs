using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAVO.Models
{
    public class OTMParametrs
    {
        public string LocalId { get; set; }
        
        public int NumberOTM { get; set; }

        public string Defect { get; set; }

        public decimal LengthOffSet { get; set; }

        public decimal DefectLength { get; set; }

        public DateTime DTEvents { get; set; }

        public DateTime DTCreateRoll { get; set; }

        public DateTime DTProductionRoll { get; set; }
    }
}