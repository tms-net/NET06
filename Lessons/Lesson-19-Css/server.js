var fs = require('fs'),
    http = require('http'),
    path = require('path');

var mimeTypes = {
    ".js": "text/javascript"
}

http.createServer(function (req, res) {
  var filePath = __dirname + req.url;
  fs.readFile(filePath, function (err,data) {
    if (err) {
      res.writeHead(404);
      res.end(JSON.stringify(err));
      return;
    }
    var headers = [];
    var ext = path.extname(filePath);
    if (ext && mimeTypes[ext]) {
        headers.push(['Content-Type', mimeTypes[ext]]);
    }
    res.writeHead(200, headers);
    res.end(data);
  });
}).listen(3000);