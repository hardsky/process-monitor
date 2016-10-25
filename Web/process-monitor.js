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
        rowTemplate = '<tr><td>{Id}</td><td>{Priority}</td><td>{VirtMemory}</td><td>{PhysMemory}</td><td>{Name}</td></tr>';

    function formatTimeFromSec(v){
        var vals = [],
            s = v % 60,
            m = (v - s) / 60 % 60;
            //h = ((v - s - m * 60) / 60 / 60 / 60).toFixed(0)
        return h + ":" + s + ":" + m;
    }

    function formatProcess(process) {
        return $.extend(process, {
            VirtMemory: (process.VirtMemory / 1000).toFixed(2),
            PhysMemory: (process.PhysMemory / 1000).toFixed(2),
            //TimeRunning: formatTimeFromSec(process.TimeRunning),
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