using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZenMovie.Models;
using ZenMovie.Tools;

namespace ZenMovie.Controllers
{
    public class AnasayfaController : Controller
    {
        public static List<Film> filmler;
        public static List<Dizi> diziler;
        public static List<Bolum> bolumler;
        public static List<Kategori> kategoriler;
        public static List<FilmKategori> filmKategoriler;

        // GET: Anasayfa
        public ActionResult Index()
        {
            if (filmler == null)
            {
                filmler= apiData.GetApiDataFilm();
                foreach(var item in filmler)
                {
                    item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace('.', ','));
                    item.FilmIMDB = item.FilmIMDB.Replace(',', '.');
                    var base64 = Convert.ToBase64String(item.FilmKapakFoto);
                    item.Resim = string.Format("data:image/jpg;base64,{0}", base64);
                }
            }
            if (diziler == null)
            {
                diziler = apiData.GetApiDataDizi();
                foreach (var item in diziler)
                {       
                    item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace('.', ','));
                    item.DiziIMDB = item.DiziIMDB.Replace(',', '.');
                    var base64 = Convert.ToBase64String(item.DiziKapakFoto);
                    item.Resim = string.Format("data:image/jpg;base64,{0}", base64);
                }
            }
            if (bolumler == null)
            {
                bolumler = apiData.GetApiDataBolum();
            }
            if (kategoriler == null)
            {
                kategoriler = apiData.GetApiDataKategori();
            }
            if (filmKategoriler == null)
            {
                filmKategoriler = apiData.GetApiDataFilmKategori();
            }

            ViewBag.diziler = diziler;
            ViewBag.filmler = filmler;
            ViewBag.kategoriler = kategoriler;

            ViewBag.soneklenenfilmler = filmler.OrderByDescending(x => x.FilmID).Take(18).ToList();
            ViewBag.cokizlenenfilmler = filmler.OrderByDescending(x => x.FilmIzlenmeSayisi).Take(18).ToList();
            ViewBag.enbegenilenfilmler = filmler.OrderByDescending(x => x.FilmBegeniOrani).Take(18).ToList();

            ViewBag.soneklenendiziler = diziler.OrderByDescending(x => x.DiziID).Take(18).ToList();
            ViewBag.cokizlenendiziler = diziler.OrderByDescending(x => x.DiziIzlenmeSayisi).Take(18).ToList();
            ViewBag.enbegenilendiziler = diziler.OrderByDescending(x => x.DiziBegeniOrani).Take(18).ToList();
            
            return View();
        }

    }
}