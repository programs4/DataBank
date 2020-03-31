function AlertPopup(T, V) {
    $('#alertMessage').fadeIn(200);

    var Bg = '#DF4255';
    var Icon = '/pics/nida.png';

    //IsSuccess
    if (T == 'S') {
        Bg = '#4DCC7C';
        Icon = '/pics/success.png';
    }

    document.getElementById('alertText').innerHTML = V;
    document.getElementById('alertImg').src = Icon;
    document.getElementById('alertMessage').style.background = Bg;

    setTimeout("$('#alertMessage').fadeOut(700);", 3000);
}