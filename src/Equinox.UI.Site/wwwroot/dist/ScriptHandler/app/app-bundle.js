(function () {
    'use strict';
    angular.module('myApp', []);
})();
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
(function () {
    'use strict';
    angular.module('myApp').controller('configController', ConfigController);
    ConfigController.$inject = ['$scope', 'configService'];

    function ConfigController($scope, configService) {

        $scope.config = {};
        $scope.configs = [];
        $scope.show = false;

        $scope.getAll = function () {
            configService.getAll().success(function (data) {
                if (data) {
                    response.data.forEach(function (msg) {
                        alertify.error('error');
                    });
                    return;
                }
                $scope.configs = data;
            }).error(function (response) {
                if (!response.notes) return;
                response.notes.forEach(function (msg) {
                    alertify.error(msg.description);
                });
            });
        };

        $scope.insert = function () {
            configService.insert($scope.config).success(function (data) {
                $scope.config = {};
                $scope.getAll();
                data.notes.forEach(function (msg) {
                    alertify.success(msg.description);
                });
            }).error(function (response) {
                response.Notes.forEach(function (msg) {
                    alertify.error(msg.description);
                });
            });
        };

        $scope.update = function () {
            configService.update($scope.config).success(function (data) {
                $scope.config = {};
                $scope.getAll();
                data.notes.forEach(function (msg) {
                    alertify.success(msg.description);
                });
            }).error(function (response) {
                response.Notes.forEach(function (msg) {
                    alertify.error(msg.description);
                });
            });
        };

        $scope.remove = function (id) {
            configService.remove(id).success(function (data) {

                data.notes.forEach(function (msg) {
                    alertify.success(msg.description);
                });

                $scope.getAll();

            }).error(function (response) {
                response.Notes.forEach(function (msg) {
                    alertify.error(msg.Description);
                });
            });
        };

        $scope.get = function (id) {
            configService.get(id).success(function (data) {
                if (data.notes) {
                    response.notes.forEach(function (msg) {
                        alertify.error(msg.description);
                    });
                    return;
                }

                $scope.config = data;
                $scope.show = true;
            }).error(function (response) {
                response.notes.forEach(function (msg) {
                    alertify.error(msg.description);
                });
            });
        };

        activate();

        function activate(){
            $scope.getAll();
        }
    }
})();