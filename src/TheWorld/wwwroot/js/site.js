// site.js
(function() {
  // var ele = $("#userName"); //instead of document.getElementById("username")
  // ele.text("Frank Chewing"); //instead of ele.innerHTML = "";
  // var main = $("#main");
  // main.on("mouseenter", function() {
  //   main.css("background-color", "#888");
  // });
  // main.on("mouseleave", function() {
  //   main.css("background-color", "");
  // });
  // //Instead OF
  // // main.onmouseleave = function() {
  // //   main.style.backgroundColor = "";
  // // };
  // var menuItems = $("ul.menu li a");
  // menuItems.on("click", function() {
  //   alert($(this).text());
  // });

  var $sidebarAndWrapper = $("#sidebar,#wrapper");

  $("#menuToggle").on("click", function() {
    $sidebarAndWrapper.toggleClass("hide-sidebar");
    if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
      $(this).text("Show Menu");
    } else {
      $(this).text("Hide Menu");
    }
  });
})();
