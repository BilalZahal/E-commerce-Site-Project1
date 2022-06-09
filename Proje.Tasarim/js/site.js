$(document).ready(function () {
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        dots: false,
        navText: ['<i class="fa fa-chevron-left"></i>', '<i class="fa fa-chevron-right"></i>'],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 3,
                loop: false,
                margin: 20
            }
        }
    })
})

function SepetiGoster() {
    $('.alisverisSepeti').show();
}
function SepetiGizle() {
    $('.alisverisSepeti').hide();
}

function urunGoster(v) {
    var imgsrc = $(v).attr('src');
    $('#urunDetayResim').attr('src', imgsrc);
}