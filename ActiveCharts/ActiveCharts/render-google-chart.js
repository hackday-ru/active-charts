var page = require('webpage').create(),
    system = require('system'),
    fs = require('fs'),
    address, output;

address = system.args[1];
output = system.args[2];

page.viewportSize = { width: 1024, height: 600 };

page.open(address, function () {

     window.setTimeout(function () {
         var s = document.querySelector("svg").parentNode.innerHTML.replace(/<\/svg>.*/i, "</svg>");
         fs.write(output, s, 'w');
         phantom.exit();
     }, 3000);
});