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
                jQuery('body').load('../pages/popup.html', function () {
                    localStorage.setItem("userToken", data.token);
                    localStorage.setItem("userName", login);
                    Popup.init();
                });
            }).fail(function () {
                jQuery('#message').html('<span>Authorization failed</span>').css('display', 'block');
            });
        } else {
            jQuery('#message').html('<span>Login and password can\'t be empty</span>').css('display', 'block');
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