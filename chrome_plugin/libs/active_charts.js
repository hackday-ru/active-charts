/**
 * Created by avdemashov_10 on 16.04.16.
 */
ActiveCharts = {};

ActiveCharts.init = function () {
    console.log('plugin was started');
};

(function () {
    ActiveCharts.init();
})();

chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    console.log(request);
    
    if (request.action == "getSelection") {
        var selection = window.getSelection();
        if (selection) {
            var currentObject = selection.focusNode.parentElement;
            var xpath = ActiveCharts.getXPath(currentObject);
            console.log("xpath: " + xpath);

            var message = '';

            var requestStatus = ActiveCharts.sendData({'xpath': xpath, 'url': request.url, 'token': request.token});
            requestStatus.done(function () {
                console.log("success");
                message = "success";
            }).fail(function () {
                console.log("error");
                message = "error";
            }).always(function () {
                sendResponse({"requestStatus": message});
            });
        } else {
            sendResponse({});
        }
    } else {
        sendResponse({});
    }


});

ActiveCharts.sendData = function (jsonData) {
    return jQuery.ajax({
        type: "POST",
        url: 'http://activecharts.tk/observe/add',
        data: jsonData,
        dataType: "json"
    });
};


ActiveCharts.getXPath = function (node) {

    var comp, comps = [];
    var xpath = '';
    var getPos = function (node) {
        var position = 1, curNode;
        if (node.nodeType == Node.ATTRIBUTE_NODE) {
            return null;
        }
        for (curNode = node.previousSibling; curNode; curNode = curNode.previousSibling) {
            if (curNode.nodeName == node.nodeName) {
                ++position;
            }
        }
        return position;
    };

    if (node instanceof Document) {
        return '/';
    }

    for (; node && !(node instanceof Document); node = node.nodeType == Node.ATTRIBUTE_NODE ? node.ownerElement : node.parentNode) {
        comp = comps[comps.length] = {};
        switch (node.nodeType) {
            case Node.TEXT_NODE:
                comp.name = 'text()';
                break;
            case Node.ATTRIBUTE_NODE:
                comp.name = '@' + node.nodeName;
                break;
            case Node.PROCESSING_INSTRUCTION_NODE:
                comp.name = 'processing-instruction()';
                break;
            case Node.COMMENT_NODE:
                comp.name = 'comment()';
                break;
            case Node.ELEMENT_NODE:
                comp.name = node.nodeName;
                break;
        }
        comp.position = getPos(node);
    }

    for (var i = comps.length - 1; i >= 0; i--) {
        comp = comps[i];
        xpath += '/' + comp.name;
        if (comp.position != null) {
            xpath += '[' + comp.position + ']';
        }
    }

    return xpath;
};