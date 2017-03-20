(function (ko, request) {
    "use strict";
    var ShellViewModel = function () {
        this.commandReady = false;
        this.commandLineHasFocus = ko.observable(true);
        this.commandLineValue = ko.observable("");
        this.commandResults = ko.observableArray();
        this.onCommandChange = function() {
            if (this.commandReady) {
                var cr = this.commandResults;
                request
                    .post('/api/documents')
                    .send({ commandText: event.target.value })
                    .set('Accept', 'application/json')
                    .end(function (err, res) {
                        if (res.statusCode === 204) {
                            cr.push("nothing to process");
                        } else {
                            cr.push(res.body.content);
                        }
                    });
                this.commandLineValue("");
            }
        };
        this.onKeyPress = function (data, e) {
            var event = e || window.event;
            var charCode = event.which || event.keyCode;
            this.commandReady = charCode === 13;
            return true;
        };
    };

    ko.applyBindings(new ShellViewModel(), document.getElementById("shell"));
}(ko, window.superagent));