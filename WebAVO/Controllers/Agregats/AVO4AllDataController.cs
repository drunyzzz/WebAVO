using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAVO.Models;
using System.Linq.Expressions;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;


namespace WebAVO.Controllers.Agregats
{
    public class AVO4AllDataController : Controller
    {
        //
        // GET: /AVO4AllData/
        public ActionResult AllData()
        {
            return View(CreateListRoll());
        }


        private List<updateList> CreateListRoll()
        {
            List<updateList> AVO4data = new List<updateList>();

            DateTime dtTMP = DateTime.Now.AddDays(-100);

            using (l2dataAVO4Entities dbContext = new l2dataAVO4Entities())
            {
                AVO4data = dbContext.sv_Web_RollProdLine.
                        Where(p => p.DateCreate > dtTMP && p.LocalId != " ").
                        OrderByDescending(p => p.DateCreate).
                        Select(p => new updateList()
                        {
                            NumberLocalRoll = p.LocalId,
                            NumberRoll = p.GlobalId + " ( " + p.LocalId + " )",
                            LengthRoll = p.Length,
                            WeightRoll = p.Weight.Value,
                            WidthRoll = p.Width.Value,
                            ThicknessRoll = p.Thickness.Value,
                            DateCreate = (DateTime)p.DateCreate,
                            Status = p.Status
                        }).ToList();
            }

            return AVO4data;
        }

    }
}
