using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZenMovie.Models;
using ZenMovie.Tools;
using Newtonsoft.Json;

namespace ZenMovie.Controllers
{
    public class KategorilerController : Controller
    {
        public static List<Film> secilifilmler;

        // GET: Kategoriler
        public ActionResult Index(string secilenkategori="0", string imdbbas ="5.0", string imdbson ="8.0", string yilbas ="1990", string yilson = "2020")
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
            if (AnasayfaController.kategoriler == null)
            {
                AnasayfaController.kategoriler = apiData.GetApiDataKategori();
            }
            if (AnasayfaController.filmKategoriler == null)
            {
                AnasayfaController.filmKategoriler = apiData.GetApiDataFilmKategori();
            }

            imdbbas = imdbbas.Replace('.', ',');
            imdbson = imdbson.Replace('.', ',');

            if (Convert.ToInt32(secilenkategori) != 0)
            {
                List<FilmKategori> secilenkategoriler = AnasayfaController.filmKategoriler.Where(x => x.KategoriID == Convert.ToInt32(secilenkategori)).ToList();
                List<Film> sfilmler = AnasayfaController.filmler.Where(x => x.FilmYapimYili >= Convert.ToInt32(yilbas) && x.FilmYapimYili <= Convert.ToInt32(yilson) && x.IMDB >= Convert.ToSingle(imdbbas) && x.IMDB <= Convert.ToSingle(imdbson)).ToList();
                secilifilmler = (from film in sfilmler
                                 join secilen in secilenkategoriler on film.FilmID equals secilen.FilmID
                                 select film).ToList();
            }
            else
            {
                //secilifilmler = filmler.Where(x => Convert.ToInt32(x.FilmIMDB) >= Convert.ToInt32(imdbbas) && Convert.ToInt32(x.FilmIMDB) <= Convert.ToInt32(imdbson) && Convert.ToInt32(x.FilmYapimYili) >= Convert.ToInt32(yilbas) && Convert.ToInt32(x.FilmYapimYili) <= Convert.ToInt32(yilson)).ToList();
                secilifilmler = AnasayfaController.filmler.Where(x => x.IMDB >= Convert.ToSingle(imdbbas) && x.IMDB <= Convert.ToSingle(imdbson) && x.FilmYapimYili >= Convert.ToInt32(yilbas) && x.FilmYapimYili <= Convert.ToInt32(yilson)).ToList();
            }

            ViewBag.kategoriler = AnasayfaController.kategoriler;

            return View();
        }

        public JsonResult Filmler()
        {
            secilifilmler = secilifilmler.OrderByDescending(x => x.FilmID).ToList();
            return Json(secilifilmler, JsonRequestBehavior.AllowGet);
        }

    }
}