var bolum = {
    DiziBolumID: 0,
    DiziID: 0,
    DiziBolumLink: "",
    DiziBolumBaslik: "",
    DiziBolumSezonNo: "",
    DiziIzlenmeSayisi: 0,
    IMDB: 0,
    DiziResim: ""
}

var state = {}

function dataResult(data) {
    state.querySet = data
}

var diziid = $(".bolumlericek").data("id");
var sezon;

$(".bolumlericek").click(function () {
    sezon = $(this).data("sezon");
    BolumleriCek(sezon);
});

$.ajax({
    type: 'Get',
    url: "/Dizi/Bolumler?id=" + diziid + "&sezonno=1",
    success: function (data) {
                dataResult(data);
                buildEpisodes();
                return data;
    }
})

function BolumleriCek(sezonno) {
    var yol = "/Dizi/Bolumler?id=" + diziid + "&sezonno=" + sezonno;
    $.ajax({
        type: 'Get',
        url: yol,
        success: function (data) {
            dataResult(data);
            buildEpisodes();
            return data;
        }
    })
}

function buildEpisodes() {
    var content = $('#bolumler')
    content.empty();
    var myList = state.querySet

    for (i = 0; i < myList.length; i++) {
        var baslik = BoslukSil(myList[i].DiziBolumBaslik);
        bolum = "<div class='col-6 col-sm-4 col-md-3 col-xl-2'><div class='card'><div class='card__cover'><img src=' " + myList[i].DiziResim + " ' width='150' height='200' alt=''><a href='/Dizi/Bolum?bid=" + myList[i].DiziBolumID + "&did=" + myList[i].DiziID + "&sezonno=" + myList[i].DiziBolumSezonNo + "' class='card__play'><i class='icon ion-ios-play'></i></a><span class='card__rate card__rate--green'>" + myList[i].IMDB + "</span></div><div class='card__content'><h3 class='card__title' title='" + baslik + "'><a>" + myList[i].DiziBolumBaslik + "</a></h3></div></div></div>"
        content.append(bolum)
    }
}

function BoslukSil(ifade) {
    var bosluksuz = ifade
    bosluksuz = bosluksuz.replace(/\s/g, '')
    return bosluksuz
}