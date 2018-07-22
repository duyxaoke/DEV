﻿var sys = (function () {
    var Alert, Loading, HideLoading, CheckNull, CheckNumber, CheckExistAttribute, DeleteRow, FormatMoney,
        UnFormatMoney, GetURLParameter, UrlDecode, FormatDate, GetMethod, PostMethod, PutMethod, DeleteMethod, SetCache, GetCache, RemoveCache;

    SetCache = function (key, value) {
        localStorage.setItem(key, JSON.stringify(value));
    };

    GetCache = function (key) {
        var value = localStorage.getItem(key);
        return value && JSON.parse(value);
    };
    RemoveCache = function (key) {
        localStorage.removeItem("token");
    };

    Alert = function (status, text) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": 400,
            "hideDuration": 500,
            "timeOut": 5000,
            "extendedTimeOut": 500,
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        var type = status === true ? "success" : "error";
        toastr[type](text);
    };
    Loading = function () {
        $("body").loading();
    };
    HideLoading = function () {
        $("body").loading('stop');
    };

    CheckNull = function (value) {
        if (value === null || !value || (typeof value === 'undefined'))
            return "";
        return value;
    }
    CheckNumber = function (value) {
        if (isNaN(parseFloat(value)) || value === null || !value || (typeof value === 'undefined'))
            return 0;
        return value;
    }
    CheckExistAttribute = function (elements, attrName, attrValue) {
        var result = true;
        if (attrValue) { // if we're checking their value...
            for (i = 0; i < elements.length; i++) {
                if (elements[i].getAttribute(attrName) == attrValue)
                    result = false;
            }
        }
        return result;
    }
    DeleteRow = function (elements) {
        if (confirm('Bạn chắc chắn muốn xóa dòng này?')) {
            $(elements).closest('tr').remove();
        }
    }
    FormatMoney = function (data, float) {
        var data = CheckNumber(data);
        return accounting.formatNumber(data, float);
    };

    UnFormatMoney = function (data) {
        return data.toString().replace(/,/g, '');
    }

    GetURLParameter = function (sParam) {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }
    UrlDecode = function (str) {
        return decodeURIComponent((str + '').replace(/\+/g, '%20'));
    }

    FormatDate = function (elements) {
        var date = $(elements).val();
        if (CheckNull(date) == "") {
            $(elements).datepicker('setDate', 'today');
        }
        else {
            let fdate = sys.UrlDecode(date);
            let from = fdate.split("/");
            $(elements).datepicker("setDate", new Date(from[2], from[1] - 1, from[0]));
        }
    }

    //call ajax to get data
    GetMethod = function (url, params) {
        return $.ajax({
            type: "Get",
            url: urlApi + url,
            dataType: "json",
            data: params,
            headers: {
                'Authorization': "Bearer " + localPrincipal,
                'Accept': 'application/json'
            },
        });
    };
    //call ajax to post data
    PostMethod = function (url, params) {
        return $.ajax({
            'type': 'POST',
            'url': urlApi + url,
            'data': JSON.stringify(params),
            'dataType': 'json',
            headers: {
                'Authorization': "Bearer " + localPrincipal,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        });
    };
    //call ajax to put data
    PutMethod = function (url, params) {
        return $.ajax({
            type: "Put",
            url: urlApi + url,
            dataType: "json",
            data: params,
            headers: {
                'Authorization': "Bearer " + localPrincipal,
                'Accept': 'application/json'
            },
        });
    };
    //call ajax to delete data
    DeleteMethod = function (url, params) {
        return $.ajax({
            type: "Delete",
            url: urlApi + url,
            dataType: "json",
            data: params,
            headers: {
                'Authorization': "Bearer " + localPrincipal,
                'Accept': 'application/json'
            },
        });
    };
    return {
        Alert: Alert,
        SetCache: SetCache,
        GetCache: GetCache,
        RemoveCache: RemoveCache,
        Loading: Loading,
        HideLoading: HideLoading,
        CheckNull: CheckNull,
        CheckNumber: CheckNumber,
        CheckExistAttribute: CheckExistAttribute,
        DeleteRow: DeleteRow,
        FormatMoney: FormatMoney,
        UnFormatMoney: UnFormatMoney,
        GetURLParameter: GetURLParameter,
        UrlDecode: UrlDecode,
        FormatDate: FormatDate,
        GetMethod: GetMethod,
        PostMethod: PostMethod,
        PutMethod: PutMethod,
        DeleteMethod: DeleteMethod
    };
})();
