(function () {
    'use strict';

    angular
        .module('ctmsApp')
        .controller('participantVisitsController', participantVisitsController);
        
    participantVisitsController.$inject = ['$http', '$scope','$location'];

    function participantVisitsController($http, $scope, $location) {
        /* jshint validthis:true */

        var vm = this;
        vm.title = 'Participants Visits';
        vm.visits = [];
      
        vm.loadVisits = loadVisits;

        var participantId = window.participantId;

        

        function loadVisits() {

            if (participantId == null) {
                alert('Please enter a Participant ID');
                return;
            }

            $http.get('/api/visits/' + participantId)
                .then(function (response) {
                    vm.visits = response.data;
                })
                .catch(function (error) {
                    console.error("Error Loading Visits:" + error);
                    alert("Could not load visits");
                })

        }

        
        activate();

        function activate() {
            vm.loadVisits(); // auto-load when page opens
        }
    }
})();
