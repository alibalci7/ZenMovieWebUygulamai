var imdbbas = document.getElementById('filter__imbd-start')
var imdbson = document.getElementById('filter__imbd-end')
var yilbas = document.getElementById('filter__years-start')
var yilson = document.getElementById('filter__years-end')

function Goster() {
    var kriterler = "/Diziler/Index?imdbbas=" + imdbbas.innerHTML + "&imdbson=" + imdbson.innerHTML + "&yilbas=" + yilbas.innerHTML + "&yilson=" + yilson.innerHTML;
    window.location.href = kriterler;
}

var state = {
    'page': 1,
    'rows': 20,
    'window': 5
}

function dataResult(data) {
    state.querySet = data
}

$("#loading").show();

$.ajax({
    type: 'Get',
    url: "/Diziler/Listele",
    success: function (data) {
        dataResult(data);
        buildTable();
        return data;
    }
})

function pagination(querySet, page, rows) {

    var trimStart = (page - 1) * rows
    var trimEnd = trimStart + rows

    var trimmedData = querySet.slice(trimStart, trimEnd)

    var pages = Math.ceil(querySet.length / rows);

    return {
        'querySet': trimmedData,
        'pages': pages
    }
}

function pageButtons(pages) {
    var wrapper = document.getElementById('pagination-wrapper')

    wrapper.innerHTML = "";
    console.log('Pages:', pages)

    var maxLeft = (state.page - Math.floor(state.window / 2))
    var maxRight = (state.page + Math.floor(state.window / 2))

    if (maxLeft < 1) {
        maxLeft = 1
        maxRight = state.window
    }

    if (maxRight > pages) {
        maxLeft = pages - (state.window - 1)

        if (maxLeft < 1) {
            maxLeft = 1
        }
        maxRight = pages
    }

    for (var page = maxLeft; page <= maxRight; page++) {
        wrapper.innerHTML += "<button value=" + page + " class='page btn btn-sm btn-info'>" + page + "</button>"
    }

    if (state.page != 1) {
        wrapper.innerHTML += "<button value=" + 1 + " class='page btn btn-sm btn-info'>İlk</button>"
    }

    if (state.page != pages) {
        wrapper.innerHTML += "<button value=" + pages + " class='page btn btn-sm btn-info'>Son</button>"
    }

    $(".page").on("click", function () {
        $("#our-table").empty()

        state.page = Number($(this).val())

        buildTable()
    })

}

function buildTable() {
    var col = $('#our-table')

    var data = pagination(state.querySet, state.page, state.rows)
    var myList = data.querySet

    for (i = 0; i < myList.length; i++) {
        var baslik = BoslukSil(myList[i].DiziBaslik);
        var kolon = "<div class='col-6 col-sm-4 col-md-3 col-xl-2'><div class='card'><div class='card__cover'><img src='" + myList[i].Resim + "' width='150' height='200'><a href='/Dizi/Index/" + myList[i].DiziID + "' class='card__play'><i class='icon ion-ios-play'></i></a><span class='card__rate card__rate--green'>" + myList[i].DiziIMDB + "</span></div><div class='card__content'><h3 class='card__title'><a href='#' title=" + baslik + ">" + myList[i].DiziBaslik + "</a></h3></div></div></div>"
        //var row = "<tr><td><div style='width:350px;' class='col-6 col - sm - 4 col - md - 3 col - xl - 2'><div class='card'><div class='card__cover'><img src='../../Content/img/covers/cover.jpg'><a href='#' class='card__play'><i class='icon ion-ios-play'></i></a><span class='card__rate card__rate--green'>" + myList[i].FilmIMDB + "</span></div><div class='card__content'><h3 class='card__title'><a href='#' title=''>" + myList[i].FilmBaslik + "</a></h3><span class='card__category'><a href='#'  title='' >" + myList[i].FilmKategoriler + "</a></span></div></div></div></td><td><div style='width:350px;' class='col-6 col - sm - 4 col - md - 3 col - xl - 2'><div class='card'><div class='card__cover'><img src='../../Content/img/covers/cover.jpg'><a href='#' class='card__play'><i class='icon ion-ios-play'></i></a><span class='card__rate card__rate--green'>" + myList[i + 1].FilmIMDB + "</span></div><div class='card__content'><h3 class='card__title'><a href='#' title=''>" + myList[i + 1].FilmBaslik + "</a></h3><span class='card__category'><a href='#' title=''>" + myList[i + 1].FilmKategoriler + "</a></span></div></div></div></td><td><div style='width:350px;' class='col-6 col - sm - 4 col - md - 3 col - xl - 2'><div class='card'><div class='card__cover'><img src='../../Content/img/covers/cover.jpg'><a href='#' class='card__play'><i class='icon ion-ios-play'></i></a><span class='card__rate card__rate--green'>" + myList[i + 2].FilmIMDB + "</span></div><div class='card__content'><h3 class='card__title'><a href='#' title=''>" + myList[i + 2].FilmBaslik + "</a></h3><span class='card__category'><a href='#' title=''>" + myList[i + 2].FilmKategoriler + "</a></span></div></div></div></td><td><div style='width:350px;' class='col-6 col - sm - 4 col - md - 3 col - xl - 2'><div class='card'><div class='card__cover'><img src='../../Content/img/covers/cover.jpg'><a href='#' class='card__play'><i class='icon ion-ios-play'></i></a><span class='card__rate card__rate--green'>" + myList[i + 3].FilmIMDB + "</span></div><div class='card__content'><h3 class='card__title'><a href='#' title=''>" + myList[i + 3].FilmBaslik + "</a></h3><h3 class='card__title' style='color:yellow;'><a href='#' style='color:yellow;' title=''>fghdfhdfghşflghkföghfdhdfghjfghmlşhjghıhhdhhıhıdhıhşdfmhşdfghdlfmhdgfkljhfdhjmıhısfohmstuımhıshl</a></h3></div></div></div></td></tr>"
        col.append(kolon)
    }
    pageButtons(data.pages)
    $("#loading").hide();
}

function BoslukSil(ifade) {
    var bosluksuz = ifade
    bosluksuz = bosluksuz.replace(/\s/g, '')
    return bosluksuz
}