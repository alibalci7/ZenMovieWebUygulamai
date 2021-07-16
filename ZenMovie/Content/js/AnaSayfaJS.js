var imdbbas = document.getElementById('filter__imbd-start')
var imdbson = document.getElementById('filter__imbd-end')
var yilbas = document.getElementById('filter__years-start')
var yilson = document.getElementById('filter__years-end')
var kategori = 0;

function KategoriSec(k) {
    kategori = k;
}
function Goster() {
    var url = "/Kategoriler/Index?secilenkategori=" + kategori + "&imdbbas=" + imdbbas.innerHTML + "&imdbson=" + imdbson.innerHTML + "&yilbas=" + yilbas.innerHTML + "&yilson=" + yilson.innerHTML;
    window.location.href = url;
}