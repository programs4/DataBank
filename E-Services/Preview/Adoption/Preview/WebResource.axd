* {
    margin: 0;
    padding: 0;
}

body {
    font: 71%/1.5 Verdana, Sans-Serif;
/*background: linear-gradient(45deg, #00557f, #0075ec);*/
}

#container {
    margin: 100px auto;
    width: 688px;
}

[class*="write"]{
    margin: 0 0 5px;
    padding: 5px;
    width: 350px;
    height: 30px;
    font: 15px Verdana, Sans-Serif;
    background: #fff;
    border: 1px solid #f9f9f9;
    -moz-border-radius: 5px;
    -webkit-border-radius: 5px;
}

[id^="keyboard"] {
    margin: 0;
    margin-top: 10px;
    padding: 0;
    list-style: none;
    display: none;
}

    [id^="keyboard"] li {
        float: left;
        margin: 0 5px 5px 0;
        width: 40px;
        height: 40px;
        line-height: 40px;
        text-align: center;
        background: #fff;
        border: 1px solid #f9f9f9;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
    }

 [id^="keyboard"] li[disabled] {
        background: rgba(255, 255, 255, 0.47);
}

.capslock, .tab, .left-shift {
    clear: left;
}

[id^="keyboard"] .tab, [id^="keyboard"] .delete {
    width: 70px;
}

[id^="keyboard"] .capslock {
    width: 80px;
}

[id^="keyboard"] .return {
    width: 75px;
}

[id^="keyboard"] .left-shift {
    width: 95px;
}

[id^="keyboard"] .right-shift {
    width: 106px;
}

.lastitem {
    margin-right: 0;
}

.uppercase {
    text-transform: uppercase;
}

[id^="keyboard"] .space {
    clear: left;
    width: 656px;
}

.on {
    display: none;
}

[id^="keyboard"] li:hover:not([disabled]) {
    position: relative;
    top: 1px;
    left: 1px;
    border-color: #e5e5e5;
    cursor: pointer;
}

.isNumeric {
  /*text-align:right!important;*/
}

.keyboard_center {
    left: 15%;
    /* margin: 0 auto; */
    top: 25%;
    position: absolute;
    z-index: 999999;
    background: url('WebResource.axd?d=DxzmOjPzEdBZtaetsv7wVgoI6XMii2jsJI-Ab2lG75nPkHtEUgr_7EA-5o8xJ8SatI-CpgXXKJpGbMmIpv36OqKROFJW0ct_wsXQmi2zAsweRSFkQ-xXEfmz9sOCR9vj0&t=636144594074205922');
    border-radius: 12px;
    box-shadow: rgba(0, 0, 0, 0.48) 0px 0px 25px 2px;
    padding: 34px 42px;
}

    .keyboard_center input {
    width:656px !important;
    }

.blur {
    -webkit-transition: opacity .15s linear;
    background-color: rgba(255, 255, 255, 0.6);
    opacity: 0;
    opacity: .8;
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    transition: opacity .15s linear;
    z-index:999998;
}

.closeKey {
    position: absolute;
    right: 0px;
    width: 50px;
    height: 30px;
    background-color: rgba(255, 0, 0, 0.6);
    text-align: center;
    color: white;
    line-height: 30px;
    font-size: 25px;
    top: 0px;
    border-radius: 2px;
    border-top-right-radius: 12px;
    display: initial;
}
/* all */
::-webkit-input-placeholder { font-size:14px; }
::-moz-placeholder { font-size:14px; } /* firefox 19+ */
:-ms-input-placeholder { font-size:14px; } /* ie */
input:-moz-placeholder { font-size:14px; }
