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
    public class AVO4StendController : Controller
    {
        //
        // GET: /AVO4Stend/

        public ActionResult Data(string id)
        {
            List<StendList> list = new List<StendList>();
            List<ParametrStendList> plist = new List<ParametrStendList>();

            list = loadRolInStend(id);
            testEnebledRoll(list);
            plist = loadParSTList(id);

            ViewBag.NStend = id;
            ViewBag.NSKO_P1750 = SKO(id, "P1750");
            ViewBag.NSKO_B800 = SKO(id, "B800");

            return View();
        }

        private List<StendList> loadRolInStend(string NStend)
        {
            List<StendList> list = new List<StendList>();

            using (l2dataAVO4Entities dbContext = new l2dataAVO4Entities())
            {
                list = dbContext.sp_web_RollInStend(NStend)
                    .Select(p => new StendList
                    {
                        NInStend = p.NInStend,
                        NStend = p.Stend,
                        NRoll = p.LocalId,
                        Length = p.Length,
                        Weight = (decimal)p.Weight,
                        Thickness = (decimal)p.Thickness,
                        DateCreate = p.DateCreate,
                        DateProduction = (DateTime)p.DateProduction,
                    }).ToList();
            }

            return list;
        }

        private bool testEnebledRoll(List<StendList> listStend)
        {
            try
            {
                using (l2l3interactionEntities dbContext = new l2l3interactionEntities())
                {
                    for (int i = 0; i <= listStend.Count() - 1; i++)
                    {
                        string _NRoll = listStend.ElementAt(i).NRoll;
                        string _NStend = listStend.ElementAt(i).NStend;
                        string _NInStend = listStend.ElementAt(i).NInStend;
                        DateTime _DateCreate = listStend.ElementAt(i).DateCreate;
                        DateTime _DateProduction = listStend.ElementAt(i).DateProduction;

                        var cntD = dbContext.DiagramToRollAVO.Where(p => p.RollNumber == _NRoll && p.Agregat == "AVO4").Count();

                        if (cntD == 0)
                        {
                            var crGlDiagr = dbContext.sp_TransportDiagramsAVO4(_NRoll,
                                                                                _NStend,
                                                                                _NInStend,
                                                                                _DateCreate,
                                                                                _DateProduction);

                            cntD = dbContext.DiagramToRollAVO.Where(p => p.RollNumber == _NRoll && p.Agregat == "AVO4").Count();
                        }

                        decimal _SKORP1750 = SKORoll(listStend.ElementAt(i).NRoll, "P1750");
                        decimal UstavkaP1750 = Convert.ToDecimal(0.05);
                        decimal UstavkaB800 = Convert.ToDecimal(0.05);
                        string _colorTextP1750 = "";
                        string _colorTextB800 = "";

                        if (_SKORP1750 <= 0)
                        {
                            _colorTextP1750 = "text-danger";
                        }
                        else if (_SKORP1750 > UstavkaP1750)
                        {
                            _colorTextP1750 = "text-warning";
                        }

                        decimal _SKORB800 = SKORoll(listStend.ElementAt(i).NRoll, "B800");
                        if (_SKORB800 <= 0)
                        {
                            _colorTextB800 = "text-danger";
                        }
                        else if (_SKORB800 > UstavkaB800)
                        {
                            _colorTextB800 = "text-warning";
                        }

                        switch (_NInStend)
                        {
                            case "1": @ViewBag.NRoll1 = listStend.ElementAt(i).NRoll; @ViewBag.DataRoll1 = "; длина(м.): " +
                                                        listStend.ElementAt(i).Length.ToString().Remove(listStend.ElementAt(i).Length.ToString().IndexOf(".")) + "; вес(кг.): " +
                                                        listStend.ElementAt(i).Weight.ToString().Remove(listStend.ElementAt(i).Weight.ToString().IndexOf(".")) + "; СКО P1750: " +
                                                        _SKORP1750 + "; СКО B800: " +
                                                        _SKORB800;
                                                        @ViewBag.SKO_P1750_1 = _SKORP1750;
                                                        @ViewBag.SKO_B800_1 = _SKORB800;
                                                        @ViewBag.colorP1750_r1 = _colorTextP1750;
                                                        @ViewBag.colorB800_r1 = _colorTextB800;
                                                        break;
                            case "2": @ViewBag.NRoll2 = listStend.ElementAt(i).NRoll; @ViewBag.DataRoll2 = "; длина(м.): " +
                                                        listStend.ElementAt(i).Length.ToString().Remove(listStend.ElementAt(i).Length.ToString().IndexOf(".")) + "; вес(кг.): " +
                                                        listStend.ElementAt(i).Weight.ToString().Remove(listStend.ElementAt(i).Weight.ToString().IndexOf(".")) + "; СКО P1750: " +
                                                        _SKORP1750 + "; СКО B800: " +
                                                        _SKORB800;
                                                        @ViewBag.SKO_P1750_2 = _SKORP1750;
                                                        @ViewBag.SKO_B800_2 = _SKORB800;
                                                        @ViewBag.colorP1750_r2 = _colorTextP1750;
                                                        @ViewBag.colorB800_r2 = _colorTextB800;
                                                        break;
                            case "3": @ViewBag.NRoll3 = listStend.ElementAt(i).NRoll; @ViewBag.DataRoll3 = "; длина(м.): " +
                                                        listStend.ElementAt(i).Length.ToString().Remove(listStend.ElementAt(i).Length.ToString().IndexOf(".")) + "; вес(кг.): " +
                                                        listStend.ElementAt(i).Weight.ToString().Remove(listStend.ElementAt(i).Weight.ToString().IndexOf(".")) + "; СКО P1750: " +
                                                        _SKORP1750 + "; СКО B800: " +
                                                        _SKORB800;
                                                        @ViewBag.SKO_P1750_3 = _SKORP1750;
                                                        @ViewBag.SKO_B800_3 = _SKORB800;
                                                        @ViewBag.colorP1750_r3 = _colorTextP1750;
                                                        @ViewBag.colorB800_r3 = _colorTextB800;
                                                        break;
                            case "4": @ViewBag.NRoll4 = listStend.ElementAt(i).NRoll; @ViewBag.DataRoll4 = "; длина(м.): " +
                                                        listStend.ElementAt(i).Length.ToString().Remove(listStend.ElementAt(i).Length.ToString().IndexOf(".")) + "; вес(кг.): " +
                                                        listStend.ElementAt(i).Weight.ToString().Remove(listStend.ElementAt(i).Weight.ToString().IndexOf(".")) + "; СКО P1750: " +
                                                        _SKORP1750 + "; СКО B800: " +
                                                        _SKORB800;
                                                        @ViewBag.SKO_P1750_4 = _SKORP1750;
                                                        @ViewBag.SKO_B800_4 = _SKORB800;
                                                        @ViewBag.colorP1750_r4 = _colorTextP1750;
                                                        @ViewBag.colorB800_r4 = _colorTextB800;
                                                        break;
                            case "5": @ViewBag.NRoll5 = listStend.ElementAt(i).NRoll; @ViewBag.DataRoll5 = "; длина(м.): " +
                                                        listStend.ElementAt(i).Length.ToString().Remove(listStend.ElementAt(i).Length.ToString().IndexOf(".")) + "; вес(кг.): " +
                                                        listStend.ElementAt(i).Weight.ToString().Remove(listStend.ElementAt(i).Weight.ToString().IndexOf(".")) + "; СКО P1750: " +
                                                        _SKORP1750 + "; СКО B800: " +
                                                        _SKORB800;
                                                        @ViewBag.SKO_P1750_5 = _SKORP1750;
                                                        @ViewBag.SKO_B800_5 = _SKORB800;
                                                        @ViewBag.colorP1750_r5 = _colorTextP1750;
                                                        @ViewBag.colorB800_r5 = _colorTextB800;
                                                        break;
                            case "6": @ViewBag.NRoll6 = listStend.ElementAt(i).NRoll; @ViewBag.DataRoll6 = "; длина(м.): " +
                                                        listStend.ElementAt(i).Length.ToString().Remove(listStend.ElementAt(i).Length.ToString().IndexOf(".")) + "; вес(кг.): " +
                                                        listStend.ElementAt(i).Weight.ToString().Remove(listStend.ElementAt(i).Weight.ToString().IndexOf(".")) + "; СКО P1750: " +
                                                        _SKORP1750 + "; СКО B800: " +
                                                        _SKORB800;
                                                        @ViewBag.SKO_P1750_6 = _SKORP1750;
                                                        @ViewBag.SKO_B800_6 = _SKORB800;
                                                        @ViewBag.colorP1750_r6 = _colorTextP1750;
                                                        @ViewBag.colorB800_r6 = _colorTextB800;
                                                        break;
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        private List<ParametrStendList> loadParSTList(string NStend)
        {
            List<ParametrStendList> PSL = new List<ParametrStendList>();

            using (l2l3interactionEntities dbContext = new l2l3interactionEntities())
            {
                var list = dbContext.DiagramToRollAVO.Where(p => p.StendNumber == NStend && p.Agregat == "AVO4")
                                .OrderBy(p => p.UtcDateTime).ToList();
                int _id = 0;

                string _strL_r1 = "";
                string _strL_r2 = "";
                string _strL_r3 = "";
                string _strL_r4 = "";
                string _strL_r5 = "";
                string _strL_r6 = "";

                string _strExtr_r1 = "";
                string _strExtr_r2 = "";
                string _strExtr_r3 = "";
                string _strExtr_r4 = "";
                string _strExtr_r5 = "";
                string _strExtr_r6 = "";

                string _strTexture_r1 = "";
                string _strTexture_r2 = "";
                string _strTexture_r3 = "";
                string _strTexture_r4 = "";
                string _strTexture_r5 = "";
                string _strTexture_r6 = "";

                string _strP1750_r1 = "";
                string _strP1750_r2 = "";
                string _strP1750_r3 = "";
                string _strP1750_r4 = "";
                string _strP1750_r5 = "";
                string _strP1750_r6 = "";

                string _strB800_r1 = "";
                string _strB800_r2 = "";
                string _strB800_r3 = "";
                string _strB800_r4 = "";
                string _strB800_r5 = "";
                string _strB800_r6 = "";

                for (int i = 0; i <= list.Count() - 1; i++)
                {
                    PSL.Add(new ParametrStendList
                    {
                        id = _id++,
                        RollNumber = list.ElementAt(i).RollNumber,
                        StendNumber = list.ElementAt(i).StendNumber,
                        NInStend = list.ElementAt(i).NInStend,
                        UtcDateTime = list.ElementAt(i).UtcDateTime,
                        p_ln = (decimal)list.ElementAt(i).ln,
                        p_speed = (decimal)list.ElementAt(i).p_speed,
                        p_extract = (decimal)list.ElementAt(i).p_extract,
                        p_P1750 = (decimal)list.ElementAt(i).p_P1750,
                        p_B800 = (decimal)list.ElementAt(i).p_B800,
                        p_ILH = (decimal)list.ElementAt(i).p_ILH,
                        p_ILL = (decimal)list.ElementAt(i).p_ILH,
                        p_IRH = (decimal)list.ElementAt(i).p_ILH,
                        p_IRL = (decimal)list.ElementAt(i).p_ILH,
                        p_Texture = (decimal)list.ElementAt(i).p_texture,
                        p_TLH = (decimal)list.ElementAt(i).p_TLH,
                        p_TLL = (decimal)list.ElementAt(i).p_TLH,
                        p_TRH = (decimal)list.ElementAt(i).p_TLH,
                        p_TRL = (decimal)list.ElementAt(i).p_TLH,
                        p_TehP_right = (decimal)list.ElementAt(i).p_TehP_righ,
                        p_TehP_left = (decimal)list.ElementAt(i).p_TehP_left
                    });


                    switch (list.ElementAt(i).NInStend)
                    {
                        case "1": _strL_r1 = _strL_r1 + list.ElementAt(i).ln.ToString() + ";";
                            _strExtr_r1 = _strExtr_r1 + list.ElementAt(i).p_extract.ToString() + ";";
                            _strTexture_r1 = _strTexture_r1 + list.ElementAt(i).p_texture.ToString() + ";";
                            _strP1750_r1 = _strP1750_r1 + list.ElementAt(i).p_P1750.ToString() + ";";
                            _strB800_r1 = _strB800_r1 + list.ElementAt(i).p_B800.ToString() + ";";
                            break;
                        case "2": _strL_r2 = _strL_r2 + list.ElementAt(i).ln.ToString() + ";";
                            _strExtr_r2 = _strExtr_r2 + list.ElementAt(i).p_extract.ToString() + ";";
                            _strTexture_r2 = _strTexture_r2 + list.ElementAt(i).p_texture.ToString() + ";";
                            _strP1750_r2 = _strP1750_r2 + list.ElementAt(i).p_P1750.ToString() + ";";
                            _strB800_r2 = _strB800_r2 + list.ElementAt(i).p_B800.ToString() + ";";
                            break;
                        case "3": _strL_r3 = _strL_r3 + list.ElementAt(i).ln.ToString() + ";";
                            _strExtr_r3 = _strExtr_r3 + list.ElementAt(i).p_extract.ToString() + ";";
                            _strTexture_r3 = _strTexture_r3 + list.ElementAt(i).p_texture.ToString() + ";";
                            _strP1750_r3 = _strP1750_r3 + list.ElementAt(i).p_P1750.ToString() + ";";
                            _strB800_r3 = _strB800_r3 + list.ElementAt(i).p_B800.ToString() + ";";
                            break;
                        case "4": _strL_r4 = _strL_r4 + list.ElementAt(i).ln.ToString() + ";";
                            _strExtr_r4 = _strExtr_r4 + list.ElementAt(i).p_extract.ToString() + ";";
                            _strTexture_r4 = _strTexture_r4 + list.ElementAt(i).p_texture.ToString() + ";";
                            _strP1750_r4 = _strP1750_r4 + list.ElementAt(i).p_P1750.ToString() + ";";
                            _strB800_r4 = _strB800_r4 + list.ElementAt(i).p_B800.ToString() + ";";
                            break;
                        case "5": _strL_r5 = _strL_r5 + list.ElementAt(i).ln.ToString() + ";";
                            _strExtr_r5 = _strExtr_r5 + list.ElementAt(i).p_extract.ToString() + ";";
                            _strTexture_r5 = _strTexture_r5 + list.ElementAt(i).p_texture.ToString() + ";";
                            _strP1750_r5 = _strP1750_r5 + list.ElementAt(i).p_P1750.ToString() + ";";
                            _strB800_r5 = _strB800_r5 + list.ElementAt(i).p_B800.ToString() + ";";
                            break;
                        case "6": _strL_r6 = _strL_r6 + list.ElementAt(i).ln.ToString() + ";";
                            _strExtr_r6 = _strExtr_r6 + list.ElementAt(i).p_extract.ToString() + ";";
                            _strTexture_r6 = _strTexture_r6 + list.ElementAt(i).p_texture.ToString() + ";";
                            _strP1750_r6 = _strP1750_r6 + list.ElementAt(i).p_P1750.ToString() + ";";
                            _strB800_r6 = _strB800_r6 + list.ElementAt(i).p_B800.ToString() + ";";
                            break;

                    }
                }
                ViewBag.StrL_r1 = _strL_r1;
                ViewBag.StrL_r2 = _strL_r2;
                ViewBag.StrL_r3 = _strL_r3;
                ViewBag.StrL_r4 = _strL_r4;
                ViewBag.StrL_r5 = _strL_r5;
                ViewBag.StrL_r6 = _strL_r6;

                ViewBag.StrExtr_r1 = _strExtr_r1.Replace(",", ".");
                ViewBag.StrExtr_r2 = _strExtr_r2.Replace(",", ".");
                ViewBag.StrExtr_r3 = _strExtr_r3.Replace(",", ".");
                ViewBag.StrExtr_r4 = _strExtr_r4.Replace(",", ".");
                ViewBag.StrExtr_r5 = _strExtr_r5.Replace(",", ".");
                ViewBag.StrExtr_r6 = _strExtr_r6.Replace(",", ".");

                ViewBag.StrTexture_r1 = _strTexture_r1.Replace(",", ".");
                ViewBag.StrTexture_r2 = _strTexture_r2.Replace(",", ".");
                ViewBag.StrTexture_r3 = _strTexture_r3.Replace(",", ".");
                ViewBag.StrTexture_r4 = _strTexture_r4.Replace(",", ".");
                ViewBag.StrTexture_r5 = _strTexture_r5.Replace(",", ".");
                ViewBag.StrTexture_r6 = _strTexture_r6.Replace(",", ".");

                ViewBag.StrP1750_r1 = _strP1750_r1.Replace(",", ".");
                ViewBag.StrP1750_r2 = _strP1750_r2.Replace(",", ".");
                ViewBag.StrP1750_r3 = _strP1750_r3.Replace(",", ".");
                ViewBag.StrP1750_r4 = _strP1750_r4.Replace(",", ".");
                ViewBag.StrP1750_r5 = _strP1750_r5.Replace(",", ".");
                ViewBag.StrP1750_r6 = _strP1750_r6.Replace(",", ".");

                ViewBag.StrB800_r1 = _strB800_r1.Replace(",", ".");
                ViewBag.StrB800_r2 = _strB800_r2.Replace(",", ".");
                ViewBag.StrB800_r3 = _strB800_r3.Replace(",", ".");
                ViewBag.StrB800_r4 = _strB800_r4.Replace(",", ".");
                ViewBag.StrB800_r5 = _strB800_r5.Replace(",", ".");
                ViewBag.StrB800_r6 = _strB800_r6.Replace(",", ".");
            }

            return PSL;
        }

        private string SKO(string NStend, string TypeSKO)
        {
            string SKO = "";

            using (l2l3interactionEntities dbContext = new l2l3interactionEntities())
            {
                if (TypeSKO == "P1750")
                {
                    var q = dbContext.sp_WebP_MulSKO_P1750_FromStend(NStend).Select(p => p.STDEV).ToList();
                    SKO = q.ElementAt(0).ToString();
                }
                else if (TypeSKO == "B800")
                {
                    var q = dbContext.sp_WebP_MulSKO_B800_FromStend(NStend).Select(p => p.STDEV).ToList();
                    SKO = q.ElementAt(0).ToString();
                }
                else
                {
                    SKO = "0";
                }
            }

            return SKO;
        }

        private decimal SKORoll(string NRoll, string TypeSKO)
        {
            decimal SKO = 0;

            using (l2l3interactionEntities dbContext = new l2l3interactionEntities())
            {
                if (TypeSKO == "P1750")
                {
                    var q = dbContext.sp_WebP_MulSKO_P1750_FromRoll(NRoll).Select(p => p.STDEV).ToList();
                    SKO = Convert.ToDecimal(q.ElementAt(0));
                }
                else if (TypeSKO == "B800")
                {
                    var q = dbContext.sp_WebP_MulSKO_B800_FromRoll(NRoll).Select(p => p.STDEV).ToList();
                    SKO = Convert.ToDecimal(q.ElementAt(0));
                }
                else
                {
                    SKO = 0;
                }
            }

            return SKO;
        }

    }
}
