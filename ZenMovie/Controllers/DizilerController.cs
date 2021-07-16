using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenMovie.Models;
using ZenMovie.Tools;

namespace ZenMovie.Controllers
{
    public class DizilerController : Controller
    {
        public static List<Dizi> secilidiziler;
        // GET: Diziler
        public ActionResult Index(string imdbbas = "5.0", string imdbson = "8.0", string yilbas = "1990", string yilson = "2020")
        {
            if (AnasayfaController.diziler == null)
            {
                AnasayfaController.diziler = apiData.GetApiDataDizi();
                foreach (var item in AnasayfaController.diziler)
                {
                    item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace('.', ','));
                    var base64 = Convert.ToBase64String(item.DiziKapakFoto);
                    item.Resim = string.Format("data:image/jpg;base64,{0}", base64);
                }
            }

            imdbbas = imdbbas.Replace('.', ',');
            imdbson = imdbson.Replace('.', ',');

            secilidiziler = AnasayfaController.diziler.Where(x => x.IMDB >= Convert.ToSingle(imdbbas) && x.IMDB <= Convert.ToSingle(imdbson) && x.DiziBaslangicYili >= Convert.ToInt32(yilbas) && x.DiziBaslangicYili <= Convert.ToInt32(yilson)).ToList();

            ViewBag.dizi = secilidiziler;
            return View();
        }

        public JsonResult Listele()
        {
            secilidiziler = secilidiziler.OrderByDescending(x => x.DiziID).ToList();
            return Json(secilidiziler, JsonRequestBehavior.AllowGet);
        }
    }
}