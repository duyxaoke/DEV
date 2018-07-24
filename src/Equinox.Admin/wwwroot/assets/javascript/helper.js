var $localstorage = function ($window) {
    return {
        set: function (key, value) {
            $window.localStorage[key] = value;
        },
        get: function (key, defaultValue) { return $window.localStorage[key] || defaultValue; },
        setObject: function (key, value) {
            $window.localStorage[key] = JSON.stringify(value);
        },
        getObject: function (key) {
            try {
                var temp = $window.localStorage[key];
                if (temp) {
                    return JSON.parse(temp || "{}");
                }
            } catch (e) {
                return JSON.parse("{}");
            }
        },
        remove: function (key) {
            $window.localStorage.removeItem(key);
        },
        clearAll: function () {
            $window.localStorage.clear();
        }
    };
};

$localstorage.$inject = ["$window"];


var ApiHelper = function ($rootScope, $localstorage, $timeout, $q, $http) {
    var service = {};
    service.CheckCacheExist = (CacheKeyClient) => {
        let version = DataCacheKey[CacheKeyClient];
        if (!version) {
            return false;
        }
        let storerage = $localstorage.getObject(CacheKeyClient);
        if (!storerage) {
            return false;
        }
        if (storerage.version != version) {
            return false;
        }
        if (!storerage.data) {
            return false;
        }
        return true;
    };
    service.GetCache = (CacheKeyClient) => {
        let storerage = $localstorage.getObject(CacheKeyClient);
        return storerage.data;
    };
    service.AddCache = (CacheKeyClient, data) => {
        let version = DataCacheKey[CacheKeyClient];
        if (!version) {
            //case này do view output có DataCacheKey ko đồng nhất khai báo với CacheKeyClient
            //vo set lai view cho đúng, hoặc xóa cache render ra
            return;
        }
        let storerage = {};
        storerage.version = version;
        storerage.data = data;

        $localstorage.remove(CacheKeyClient);
        $localstorage.setObject(CacheKeyClient, storerage);
    };

    service.CodeStep = {
        Status: "",
        StatusCode: 0,
        ErrorStep: "",
        Message: "",
        ErrorMessage: "",
        Data: ""
    };

    service.JsonStatusCode = {
        Success: "Success",
        Error: "Error",
        Warning: "Warning",
        Info: "Info"
    };

    service.GetMethod = function (url, data, config) {
        let defer = $q.defer();

        let codeStep = jQuery.extend({}, ApiHelper.CodeStep);

        if (!service.CheckToken()) {
            $rootScope.MasterPage.IsLoading = false;
            defer.reject(codeStep);
            return defer.promise;
        };

        if (config && config.CacheKeyClient && service.CheckCacheExist(config.CacheKeyClient)) {
            let response = {};
            response.Data = service.GetCache(config.CacheKeyClient);
            defer.resolve(response);
            return defer.promise;
        }

        var req = {
            method: 'GET',
            url: urlApi + url,
            headers: {
                'Authorization': "Bearer " + localPrincipal,
                'Accept': 'application/json'
            },
            data: data
        }
        $http(req).then(function (jqXHR) {
            if (jqXHR.status == 204) {
                codeStep = service.SetErrorAPI(jqXHR, url, data);
                defer.reject(codeStep);
            } else {
                codeStep.Status = service.JsonStatusCode.Success;
                codeStep.Data = jqXHR.data;

                config && config.CacheKeyClient && service.AddCache(config.CacheKeyClient, codeStep.Data);

                defer.resolve(codeStep);
            }
        }, function (jqXHR) {
            codeStep = service.SetErrorAPI(jqXHR, url, data);
            defer.reject(codeStep);
        });
        return defer.promise;
    };

    service.PostMethod = function (url, data, CacheKey) {

        let codeStep = jQuery.extend({}, ApiHelper.CodeStep);
        let defer = $q.defer();

        if (!service.CheckToken()) {
            $rootScope.MasterPage.IsLoading = false;
            defer.reject(codeStep);
            return defer.promise;
        };

        var req = {
            method: 'POST',
            url: urlApi + url,
            headers: {
                'Authorization': "Bearer " + localPrincipal,
                'Accept': 'application/json'
            },
            data: data
        }
        $http(req).then(function (jqXHR) {
            if (jqXHR.status == 204) {
                codeStep = service.SetErrorAPI(jqXHR, url, data);
                defer.reject(codeStep);
            } else {
                codeStep.Status = service.JsonStatusCode.Success;
                codeStep.Data = jqXHR.data;
                defer.resolve(codeStep);
            }
        }, function (jqXHR) {
            codeStep = service.SetErrorAPI(jqXHR, url, data);
            defer.reject(codeStep);
        });
        return defer.promise;
    };

    service.PutMethod = function (url, data, CacheKey) {

        let codeStep = jQuery.extend({}, ApiHelper.CodeStep);
        let defer = $q.defer();
        if (!service.CheckToken()) {
            $rootScope.MasterPage.IsLoading = false;
            defer.reject(codeStep);
            return defer.promise;
        };

        var req = {
            method: 'PUT',
            url: urlApi + url,
            headers: {
                'Authorization': "Bearer " + localPrincipal,
                'Accept': 'application/json'
            },
            data: data
        }
        $http(req).then(function (jqXHR) {
            if (jqXHR.status == 204) {
                codeStep = service.SetErrorAPI(jqXHR, url, data);
                defer.reject(codeStep);
            } else {
                codeStep.Status = service.JsonStatusCode.Success;
                codeStep.Data = jqXHR.data;
                defer.resolve(codeStep);
            }
        }, function (jqXHR) {
            codeStep = service.SetErrorAPI(jqXHR, url, data);
            defer.reject(codeStep);
        });
        return defer.promise;
    };

    service.DeleteMethod = function (url, data, CacheKey) {

        let codeStep = jQuery.extend({}, ApiHelper.CodeStep);
        let defer = $q.defer();

        if (!service.CheckToken()) {
            $rootScope.MasterPage.IsLoading = false;
            defer.reject(codeStep);
            return defer.promise;
        };

        var req = {
            method: 'DELETE',
            url: urlApi + url,
            headers: {
                'Authorization': "Bearer " + localPrincipal,
                'Accept': 'application/json'
            },
            data: data
        }
        $http(req).then(function (jqXHR) {
            if (jqXHR.status == 204) {
                codeStep = service.SetErrorAPI(jqXHR, url, data);
                defer.reject(codeStep);
            } else {
                codeStep.Status = service.JsonStatusCode.Success;
                codeStep.Data = jqXHR.data;
                defer.resolve(codeStep);
            }
        }, function (jqXHR) {
            codeStep = service.SetErrorAPI(jqXHR, url, data);
            defer.reject(codeStep);
        });
        return defer.promise;
    };

    service.PostPromise = function (url, Method, data, SuccessCallback, ErrorCallback) {
        var q = $q.defer();
        $http({
            method: Method,
            url: urlApi + url,
            data: data
        }).then(function SuccessResolve(response) {
            if (SuccessCallback) {
                SuccessCallback(response);
            }
            q.resolve(response);
        }, function ErrorReject(response) {
            if (ErrorCallback) {
                ErrorCallback(response);
            }
            q.reject(response);

        });
        return q.promise;
    };

    service.SetErrorAPI = function (jqXHR, ApiEndPoint) {
        var codeStep = jQuery.extend({}, service.CodeStep);
        if (jqXHR.status == 200 || jqXHR.status == 201) return;
        codeStep.Status = service.JsonStatusCode.Error;
        codeStep.StatusCode = jqXHR.status;
        codeStep.ErrorStep = "API error " + jqXHR.status + ", ApiEndPoint:" + ApiEndPoint;
        switch (jqXHR.status) {
            case 406:
                var errorLst = jqXHR.data;
                codeStep.Status = service.JsonStatusCode.Warning;
                codeStep.Message = errorLst;
                if (jQuery.type(errorLst) == "array") {
                    codeStep.Message = errorLst.join("</br>");
                }
                break;
            case 500:
                //var errorLst = jqXHR.data;
                codeStep.ErrorMessage = jqXHR.data;
                codeStep.Message = service.StatusCodeMessage(jqXHR.status);
                break;
            case 204:
                codeStep.Message = "Không có dữ liệu hoặc bạn không có quyền xem dữ liệu";
                codeStep.Status = service.JsonStatusCode.Warning;
                break;
            default:
                codeStep.Message = service.StatusCodeMessage(jqXHR.status);
                break;
        }
        return codeStep;
    }

    service.StatusCodeMessage = function (status) {
        var strMessage = '';
        switch (status) {
            case 400:
                strMessage = 'Lỗi dữ liệu không hợp lệ';
                break;
            case 401:
                strMessage = 'Phiên làm việc đã hết hạn, vui lòng đăng nhập lại.';
                break;
            case 403:
                strMessage = 'Bạn không có quyền thực hiện thao tác này.';
                break;
            case 404:
                strMessage = 'URL action không chính xác';
                break;
            case 405:
                strMessage = 'Phương thức không được chấp nhận';
                break;
            case 429:
                strMessage = 'Thao tác quá nhanh';
                break;
            case 500:
                strMessage = 'Lỗi hệ thống';
                break;
            case 502:
                strMessage = 'Đường truyền kém';
                break;
            case 503:
                strMessage = 'Dịch vụ không hợp lệ';
                break;
            case 504:
                strMessage = 'Hết thời gian chờ';
                break;
            case 440:
                strMessage = 'Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại';
                break;
            default:
                strMessage = 'Lỗi chưa xác định';
                break;
        }
        return strMessage;
    };

    service.CheckToken = function () {
        return true;
        let dtmNow = new Date();
        if (!$rootScope.UserPricinpal.ExpireDate) {
            service.ConfirmRedirectLogin();
            return false;
        }
        if (moment($rootScope.UserPricinpal.ExpireDate)._d.getTime() > dtmNow.getTime()) {
            return true;
        }
        let dataSend = {};
        dataSend.RefreshToken = $rootScope.UserPricinpal.RefreshToken;

        var IsSuccess = false;
        try {
            $.ajax({
                url: urlApi + "/Accounts/RefreshToken",
                type: "POST",
                async: false,
                dataType: "json",
                data: dataSend,
                success: function (response, textStatus, jqXHR) {
                    if (response.objCodeStep.Status != 'Success') {
                        console.log("Refesh token fail");
                        console.log(response.objCodeStep);
                        IsSuccess = false;
                        service.ConfirmRedirectLogin();
                    }
                    else {
                        $localstorage.remove("UserPricinpal");
                        $localstorage.setObject("UserPricinpal", response.objUserPrincipal);
                        IsSuccess = true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log("Error Refesh token fail");
                    IsSuccess = false;
                    service.ConfirmRedirectLogin();
                }
            })
        } catch (e) {
            IsSuccess = false;
            jAlert.Warning("Error Refesh token fail");
            console.log('ApiHelper.CheckToken() error :' + e);
        }
        return IsSuccess;
    };

    service.ConfirmRedirectLogin = function () {
        if ($rootScope.IsShowConfirmRedirectLogin) {
            return;
        }
        $rootScope.IsShowConfirmRedirectLogin = true;
        jConfirm('Thông báo', 'Phiên đăng nhập hết hạn, bạn vui lòng đăng nhập lại?', function (isOK) {
            if (!isOK) {
                return;
            }
            $rootScope.IsShowConfirmRedirectLogin = false;
            window.location.href = "/Accounts/Logout";
        });
    }

    return service;
};
ApiHelper.$inject = ["$rootScope", "$localstorage", "$timeout", "$q", "$http"];