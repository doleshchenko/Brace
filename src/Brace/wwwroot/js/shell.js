(function (ko, request) {
    "use strict";

    var PlainResultViewModel = function (item) {
        this.formatItem = function(item) {
            var resultStr = "";
            for (var propertyName in item) {
                if (item.hasOwnProperty(propertyName)) {
                    resultStr += item[propertyName] + " ";
                }
            }
            return resultStr;
        }
        this.item = ko.observable(this.formatItem(item));
    }

    var TableResultViewModel = function(items) {
        var that = this;
        this.items = ko.observableArray(items);
        this.columnNames = ko.computed(function () {
            if (that.items().length === 0) {
                return [];
            }
            var props = [];
            var obj = that.items()[0];
            for (var name in obj) {
                if (obj.hasOwnProperty(name)) {
                    props.push(name);
                }
            }
            return props;
        });

        this.columnNamesToDisplay = ko.computed(function () {
            var names = [];
            var columnNames = that.columnNames();
            for (var i = 0; i < columnNames.length; i++) {
                var columnName = columnNames[i];
                var j = columnName.length - 1;
                for (; j > 0; j--) {
                    if (columnName[j] === columnName[j].toUpperCase()) {
                        break;
                    }
                }
                var name = columnName.substring(j, columnName.length);
                names.push(name[0].toLowerCase() + name.slice(1));
            }
            return names;
        });
    }

    var CommandResultViewModel = function(result, type, forCommand) {
        this.result = result;
        this.forCommand = ko.observable(forCommand);
        this.type = ko.observable(type);
        var that = this;
        this.displayMode = function() {
            return that.result instanceof TableResultViewModel ? "table" : "plain";
        };
        this.statusStyle = ko.pureComputed(function () {
            switch (that.type()) {
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
                var commandText = event.target.value;
                request
                    .post('/api/documents')
                    .send({ commandText: commandText })
                    .set('Accept', 'application/json')
                    .end(function (err, res) {
                        if (res.statusCode >= 200 && res.statusCode < 300) {
                            var content = res.body.content;
                            if (content.constructor === Array) {
                                that.commandResults.push(new CommandResultViewModel(new TableResultViewModel(content), res.body.type, commandText));
                            } else {
                                that.commandResults.push(new CommandResultViewModel(new PlainResultViewModel(content), res.body.type, commandText));
                            }
                            
                        } else if (res.statusCode >= 500) {
                            that.commandResults.push(new CommandResultViewModel(new PlainResultViewModel({
                                errorText: "error occured. try it again."
                            }), 2, commandText));
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