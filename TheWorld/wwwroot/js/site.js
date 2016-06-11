// site.js
(function () {

    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa");

  $("#sidebarToggle").on("click", function () {
    $sidebarAndWrapper.toggleClass("hide-sidebar");
    if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
        $icon.removeClass("fa-angle-left");
        $icon.addClass("fa-angle-right");
    } else {
        $icon.addClass("fa-angle-left");
        $icon.removeClass("fa-angle-right");
    }
  });
})();

//(function () {
//    var sidebarAndWrapper = $("#sidebar, #wrapper");

//    if (sidebarAndWrapper.hasClass("hide-sidebar")) {
//        $("#sidebarToggle").on("click", function () {
//            $("sidebarToggle").toggleClass("show-sidebar");
//            $("<div><a class=fa fa-angle-right></a></div>")
//        });
//    }
//    else {
//        $("#sidebarToggle").on("click", function () {
//            $("sidebarToggle").toggleClass("hide-sidebar")
//            $("<div><a class=fa fa-angle-left></a></div>")
//        });
//    }
//})();