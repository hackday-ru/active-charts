Popup = {};

// window.onload = function() {
//     console.log("onload" + Date());
// };

jQuery(document).ready(function () {
    Popup.init();
});

Popup.init = function () {
    jQuery('#xpath-button').click(function () {

        // chrome.tabs.getSelected(null, function(tab) { //<-- "tab" has all the information
        chrome.tabs.query({active: true, currentWindow: true}, function (tab) {
            chrome.tabs.sendMessage(tab[0].id, {action: "getSelection", url: tab[0].url, token: "56122275dea4f889e8ce24f67034afe"});
        });
        // });
    });

    jQuery('#graph-button').click(function () {
        jQuery.ajax({
            type: "GET",
            beforeSend: function() {
                jQuery('#graph-content').html('');
                jQuery('.spinner-background').css('display', 'block');
            },
            url: 'http://www.cbr.ru/'
        }).done(function (data) {
            jQuery('.spinner-background').css('display', 'none');
            jQuery('#graph-content').append(data);
        }).fail(function () {
            jQuery('.spinner-background').css('display', 'none');
            jQuery('#graph-content').append('<b>Error during loading graph</b>');
        });
    });
};