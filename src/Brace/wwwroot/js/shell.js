(function (ko, request) {
    "use strict";

    var CommandResultViewModel = function(text, type) {
        this.text = ko.observable(text);
        this.type = ko.observable(type);

        this.statusStyle = ko.pureComputed(function () {
            switch (this.type()) {
            case 0:
                return "info";
            case 1:
                return "warning";
            case 2:
                return "error";
            default:
                return "info";
            }
        }, this);
    }

    var ShellViewModel = function () {
        this.commandReady = false;
        this.commandLineEnabled = ko.observable(true);
        this.commandLineHasFocus = ko.observable(true);
        this.commandLineValue = ko.observable("");
        this.commandResults = ko.observableArray();

        this.onCommandChange = function() {
            if (this.commandReady) {
                var that = this;
                var animationId;
                request
                    .post('/api/documents')
                    .send({ commandText: event.target.value })
                    .set('Accept', 'application/json')
                    .end(function (err, res) {
                        if (res.statusCode >= 200 && res.statusCode < 300) {
                            that.commandResults.push(new CommandResultViewModel(res.body.content, res.body.type));
                        } else if (res.statusCode >= 500) {
                            that.commandResults.push(new CommandResultViewModel("error occured. please make sure that command is correct.", 2));
                        }
                        that.commandLineStopAnimation(animationId);
                    });
                animationId = this.commandLineStartAnimation();
            }
        };
        this.onKeyPress = function (data, e) {
            var event = e || window.event;
            var charCode = event.which || event.keyCode;
            this.commandReady = charCode === 13;
            return true;
        };

        this.commandLineStartAnimation = function () {
            this.commandLineHasFocus(false);
            this.commandLineEnabled(false);
            var commandLineValue = this.commandLineValue;
            var current = 0;
            var animationChars = ["/", "-", "\\", "|"];
            return setInterval(function() {
                commandLineValue(animationChars[current % 4]);
                current++;
            }, 100);
        }

        this.commandLineStopAnimation = function(animationId) {
            clearInterval(animationId);
            this.commandLineValue("");
            this.commandLineEnabled(true);
            this.commandLineHasFocus(true);
        }
    };

    ko.applyBindings(new ShellViewModel(), document.getElementById("shell"));
}(ko, window.superagent));