(function() {
	'use strict';
	
	angular
		.module('RaceTiming')
		.controller('FinishersController', function($scope, $http) {

		$http.get("/api/raceinfo")
            .success(function (response) {
                $scope.raceinfo = response;
            });

        $http.get("/api/finishers")
            .success(function(response) {
                $scope.finishers = response.finishers;
            });
		});


})();