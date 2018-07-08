(function () {
    'use strict';

    angular.module('myApp').factory('configService', ConfigService);
    ConfigService.$inject = ['$http'];

    function ConfigService($http) {

        var baseUrl = '/api/ConfigApi';

        return {
            getAll: function () {
                return $http({
                    method: 'GET',
                    url: baseUrl
                });
            },

            insert: function (config) {
                return $http({
                    method: 'POST',
                    url: baseUrl,
                    data: config
                });
            },

            update: function (config) {
                return $http({
                    method: 'PUT',
                    url: baseUrl,
                    data: config
                });
            },

            remove: function (id) {
                return $http({
                    method: 'DELETE',
                    url: baseUrl + '/' + id
                });
            },

            get: function (id) {
                return $http({
                    method: 'GET',
                    url: baseUrl + '/' + id
                });
            }
        };
    }
})();