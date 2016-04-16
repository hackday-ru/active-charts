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
            chrome.tabs.sendMessage(tab[0].id, {action: "getSelection", url: tab[0].url, token: "56122275dea4f889e8ce24f67034afe"}, function (response) {
                // console.log(response.requestStatus);
            });
        });
        // });
    });
};