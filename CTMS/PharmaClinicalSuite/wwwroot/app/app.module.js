(function () {
    'use strict';

    angular.module('ctmsApp', [
        // Angular modules 
        'ngRoute',

        // Custom modules 

        // 3rd Party Modules

    ])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/participant-visits', {
                templateUrl: 'app/views/participant-visits.html',
                controller: 'participantVisitsController',
                controllerAs: 'vm'
            })
            .otherwise({ redirectTo: '/participant-visits' });
    }])
})();