(function (ko, request) {
    "use strict";
    var ShellViewModel = function() {
        this.commandLine = ko.observable("please enter the command");
        this.onkeypress = function(data, e) {
            var event = e || window.event;
            var charCode = event.which || event.keyCode;
            if (charCode === 13) {
                console.log("user pressed enter key");
                request
                    .post('/api/documents')
                    .send({ command: this.commandLine })
                    .set('Accept', 'application/json')
                    .end(function (err, res) {
                        // Calling the end function will send the request
                    });

            }
            return true;
        }
    };

    ko.applyBindings(new ShellViewModel(), document.getElementById("shell"));
}(ko, window.superagent));