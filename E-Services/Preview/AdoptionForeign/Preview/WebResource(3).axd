//$(document).ready(
function textboxClick(id) {
    $('#keyboard_' + id).css('display', 'block');
    $('#dv' + id).addClass('keyboard_center');
    //.draggable();

    $('#dv' + id).find('.closeKey').unbind().click(function () {
        $("[id^='keyboard']").css('display', 'none');
        $('.keyboard_center').removeClass('keyboard_center');
        $('#blur').removeClass('blur');
        //avtomatik TextChanged Funksiyasini iwletsin
        $write.trigger("change");
        return true;
    });

    $('#blur').addClass('blur');

    var $write = $('#' + id),
            shift = false,
            capslock = true;

       //$write.keydown(function (e) {
       //    if (e.which == 13) {
       //        e.preventDefault();
       //        $write.trigger("change");
       // }

    //});

    $('[id^="keyboard"]  li:not([disabled])').unbind().click(function () {

        
        var $this = $(this),
            character = $this.html(); // If it's a lowercase letter, nothing happens to this variable

        // Shift keys
        if ($this.hasClass('left-shift') || $this.hasClass('right-shift')) {
            $('.letter').toggleClass('uppercase');
            $('.symbol span').toggle();

            shift = (shift === true) ? false : true;
            capslock = false;
            return false;
        }

        // Caps lock
        if ($this.hasClass('capslock')) {
            $('.letter').toggleClass('uppercase');
            capslock = true;
            return false;
        }

        // Delete
        if ($this.hasClass('delete')) {
            var html = $write.val();

            $write.val(html.substr(0, html.length - 1));
            return false;
        }


        // Special characters
        if ($this.hasClass('symbol')) character = $('span:visible', $this).html();
        if ($this.hasClass('space')) character = ' ';
        if ($this.hasClass('tab')) character = "\t";
        if ($this.hasClass('return')) {
            //character = "\n";
            $("[id^='keyboard']").css('display', 'none');
            $('.keyboard_center').removeClass('keyboard_center');
            $('#blur').removeClass('blur');
            //avtomatik TextChanged Funksiyasini iwletsin
            $write.trigger("change");
            return true;
        }

        // Uppercase letter
        if ($this.hasClass('uppercase')) character = character.toUpperCase();

        // Remove shift once a key is clicked.
        if (shift === true) {
            $('.symbol span').toggle();
            if (capslock === false) $('.letter').toggleClass('uppercase');
            shift = false;
        }
        if ($write.attr('maxlength') === undefined && $write.attr('max') === undefined)
        { }
        else if (!(($write.attr('maxlength') != undefined && $write.attr('maxlength') > $write.val().length) || ($write.attr('max') != undefined && $write.attr('max') > $write.val().length))) {
            return true;
        }

        // Add the character
        $write.val($write.val() + character);
        //$write.reload();
    });
}
//$(document).click(function (event) {
//    if (!$(event.target).is('[id^="keyboard"],[id^="keyboard"] li,[id^="keyboard"] li span,[class*="write"]')) {
//        //$("[id^='keyboard']").css('display', 'none');
//        //$('.keyboard_center').removeClass('keyboard_center');
//          $('#blur').removeClass('blur');
//    }
//});


$(document).ready(function () {
    //only numeric
    $("[class*='isNumeric']").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    //////$('.closeKey,.return').click(function () {

    //////    $("[id^='keyboard']").css('display', 'none');
    //////            $('.keyboard_center').removeClass('keyboard_center');
    //////            $('#blur').removeClass('blur');
    //////            //location.reload();
    //////});
});
   