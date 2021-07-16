using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenMovie.Models;
using ZenMovie.Tools;

namespace ZenMovie.Controllers
{
    public class AramaController : Controller
    {
        public static string aranan;

        [HttpPost]
        public ActionResult Index(string girilenkelime)
        {
            if (AnasayfaController.filmler == null)
            {
                AnasayfaController.filmler = apiData.GetApiDataFilm();
                foreach (var item in AnasayfaController.filmler)
                {
                    item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace('.', ','));
                    var base64 = Convert.ToBase64String(item.FilmKapakFoto);
                    item.Resim = string.Format("data:image/jpg;base64,{0}", base64);
                }
            }
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

            aranan = girilenkelime;
            return View();
        }

        public JsonResult FilmleriGoster()
        {
            List<Film> arananfilmler = (from f in AnasayfaController.filmler
                                        where f.FilmBaslik.ToLower().StartsWith(aranan.ToLower()) || f.FilmBaslik.ToLower().Contains(aranan.ToLower())
                                        select f).ToList();

            return Json(arananfilmler, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DizileriGoster()
        {
            List<Dizi> aranandiziler = (from d in AnasayfaController.diziler
                                  where d.DiziBaslik.ToLower().StartsWith(aranan.ToLower()) || d.DiziBaslik.ToLower().Contains(aranan.ToLower())
                                  select d).ToList();

            return Json(aranandiziler, JsonRequestBehavior.AllowGet);
        }
    }
}