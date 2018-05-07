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
    public class AVO5AllDataController : Controller
    {
        //
        // GET: /AVO5AllData/

        public ActionResult AllData()
        {
            return View(CreateListRoll());
        }


        private List<updateList> CreateListRoll()
        {
            List<updateList> AVO5data = new List<updateList>();

            DateTime dtTMP = DateTime.Now.AddDays(-100);

            using (l2dataAVO5Entities dbContext = new l2dataAVO5Entities())
            {
                AVO5data = dbContext.sv_Web_RollProdLine.
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

            return AVO5data;
        }

    }
}
