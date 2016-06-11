// site.js
(function () {

  var $sidebarAndWrapper = $("#sidebar,#wrapper");

  $("#sidebarToggle").on("click", function () {
    $sidebarAndWrapper.toggleClass("hide-sidebar");
    if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
      $("<div><a class=fa fa-angle-left></a></div>")
    } else {
      $("<div><a class=fa fa-angle-right></a></div>");
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