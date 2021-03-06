﻿(function () {
    'use strict';

    angular.module('myApp').factory('configService', ConfigService);
    ConfigService.$inject = ['$http'];

    function ConfigService($http) {

        var baseUrl = 'http://localhost:50000/api/config/';

        var req = {
            method: 'PUT',
            url: url,
            headers: {
                'Authorization': "Bearer " + $rootScope.UserPricinpal.Token,
                'Accept': 'application/json'
            },
            data: data
        }

        return {
            getAll: function () {
                return $http({
                    method: 'GET',
                    url: baseUrl + "list",
                    headers: {
                        'Authorization': "Bearer " + "CfDJ8EfnEZl3IKhLgGulGWP-kEAeRVFGgv1SAay7K9FSCgdIxLmLkNMdty3Rya2esBGSte45z2pzbdkhk2Zn1MSUBq1oD-jk5GnCo_0sK-XRbtUUibUMWZiuCHHYJW7JJpvi9cQ4je0Ym0nHCkbB4LKXTgGm_xY8K6vNKrp5irtN4LwoTWqPf2R1qrVapQDWDBl-jOc53cXsVHU8ydRSy1mcraVw8nu7XVYv0_mf_yWYwZGdPr95_s90C0eYY2xoLEkZX2AQBdYq0EUQTl5tRLn8vtw2sUMnPNsRXiG-yDIAIci7-heDN6d0QxatqLRB67KzOY2t789rgt5N4nLpDytXyIEQdw8dSRGLEYim6dkrMm6vNFECAD_owJ0iX_SIlPojjQbosZXmTE_tFL_D11XJcrzq3qnCGnvF3xylIt_ZtzzGwOZZs7H7hRar_IXNVlQEBwKBNTt5ozWB7BoV7m4NqyovIgOrlo4XGvx4v7GJhV0Ph0iKVsjxlG3zSmk-9a9c3ub0MY4enBr0Io_I1GtsDWcc2XaWfplAS6nIrVKX2o0-w8TcXvZ36HZy1YIUTvTADJgjMF9NFccXrZMgpyhMCohN0fxif1ngykDCS13jiV9mI0gdQ1bEJhNzs-NRJWlF2sPjwSrbLH38lr-mnCXWMBLx3K2mjxlkLKJtImGe8WFUeGQVIorOsMvd2L5v_pEBX0PPl9d2SMkkpWXpvPywmoBtdrBfSToUZp5UcIN4jdR8xckphWCmKzyAoBvKaqjJ9cfcwqnSp8atejBoiCOwT-s",
                        'Accept': 'application/json'
                    },
                });
            },

            insert: function (config) {
                return $http({
                    method: 'POST',
                    url: baseUrl + "create",
                    data: config
                });
            },

            update: function (config) {
                return $http({
                    method: 'PUT',
                    url: baseUrl + "update",
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