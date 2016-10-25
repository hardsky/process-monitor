// A simple templating method for replacing placeholders enclosed in curly braces.
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

$(function () {

    var hub = $.connection.processHub, // the generated client-side hub proxy
        processTableBody = $('#processTable tbody'),
        rowTemplate = '<tr><td>{Id}</td><td>{Priority}</td></tr>';

    function formatProcess(process) {
        return $.extend(process, {
            Id: process.Id,
            Priority: process.Priority,
        });
    }

    function init() {
        hub.server.getMonitorData().done(function (data) {
            processTableBody.empty();
            $.each(data.Processes, function (i, v) {
                var process = formatProcess(v);
                var raw = rowTemplate.supplant(process);
                processTableBody.append(raw);
            });
        });
    }

    hub.client.update = function (data) {
        processTableBody.empty();
        $.each(data.Processes, function (i, v) {
            var process = formatProcess(v);
            var raw = rowTemplate.supplant(process);
            processTableBody.append(raw);
        });
    };

    // Start the connection
    $.connection.hub.start().done(init);
});