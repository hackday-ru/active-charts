Popup = {};

jQuery(document).ready(function () {
    Popup.init();
});

Popup.init = function () {
    jQuery('#xpath-button').click(function () {

        // chrome.tabs.getSelected(null, function(tab) { //<-- "tab" has all the information
        chrome.tabs.query({active: true, currentWindow: true}, function (tab) {            
            var token = localStorage.getItem('userToken');            
            chrome.tabs.sendMessage(tab[0].id, {action: "getSelection", url: tab[0].url, token: token});
        });
        // });
    });

    jQuery('#graph-button').on('click', function () {

        var token = localStorage.getItem('userToken');

        jQuery.ajax({
            type: "GET",
            beforeSend: function() {
                jQuery('#graph-content').html('');
                jQuery('.spinner-background').css('display', 'block');
            },
            url: 'http://activecharts.tk/profile/getchartspreview?token='+token,
            dataType: "html"
        }).done(function (data) {
            jQuery('.spinner-background').css('display', 'none');
            jQuery('#graph-content').append(data);
        }).fail(function () {
            jQuery('.spinner-background').css('display', 'none');
            jQuery('#graph-content').append('<b>Error during loading graph</b>');
        });
    });
};