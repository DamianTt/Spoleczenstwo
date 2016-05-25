function switch_style(css_title) {
    var i, link_tag;
    for (i = 0, link_tag = document.getElementsByTagName("link") ;
      i < link_tag.length ; i++) {
        if ((link_tag[i].rel.indexOf("stylesheet") != -1) &&
          link_tag[i].title) {
            link_tag[i].disabled = true;
            if (link_tag[i].title == css_title) {
                link_tag[i].disabled = false;
            }
        }
    }
    setCookie("style", css_title, 20);
}

function setStyleFromCookie() {
    if (getCookie("style")) {
        var style = getCookie("style");
        switch_style(style);
    }
    else
        switch_style('light');
}

function setCheckboxFromCookie() {
    if (getCookie("style").length === 4) {
        $("#jebanyklikacz").switchButton({
            checked: true
        });
        $("#checkbox").prop("checked", true);
    }
    else
        $("#jebanyklikacz").switchButton({
            checked: false
        });
}

setCheckboxFromCookie();
setStyleFromCookie();