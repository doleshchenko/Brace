(function (ko) {
    "use strict";
    var ShellViewModel = function() {
        this.commandLine = ko.observable("please enter the command");
        this.onkeypress = function(data, e) {
            var event = e || window.event;
            var charCode = event.which || event.keyCode;

            if (charCode === 13) {
                console.log("user pressed enter key");
            }
            return true;
        }
    };

    ko.applyBindings(new ShellViewModel(), document.getElementById("shell"));
}(ko));