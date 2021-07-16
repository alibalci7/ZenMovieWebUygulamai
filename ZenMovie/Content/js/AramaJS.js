var film = {
    FilmID: 0,
    FilmLink: "",
    FilmBaslik: "",
    FilmKonu: "",
    FilmKapakFoto: 0,
    FilmYapimYili: "",
    FilmDil: "",
    FilmIMDB: "",
    FilmSure: "",
    FilmIzlenmeSayisi: 0,
    FilmBegeniOrani: 0,
    FilmKategoriler: "",
    FilmOyuncular: "",
    FilmYonetmenler: "",
    Resim: ""
}

var dizi = {
    DiziID: 0,
    DiziBaslik: "",
    DiziKonu: "",
    DiziKapakFoto: 0,
    DiziBaslangicYili: "",
    DiziIMDB: "",
    DiizIzlenmeSayisi: 0,
    DiziBegeniOrani: 0,
    DiziOyuncular: "",
    DiziYonetmenler: "",
    Resim: ""
}

var statefilm = {
    'page': 1,
    'rows': 12,
    'window': 5
}

var statedizi = {
    'page': 1,
    'rows': 12,
    'window': 5
}

function dataResultfilm(data) {
    statefilm.querySet = data
}

$("#loading").show();

$.ajax({
    type: 'Get',
    url: "/Arama/FilmleriGoster",
    success: function (data) {
        dataResultfilm(data);
        buildTablefilm();
        return data;
    }
})

function dataResultdizi(data) {
    statedizi.querySet = data
}

$.ajax({
    type: 'Get',
    url: "/Arama/DizileriGoster",
    success: function (data) {
        dataResultdizi(data);
        buildTabledizi();
        return data;
    }
})

function paginationfilm(querySet, page, rows) {

    var trimStart = (page - 1) * rows
    var trimEnd = trimStart + rows

    var trimmedData = querySet.slice(trimStart, trimEnd)

    var pages = Math.ceil(querySet.length / rows);

    return {
        'querySet': trimmedData,
        'pages': pages
    }
}

function pageButtonsfilm(pages) {
    var wrapper = document.getElementById('pagination-wrapper-film')

    wrapper.innerHTML = "";
    console.log('Pages:', pages)

    var maxLeft = (statefilm.page - Math.floor(statefilm.window / 2))
    var maxRight = (statefilm.page + Math.floor(statefilm.window / 2))

    if (maxLeft < 1) {
        maxLeft = 1
        maxRight = statefilm.window
    }

    if (maxRight > pages) {
        maxLeft = pages - (statefilm.window - 1)

        if (maxLeft < 1) {
            maxLeft = 1
        }
        maxRight = pages
    }

    for (var page = maxLeft; page <= maxRight; page++) {
        wrapper.innerHTML += "<button value=" + page + " class='page btn btn-sm btn-info'>" + page + "</button>"
    }

    if (statefilm.page != 1) {
        wrapper.innerHTML += "<button value=" + 1 + " class='page btn btn-sm btn-info'>İlk</button>"
    }

    if (statefilm.page != pages) {
        wrapper.innerHTML += "<button value=" + pages + " class='page btn btn-sm btn-info'>Son</button>"
    }

    $(".page").on("click", function () {
        $("#film").empty()

        statefilm.page = Number($(this).val())

        buildTablefilm()
    })

}

function buildTablefilm() {
    var col = $('#film')

    if (statefilm.querySet.length > 0) {

        var data = paginationfilm(statefilm.querySet, statefilm.page, statefilm.rows)
        var myList = data.querySet

        for (i = 0; i < myList.length; i++) {
            var baslik = BoslukSil(myList[i].FilmBaslik);
            var kategori = BoslukSil(myList[i].FilmKategoriler);
            var kolon = "<div class='col-6 col-sm-4 col-md-3 col-xl-2'><div class='card'><div class='card__cover'><img src='" + myList[i].Resim + "' width='150' height='200'><a href='/Film/Index/" + myList[i].FilmID + "' class='card__play'><i class='icon ion-ios-play'></i></a><span class='card__rate card__rate--green'>" + myList[i].FilmIMDB + "</span></div><div class='card__content'><h3 class='card__title'><a href='#' title=" + baslik + ">" + myList[i].FilmBaslik + "</a></h3><h3 class='card__title'><a style='color:yellow; font-size:16px;' href='#' title=" + kategori + ">" + myList[i].FilmKategoriler + "</a></h3></div></div></div>"
            col.append(kolon);
        }
        pageButtonsfilm(data.pages)
        $("#loading").hide();
    }

}

function paginationdizi(querySet, page, rows) {

    var trimStart = (page - 1) * rows
    var trimEnd = trimStart + rows

    var trimmedData = querySet.slice(trimStart, trimEnd)

    var pages = Math.ceil(querySet.length / rows);

    return {
        'querySet': trimmedData,
        'pages': pages
    }
}

function pageButtonsdizi(pages) {
    var wrapper = document.getElementById('pagination-wrapper-dizi')

    wrapper.innerHTML = "";
    console.log('Pages:', pages)

    var maxLeft = (statedizi.page - Math.floor(statedizi.window / 2))
    var maxRight = (statedizi.page + Math.floor(statedizi.window / 2))

    if (maxLeft < 1) {
        maxLeft = 1
        maxRight = statedizi.window
    }

    if (maxRight > pages) {
        maxLeft = pages - (statedizi.window - 1)

        if (maxLeft < 1) {
            maxLeft = 1
        }
        maxRight = pages
    }

    for (var page = maxLeft; page <= maxRight; page++) {
        wrapper.innerHTML += "<button value=" + page + " class='page btn btn-sm btn-info'>" + page + "</button>"
    }

    if (statedizi.page != 1) {
        wrapper.innerHTML += "<button value=" + 1 + " class='page btn btn-sm btn-info'>İlk</button>"
    }

    if (statedizi.page != pages) {
        wrapper.innerHTML += "<button value=" + pages + " class='page btn btn-sm btn-info'>Son</button>"
    }

    $(".page").on("click", function () {
        $("#dizi").empty()

        statedizi.page = Number($(this).val())

        buildTabledizi()
    })

}

function buildTabledizi() {
    var col = $('#dizi')

    if (statedizi.querySet.length > 0) {
        var data = paginationdizi(statedizi.querySet, statedizi.page, statedizi.rows)
        var myList = data.querySet

        for (i = 0; i < myList.length; i++) {
            var baslik = BoslukSil(myList[i].DiziBaslik);
            var kolon = "<div class='col-6 col-sm-4 col-md-3 col-xl-2'><div class='card'><div class='card__cover'><img src='" + myList[i].Resim + "' width='150' height='200'><a href='/Dizi/Index/" + myList[i].DiziID + "' class='card__play'><i class='icon ion-ios-play'></i></a><span class='card__rate card__rate--green'>" + myList[i].IMDB + "</span></div><div class='card__content'><h3 class='card__title'><a href='#' title=" + baslik + ">" + myList[i].DiziBaslik + "</a></h3></div></div></div>"
            col.append(kolon);
        }
        pageButtonsdizi(data.pages)
        $("#loading").hide();
    }

}

function BoslukSil(ifade) {
    var bosluksuz = ifade
    bosluksuz = bosluksuz.replace(/\s/g, '')
    return bosluksuz
}