using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAVO.Models;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using System.IO;

namespace WebAVO.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View(CreateList());
        }

        private List<AlldataList> CreateList()
        {
            List<AlldataList> AllData = new List<AlldataList>();
            List<updateList> AVO3data = new List<updateList>();
            List<updateList> AVO4data = new List<updateList>();
            List<updateList> AVO5data = new List<updateList>();

            DateTime dtTMP = DateTime.Now.AddHours(-1200);

            using (l2dataAVO3Entities dbContext = new l2dataAVO3Entities())
            {
                AVO3data = dbContext.sv_Web_RollProdLine.
                        Where(p => p.DateCreate > dtTMP).
                        OrderByDescending(p => p.DateCreate).
                        Take(10).                                   //Аналог TOP 10 в SQL
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

            using (l2dataAVO4Entities dbContext = new l2dataAVO4Entities())
            {
                AVO4data = dbContext.sv_Web_RollProdLine.
                        Where(p => p.DateCreate > dtTMP).
                        OrderByDescending(p => p.DateCreate).
                        Take(10).                                   //Аналог TOP 10 в SQL
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

            using (l2dataAVO5Entities dbContext = new l2dataAVO5Entities())
            {
                AVO5data = dbContext.sv_Web_RollProdLine.
                        Where(p => p.DateCreate > dtTMP).
                        OrderByDescending(p => p.DateCreate).
                        Take(10).                                   //Аналог TOP 10 в SQL
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


            for (int i = 0; i <= 9; i++)
            {
                AllData.Add(new AlldataList
                {
                    AVO3_NumberLocalRoll = AVO3data.ElementAt(i).NumberLocalRoll,
                    AVO3_NumberRoll = AVO3data.ElementAt(i).NumberRoll,
                    AVO3_LengthRoll = AVO3data.ElementAt(i).LengthRoll,
                    AVO3_WeightRoll = AVO3data.ElementAt(i).WeightRoll,
                    AVO3_WidthRoll = AVO3data.ElementAt(i).WidthRoll,
                    AVO3_ThicknessRoll = AVO3data.ElementAt(i).ThicknessRoll,
                    AVO3_DateCreate = AVO3data.ElementAt(i).DateCreate,
                    AVO3_Status = AVO3data.ElementAt(i).Status,

                    AVO4_NumberLocalRoll = AVO4data.ElementAt(i).NumberLocalRoll,
                    AVO4_NumberRoll = AVO4data.ElementAt(i).NumberRoll,
                    AVO4_LengthRoll = AVO4data.ElementAt(i).LengthRoll,
                    AVO4_WeightRoll = AVO4data.ElementAt(i).WeightRoll,
                    AVO4_WidthRoll = AVO4data.ElementAt(i).WidthRoll,
                    AVO4_ThicknessRoll = AVO4data.ElementAt(i).ThicknessRoll,
                    AVO4_DateCreate = AVO4data.ElementAt(i).DateCreate,
                    AVO4_Status = AVO4data.ElementAt(i).Status,

                    AVO5_NumberLocalRoll = AVO5data.ElementAt(i).NumberLocalRoll,
                    AVO5_NumberRoll = AVO5data.ElementAt(i).NumberRoll,
                    AVO5_LengthRoll = AVO5data.ElementAt(i).LengthRoll,
                    AVO5_WeightRoll = AVO5data.ElementAt(i).WeightRoll,
                    AVO5_WidthRoll = AVO5data.ElementAt(i).WidthRoll,
                    AVO5_ThicknessRoll = AVO5data.ElementAt(i).ThicknessRoll,
                    AVO5_DateCreate = AVO5data.ElementAt(i).DateCreate,
                    AVO5_Status = AVO5data.ElementAt(i).Status
                });
            }
            return AllData;
        }

    }
}
