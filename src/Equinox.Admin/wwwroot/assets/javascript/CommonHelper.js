var CommonHelper = function ($rootScope, $localstorage, $timeout, $q, $http) {
    var service = {};

    service.ConfigsUrl = "Config/"; 
    service.CurrencysUrl = "Currency/"; 

    
    return service;
}
CommonHelper.$inject = ["$rootScope", "$localstorage", "$timeout", "$q", "$http"];
