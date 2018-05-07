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
    public class AVO4DetailsController : Controller
    {
        //
        // GET: /AVO4Details/

        public ActionResult Details(string id)
        {
            List<ParametrList> List = new List<ParametrList>();
            if (id.IndexOf("OTM") > -1)
            {
                List = LoadParametrs(LoadTimeProductionOTM(id));
            }
            else
            {
                List = LoadParametrs(LoadTimeProduction(id));
            }

            ViewBag.StrL = ForJSLabel(List);
            ViewBag.StrExtr = ForJSParam(List, "Extract").Replace(",", ".");
            ViewBag.StrSpeed = ForJSParam(List, "Speed").Replace(",", ".");
            ViewBag.StrILH = ForJSParam(List, "ILH").Replace(",", ".");
            ViewBag.StrILL = ForJSParam(List, "ILL").Replace(",", ".");
            ViewBag.StrIRH = ForJSParam(List, "IRH").Replace(",", ".");
            ViewBag.StrIRL = ForJSParam(List, "IRL").Replace(",", ".");
            ViewBag.StrTLH = ForJSParam(List, "TLH").Replace(",", ".");
            ViewBag.StrTLL = ForJSParam(List, "TLL").Replace(",", ".");
            ViewBag.StrTRH = ForJSParam(List, "TRH").Replace(",", ".");
            ViewBag.StrTRL = ForJSParam(List, "TRL").Replace(",", ".");
            ViewBag.StrP1750 = ForJSParam(List, "P1750").Replace(",", ".");
            ViewBag.StrB800 = ForJSParam(List, "B800").Replace(",", ".");
            ViewBag.StrTexture = ForJSParam(List, "Texture").Replace(",", ".");
            ViewBag.StrTehP_right = ForJSParam(List, "TehP_right").Replace(",", ".");
            ViewBag.StrTehP_left = ForJSParam(List, "TehP_left").Replace(",", ".");

            ViewBag.StrNRoll = id;

            return View(List);
        }

        private string ForJSLabel(List<ParametrList> ListParam)
        {
            string str = "";
            for (int i = 0; i <= ListParam.Count() - 2; i++)
            {
                str = str + ListParam.ElementAt(i).Length + ";";
            }

            return str;
        }

        private string ForJSParam(List<ParametrList> ListParam, string ParamName)
        {
            string str = "";

            for (int i = 0; i <= ListParam.Count() - 2; i++)
            {
                switch (ParamName)
                {
                    case "Extract": str = str + ListParam.ElementAt(i).Extract + ";"; break;
                    case "Speed": str = str + ListParam.ElementAt(i).Speed + ";"; break;
                    case "ILH": str = str + ListParam.ElementAt(i).ILH + ";"; break;
                    case "ILL": str = str + ListParam.ElementAt(i).ILL + ";"; break;
                    case "IRH": str = str + ListParam.ElementAt(i).IRH + ";"; break;
                    case "IRL": str = str + ListParam.ElementAt(i).IRL + ";"; break;
                    case "TLH": str = str + ListParam.ElementAt(i).TLH + ";"; break;
                    case "TLL": str = str + ListParam.ElementAt(i).TLL + ";"; break;
                    case "TRH": str = str + ListParam.ElementAt(i).TRH + ";"; break;
                    case "TRL": str = str + ListParam.ElementAt(i).TRL + ";"; break;
                    case "P1750": str = str + ListParam.ElementAt(i).P1750 + ";"; break;
                    case "B800": str = str + ListParam.ElementAt(i).B800 + ";"; break;
                    case "Texture": str = str + ListParam.ElementAt(i).Texture + ";"; break;
                    case "TehP_right": str = str + ListParam.ElementAt(i).TehP_right + ";"; break;
                    case "TehP_left": str = str + ListParam.ElementAt(i).TehP_left + ";"; break;
                }

            }

            return str;
        }

        class StartFinish
        {
            public DateTime DateStart { get; set; }
            public DateTime DateFinish { get; set; }
            public string NStend { get; set; }
            public decimal ThStend { get; set; }
            public decimal WidStend { get; set; }
        }

        private List<StartFinish> LoadTimeProduction(string LocalNumber)
        {
            List<StartFinish> List = new List<StartFinish>();

            List.Add(new StartFinish
            {
                DateStart = DateTime.Now.AddMinutes(-1),
                DateFinish = DateTime.Now
            });

            using (l2dataAVO4Entities dbContext = new l2dataAVO4Entities())
            {
                List = dbContext.sv_Web_TimeProduction.
                        Where(p => p.LocalId == LocalNumber).
                        Select(p => new StartFinish()
                        {
                            DateStart = (DateTime)p.DateCreate,
                            DateFinish = (DateTime)p.DateProduction,
                            NStend = p.GlobalId,
                            ThStend = (decimal)p.Thickness,
                            WidStend = (decimal)p.Width
                        }).ToList();
            }

            ViewBag.StrNStend = List.ElementAt(0).NStend;
            ViewBag.StrThStend = List.ElementAt(0).ThStend.ToString();
            ViewBag.StrWidStend = List.ElementAt(0).WidStend.ToString();

            return List;
        }

        private List<StartFinish> LoadTimeProductionOTM(string LocalNumber)
        {
            List<OTMParametrs> ListOTMParam = new List<OTMParametrs>();
            string _tmpLocId = LocalNumber.Remove(6, LocalNumber.Length - 6);
            int _tmpNumOTM = Convert.ToInt32(LocalNumber.Remove(0, LocalNumber.Length - 1));

            using (l2dataAVO4Entities dbContext = new l2dataAVO4Entities())
            {
                ListOTMParam = dbContext.sp_web_LoadDetalOTM(_tmpLocId, _tmpNumOTM).
                        Select(p => new OTMParametrs()
                        {
                            LocalId = p.LocalId,
                            NumberOTM = (int)p.NumberOTM,
                            Defect = p.Defect,
                            LengthOffSet = (decimal)p.LengthOffSet,
                            DefectLength = (decimal)p.DefectLength,
                            DTEvents = (DateTime)p.DTEvents,
                            DTCreateRoll = (DateTime)p.DTCreateRoll,
                            DTProductionRoll = (DateTime)p.DTProductionRoll
                        }).ToList();
            }

            List<StartFinish> List = new List<StartFinish>();

            List.Add(new StartFinish
            {
                DateStart = DateTime.Now.AddMinutes(-1),
                DateFinish = DateTime.Now
            });

            using (l2l3interactionEntities dbContext = new l2l3interactionEntities())
            {
                List = dbContext.sp_WebP_LoadAVO4DTEventsOTM(ListOTMParam.ElementAt(0).DTCreateRoll,
                                                                ListOTMParam.ElementAt(0).DTProductionRoll,
                                                                ListOTMParam.ElementAt(0).LengthOffSet,
                                                                ListOTMParam.ElementAt(0).DefectLength).
                        Select(p => new StartFinish()
                        {
                            DateStart = (DateTime)p.Start,
                            DateFinish = (DateTime)p.Finish
                        }).ToList();
            }


            return List;
        }

        private List<ParametrList> LoadParametrs(List<StartFinish> DT)
        {
            List<ParametrList> ListParam = new List<ParametrList>();

            using (l2l3interactionEntities dbContext = new l2l3interactionEntities())
            {
                ListParam = dbContext.sp_WebP_LoadAVO4Parametrs(DT.ElementAt(0).DateStart, DT.ElementAt(0).DateFinish).
                        Select(p => new ParametrList()
                        {
                            UtcDateTime = (DateTime)p.UtcDateTime,
                            Length = (decimal)p.Length,
                            Speed = (decimal)p.Speed,
                            Extract = (decimal)p.Extract,
                            P1750 = (decimal)p.P1750,
                            B800 = (decimal)p.B800, //Индукция
                            ILH = (decimal)p.ILH,
                            ILL = (decimal)p.ILL,
                            IRH = (decimal)p.IRH,
                            IRL = (decimal)p.IRL,
                            Texture = (decimal)p.Texture,
                            TLH = (decimal)p.TLH,
                            TLL = (decimal)p.TLL,
                            TRH = (decimal)p.TRH,
                            TRL = (decimal)p.TRL,
                            TehP_right = (decimal)p.TehP_right,
                            TehP_left = (decimal)p.TehP_left
                        }).ToList();
            }

            return ListParam;
        }
    }
}
