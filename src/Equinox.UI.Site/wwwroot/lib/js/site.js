var sys = (function () {

    var Alert, Loading, HideLoading, CheckNull, CheckNumber, CheckExistAttribute, DeleteRow, FormatMoney,
        UnFormatMoney, GetURLParameter, UrlDecode, FormatDate, CallAjax;

    Alert = function (status, text) {
        $.notify({
            icon: status === true ? 'font-icon font-icon-check-circle' : 'font-icon font-icon-warning',
            title: status === true ? '<strong>Thành công!</strong>' : '<strong>Thất bại!</strong>',
            message: text
        }, {
                type: status === true ? "success" : "danger"
            });
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
    CallAjax = function (url, params) {
        return $.ajax({
            type: "POST",
            url: url,
            data: params,
            async: false
        });
    };
    return {
        Alert: Alert,
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
        CallAjax: CallAjax,
    };
})();
