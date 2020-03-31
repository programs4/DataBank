/**
 * Created by Nasir on 15.12.2015.
 */


$(document).on('click', 'span.opener', function () {

    obj = $(this).parent('.u-menu').find('ul');

    if (obj.is(':visible'))
        obj.slideUp('fast');
    else
        obj.slideDown('fast');
    setTimeout("SetLeftBlackPanel();", 500);
})


$(document).on('click', '.accordion li > h4', function () {

    obj = $(this).parent('li').find('.acc-cont');

    if (obj.is(':visible'))
        obj.slideUp('fast').parent('li').removeClass("active");
    else
        obj.slideDown('fast').parent('li').addClass("active");
    setTimeout("SetLeftBlackPanel();", 500);
})

$(document).on('keyup', '.src input', function () {
    var keyword = $(this).val();

    //console.log(keyword);    
    $('.accordion').find('li h4:not(:contains("' + keyword.toLowerCase() + '"))').parents('li').fadeOut(200);
    $('.accordion').find('li h4:contains("' + keyword.toLowerCase() + '")').parents('li').fadeIn(200);

    SetLeftBlackPanel();
})

$(document).on('click', '.ac-hide', function () {
    $('.accordion li').removeClass("active").find('.acc-cont').fadeOut(200);
    setTimeout("SetLeftBlackPanel();", 500);
})

$(document).on('click', '.ac-show', function () {
    $('.accordion li').addClass("active").find('.acc-cont').fadeIn(200);
    setTimeout("SetLeftBlackPanel();", 500);
})


