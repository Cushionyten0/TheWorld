//tripEditorController.js

(function() {
  "use strict";

  angular
    .module("app-trips")
    .controller("tripEditorController", tripEditorController);

  function tripEditorController($routeParams) {
    var vm = this;

    vm.tripname = $routeParams.tripname;
  }
})();
