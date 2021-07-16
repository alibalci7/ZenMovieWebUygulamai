using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoLibrary;
using ZenMovie.Models;
using ZenMovie.Tools;

namespace ZenMovie.Controllers
{
    public class DiziController : Controller
    {
        string url = "";
        string link = "";
        // GET: Dizi
        public ActionResult Index(int id)
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
            if (AnasayfaController.bolumler == null)
            {
                AnasayfaController.bolumler = apiData.GetApiDataBolum();
            }

            Dizi dizi = AnasayfaController.diziler.FirstOrDefault(x => x.DiziID == id);
            List<string> sezonnolar = AnasayfaController.bolumler.Where(x=>x.DiziID==dizi.DiziID).Select(x => x.DiziBolumSezonNo).Distinct().Reverse().ToList();

            ViewBag.dizi = dizi;
            ViewBag.sezonnolar = sezonnolar;
            return View();
        }

        public async Task<ActionResult> Bolum(int bid, int did, string sezonno)
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
            if (AnasayfaController.bolumler == null)
            {
                AnasayfaController.bolumler = apiData.GetApiDataBolum();
            }

            Bolum bolum = AnasayfaController.bolumler.FirstOrDefault(x => x.DiziBolumID == bid);
            Dizi dizi = AnasayfaController.diziler.FirstOrDefault(x => x.DiziID == did);
            List<string> sezonnolar = AnasayfaController.bolumler.Where(x => x.DiziID == dizi.DiziID).Select(x => x.DiziBolumSezonNo).Distinct().Reverse().ToList();

            url = bolum.DiziBolumLink;
            await Task.Run(() => LinkDonustur());

            Thread.Sleep(2500);

            bolum.DiziBolumLink = link;
            ViewBag.bolum = bolum;
            ViewBag.dizi = dizi;
            ViewBag.sezonno = bolum.DiziBolumSezonNo;
            ViewBag.sezonnolar = sezonnolar;
            return View();
        }

        public JsonResult Bolumler(int id, string sezonno)
        {
            Dizi dizi = AnasayfaController.diziler.FirstOrDefault(x => x.DiziID == id);
            List<Bolum> bolumleri = AnasayfaController.bolumler.Where(x => x.DiziID == id && x.DiziBolumSezonNo==sezonno).ToList();
            foreach (Bolum item in bolumleri)
            {
                item.IMDB = dizi.IMDB;
                item.DiziResim = dizi.Resim;
            }
            return Json(bolumleri, JsonRequestBehavior.AllowGet);
        }

        private async void LinkDonustur()
        {
            try
            {
                var youtube = YouTube.Default;
                var video = await youtube.GetVideoAsync(url);
                link = video.Uri;
            }
            catch
            {

            }

        }
    }
}