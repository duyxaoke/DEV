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
                if (!data.data) {
                    response.data.data.forEach(function (msg) {
                        alertify.error('error');
                    });
                    return;
                }
                $scope.configs = data.data;
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
                alertify.success('success');
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
            configService.get(id).success(function (response) {
                $scope.config = response.data;
                $scope.show = true;
            }).error(function (response) {
                    alertify.error('error');
            });
        };

        activate();

        function activate(){
            $scope.getAll();
        }
    }
})();
