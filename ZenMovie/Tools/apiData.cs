using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using ZenMovie.Models;

namespace ZenMovie.Tools
{
    public class apiData
    {
        static string yol = "http://192.168.1.8:8085/api/";

        public static List<Dizi> GetApiDataDizi()
        {
            string dizi = yol + "Dizi/GetDizileriListele?imdbmin=1&imdbmax=10&yilmin=1950&yilmax=2200&id=0&adet=10";

            //Connect API
            Uri url = new Uri(dizi);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            var json = client.DownloadString(url);
            //END

            //JSON Parse START
            List<Dizi> jsonList = JsonConvert.DeserializeObject<List<Dizi>>(json);
            //END

            return jsonList;
        }

        public static List<Film> GetApiDataFilm()
        {
            string film = yol + "Film/GetFilmleriListele?imdbmin=1&imdbmax=10&yilmin=1950&yilmax=2200&id=0&adet=10";

            //Connect API
            Uri url = new Uri(film);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            var json = client.DownloadString(url);
            //END

            //JSON Parse START
            List<Film> jsonList = JsonConvert.DeserializeObject<List<Film>>(json);
            //END

            return jsonList;
        }

        public static List<Bolum> GetApiDataBolum()
        {

            string bolum = yol + "Dizi/GetBolumleriListele?id=1";

            //Connect API
            Uri url = new Uri(bolum);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            var json = client.DownloadString(url);
            //END

            //JSON Parse START
            List<Bolum> jsonList = JsonConvert.DeserializeObject<List<Bolum>>(json);
            //END

            return jsonList;
        }

        public static List<Kategori> GetApiDataKategori()
        {

            string film = yol + "Film/GetKategorileriListele";

            //Connect API
            Uri url = new Uri(film);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            var json = client.DownloadString(url);
            //END

            //JSON Parse START
            List<Kategori> jsonList = JsonConvert.DeserializeObject<List<Kategori>>(json);
            //END

            return jsonList;
        }

        public static List<FilmKategori> GetApiDataFilmKategori()
        {

            string film = yol + "Film/GetFilmKategorileriListele";

            //Connect API
            Uri url = new Uri(film);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            var json = client.DownloadString(url);
            //END

            //JSON Parse START
            List<FilmKategori> jsonList = JsonConvert.DeserializeObject<List<FilmKategori>>(json);
            //END

            return jsonList;
        }

    }
}