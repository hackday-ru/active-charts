Login = {};

Login.init = function () {
    jQuery('#loginButton').click(function () {

        var login = jQuery('#loginField').val();
        var password = jQuery('#passwordField').val();

        if(login && password) {
            jQuery.ajax({
                type: "GET",
                url: "http://activecharts.tk/account/gettoken?nickname=" + login + "&password=" + password,
                dataType: "json"
            }).done(function (data) {
                localStorage.setItem("userToken", data.token);
                jQuery('body').load('../pages/popup.html', function () {
                    Popup.init();
                });
            }).fail(function (data) {

            });
        } else {

        }
    });
};

jQuery(document).ready(function () {
    var token = localStorage.getItem('userToken');
    if(token != null) {
        jQuery('body').load('../pages/popup.html', function () {
            Popup.init();
        });
    } else {
        Login.init();
    }
});