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

    $('.kutu').click(function () {
        $('.kutu').css('background-color', '#fff');
        $('.kutu').css('color', '#000');

        $(this).css('background-color', '#000');
        $(this).css('color', '#fff');

        $('#numara').val($(this).data('value'));
    })

    $('.azalt').click(function () {
        var adet = parseInt($('#adet').val());
        if (adet > 1) {
            adet--;
        }
        $('#adet').val(adet);
    })

    $('.arttir').click(function () {
        var adet = parseInt($('#adet').val());
        adet++;
        $('#adet').val(adet);
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