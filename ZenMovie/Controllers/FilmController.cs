using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoLibrary;
using ZenMovie.Models;
using ZenMovie.Tools;

namespace ZenMovie.Controllers
{
    public class FilmController : Controller
    {
        string url = "";
        //string link = "";
        
        public ActionResult Index(int id)
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
            Film f = AnasayfaController.filmler.FirstOrDefault(x => x.FilmID == id);

            url = f.FilmLink;

            //await Task.Run(() => LinkDonustur());

            //Thread.Sleep(1500);

            var youtube = YouTube.Default;
            var video = youtube.GetVideo(url);
            f.FilmLink = video.Uri;
            ViewBag.film = f;

            return View();
        }

        //public async Task<ActionResult> Index(int id)
        //{
        //    if (AnasayfaController.filmler == null)
        //    {
        //        AnasayfaController.filmler = apiData.GetApiDataFilm();
        //        foreach (var item in AnasayfaController.filmler)
        //        {
        //            item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace('.', ','));
        //            var base64 = Convert.ToBase64String(item.FilmKapakFoto);
        //            item.Resim = string.Format("data:image/jpg;base64,{0}", base64);
        //        }
        //    }
        //    Film f = AnasayfaController.filmler.FirstOrDefault(x => x.FilmID == id);

        //    url = f.FilmLink;

        //    //await Task.Run(() => LinkDonustur());

        //    //Thread.Sleep(1500);

        //    var youtube = YouTube.Default;
        //    var video = youtube.GetVideo(url);
        //    f.FilmLink = video.Uri;
        //    ViewBag.film = f;

        //    return View();
        //}

        //private async void LinkDonustur()
        //{
        //    try
        //    {
        //        var youtube = YouTube.Default;
        //        var video = await youtube.GetVideoAsync(url);
        //        link = video.Uri;
        //    }
        //    catch
        //    {
                
        //    }

        //}



    }
}