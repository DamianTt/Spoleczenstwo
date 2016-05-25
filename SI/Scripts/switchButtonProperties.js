$("#slider-2.demo input").switchButton({
    on_label: 'Night',
    off_label: 'Light',
    width: 40,
    height: 15,
    button_width: 16
});

$('input[name=cbSkin]').change(function () {
    if (document.getElementById("jebanyklikacz").checked === true) {
       //
    }
    else {
        //switch_style('light');
    }
});